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
using static Dapper.SqlMapper;
using System.Transactions;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class PrecoPecasRepository : IPrecoPecasRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<PrecoPecasRepository> _logger;

        public PrecoPecasRepository(SqlCommunication sql, ILogger<PrecoPecasRepository> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        public async Task<bool> UpdateStatus(int idPrecoPeca, bool status)
        {
            return await _sql.ExcuteAsyncDapper("BEGIN UPDATE PRECO_PECAS SET STATUS = @Status WHERE ID_PRECO_PECA = @IdPrecoPeca END", new { IdPrecoPeca = idPrecoPeca, Status = status }) > 0;
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

        private async Task<int> Create<T>(T entity, SqlConnection connection, SqlTransaction transaction) where T : class
        {
            return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
        }       

        private async Task<int> CrudPrecoPecas(PrecoPecasEntity entity, SqlConnection connection, SqlTransaction transaction)
        {
            if (entity.ID_PECA == 0)
                entity.ID_PECA = await Create(entity.Pecas, connection, transaction);
            if (entity.ID_MARCA_PECAS == 0)
                entity.ID_MARCA_PECAS = await Create(entity.MarcasPecas, connection, transaction);
            if (entity.ModelosVeiculos.ID_MARCA == 0)
                entity.ModelosVeiculos.ID_MARCA = await Create(entity.MarcasVeiculos, connection, transaction);
            if (entity.ID_MODELO == 0)
                entity.ID_MODELO = await Create(entity.ModelosVeiculos, connection, transaction);

            if (entity.ID_PRECO_PECA == 0)
                return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            return entity.ID_PRECO_PECA;
        }
        private async Task CrudHistoricoPrecosPecas(List<HistoricosPrecoPecasEntity> hisEntity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var entity in hisEntity)
            {
                var idEstoque = await CrudEstoquePecas(entity.EstoquePecas, idPrecoPeca, connection, transaction);

                if(entity.ID_FORNECEDOR == 0)
                    entity.ID_FORNECEDOR = await Create(entity.Fornecedores, connection, transaction);

                if (entity.ID_HIST_PRECO_PECA == 0)
                {
                    if (idEstoque > 0)
                        entity.ID_ESTOQUE_PECAS = idEstoque;

                    entity.ID_PRECO_PECA = idPrecoPeca;
                    await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
                }
                else
                {
                    await connection.ExecuteAsync("BEGIN UPDATE HISTORICOS_PRECO_PECAS SET STATUS = @STATUS, ID_FORNECEDOR = @ID_FORNECEDOR WHERE ID_HIST_PRECO_PECA = @ID_HIST_PRECO_PECA END",
                       new { STATUS = entity.STATUS, ID_FORNECEDOR = entity.ID_FORNECEDOR, ID_HIST_PRECO_PECA = entity.ID_HIST_PRECO_PECA }, transaction: transaction, commandTimeout: 900);
                }
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
