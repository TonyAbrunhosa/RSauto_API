using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class ListaPrecoPecaRepository : IListaPrecoPecasRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<ListaPrecoPecaRepository> _logger;

        public ListaPrecoPecaRepository(SqlCommunication sql, ILogger<ListaPrecoPecaRepository> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        public async Task AlterarStaus(int idPrecoPeca, int status)
        {
            await _sql.ExcuteAsyncDapper("BEGIN UPDATE LISTA_PRECO_PECAS SET STATUS = @Status WHERE ID_PRECO_PECA = @IdPrecoPeca END", new { IdPrecoPeca = idPrecoPeca, Status = status });
        }

        public async Task<bool> Atualizar(ListaPrecoPecasEntity entity)
        {
            using (var connection = new SqlConnection(_sql.GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await CrudListaPrecoPecas(entity, connection, transaction);
                        await NovoHistoricoPrecosPecas(entity.HistoricoPrecosPecas, entity.ID_PRECO_PECA, connection, transaction);
                        await CrudListaAnoModeloPreco(entity.ListaAnoModeloPreco, entity.ID_PRECO_PECA, connection, transaction);
                        await CrudEstoquePecas(entity.EstoquePecas, entity.ID_PRECO_PECA, connection, transaction);

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<int> Novo(ListaPrecoPecasEntity entity)
        {
            using (var connection = new SqlConnection(_sql.GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {                        
                        int IdPrecoPeca = await CrudListaPrecoPecas(entity, connection, transaction);
                        await NovoHistoricoPrecosPecas(entity.HistoricoPrecosPecas, IdPrecoPeca, connection, transaction);
                        await CrudListaAnoModeloPreco(entity.ListaAnoModeloPreco, IdPrecoPeca, connection, transaction);
                        await CrudEstoquePecas(entity.EstoquePecas, IdPrecoPeca, connection, transaction);

                        transaction.Commit();

                        return IdPrecoPeca;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
        private async Task<int> CrudListaPrecoPecas(ListaPrecoPecasEntity entity, SqlConnection connection, SqlTransaction transaction)
        {
            if(entity.ID_PRECO_PECA == 0)
                return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            return entity.ID_PRECO_PECA;
        }
        private async Task NovoHistoricoPrecosPecas(HistoricoPrecosPecasEntity entity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            if(entity.ID_HIST_PRECO == 0)
            {
                entity.ID_PRECO_PECA = idPrecoPeca;
                await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            }            
        }
        private async Task CrudEstoquePecas(EstoquePecasEntity entity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            if (entity.ID_ESTOQUE_PECAS == 0)
            {
                entity.ID_PRECO_PECA = idPrecoPeca;
                await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            }
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);
        }
        private async Task CrudListaAnoModeloPreco(List<ListaAnoModeloPrecoEntity> listaAnoModeloPreco, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var item in listaAnoModeloPreco)
            {
                if(item.REMOVER)
                    await connection.DeleteAsync(item, transaction: transaction, commandTimeout: 900);
                else
                {
                    if(item.ID_PRECO_PECA == 0)
                    {
                        item.ID_PRECO_PECA = idPrecoPeca;
                        await connection.InsertAsync(item, transaction: transaction, commandTimeout: 900);
                    }                    
                }
            }
        }
    }
}
