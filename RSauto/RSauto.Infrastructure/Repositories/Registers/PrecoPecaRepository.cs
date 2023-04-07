using Dapper;
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
    public class PrecoPecaRepository : IListaPrecoPecasRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<PrecoPecaRepository> _logger;

        public PrecoPecaRepository(SqlCommunication sql, ILogger<PrecoPecaRepository> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        public async Task AlterarStaus(int idPrecoPeca, int status)
        {
            await _sql.ExcuteAsyncDapper("BEGIN UPDATE LISTA_PRECO_PECAS SET STATUS = @Status WHERE ID_PRECO_PECA = @IdPrecoPeca END", new { IdPrecoPeca = idPrecoPeca, Status = status });
        }

        public async Task<bool> Update(PrecoPecasEntity entity)
        {
            using (var connection = new SqlConnection(_sql.GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await CrudPrecoPecas(entity, connection, transaction);
                        await CrudHistoricoPrecosPecas(entity.HistoricoPrecosPecas, entity.ID_PRECO_PECA, connection, transaction);
                        await CrudListaAnoModeloPreco(entity.ListaAnoModeloPreco, entity.ID_PRECO_PECA, connection, transaction);                        

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

        public async Task<int> Create(PrecoPecasEntity entity)
        {
            using (var connection = new SqlConnection(_sql.GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {                        
                        int IdPrecoPeca = await CrudPrecoPecas(entity, connection, transaction);
                        await CrudHistoricoPrecosPecas(entity.HistoricoPrecosPecas, IdPrecoPeca, connection, transaction);
                        await CrudListaAnoModeloPreco(entity.ListaAnoModeloPreco, IdPrecoPeca, connection, transaction);                        

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
        private async Task<int> CrudPrecoPecas(PrecoPecasEntity entity, SqlConnection connection, SqlTransaction transaction)
        {
            if(entity.ID_PRECO_PECA == 0)
                return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            return entity.ID_PRECO_PECA;
        }
        private async Task CrudHistoricoPrecosPecas(HistoricosPrecoPecasEntity entity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {            
            var idEstoque = await CrudEstoquePecas(entity.EstoquePecas, idPrecoPeca, connection, transaction);

            if (entity.ID_HIST_PRECO_PECA == 0)
            {
                if(idEstoque > 0)
                    entity.ID_ESTOQUE_PECAS= idEstoque;

                entity.ID_PRECO_PECA = idPrecoPeca;
                await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            }
            else
            {
                await connection.ExecuteAsync("BEGIN UPDATE HISTORICOS_PRECO_PECAS SET STATUS = @STATUS, ID_FORNECEDOR = @ID_FORNECEDOR WHERE ID_HIST_PRECO_PECA = @ID_HIST_PRECO_PECA END",
                   new { STATUS = entity.STATUS, ID_FORNECEDOR = entity.ID_FORNECEDOR, ID_HIST_PRECO_PECA = entity.ID_HIST_PRECO_PECA }, transaction: transaction, commandTimeout: 900);
            }
            
        }
        private async Task<int> CrudEstoquePecas(EstoquePecasEntity entity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            if (entity.ID_ESTOQUE_PECAS == 0)
            {                
                entity.ID_PRECO_PECA = idPrecoPeca;
                if(entity.QTDE_ESTOQUE > 0)
                    return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            }
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            return 0;
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
