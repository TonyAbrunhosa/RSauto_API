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
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
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
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex; 
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
            else if (!string.IsNullOrEmpty(entity.Pecas.DESCRICAO))
                await connection.ExecuteAsync("UPDATE PECAS SET DESCRICAO = @DESCRICAO WHERE ID_PECA = @ID_PECA", new { DESCRICAO = entity.Pecas.DESCRICAO, ID_PECA = entity.Pecas.ID_PECA }, transaction);

            if (entity.ID_MARCA_PECAS == 0)
                entity.ID_MARCA_PECAS = await Create(entity.MarcasPecas, connection, transaction);
            else if (!string.IsNullOrEmpty(entity.MarcasPecas.DESCRICAO))
                await connection.ExecuteAsync("UPDATE MARCAS_PECAS SET DESCRICAO = @DESCRICAO WHERE ID_MARCA_PECAS = @ID_MARCA_PECAS", new { DESCRICAO = entity.MarcasPecas.DESCRICAO, ID_MARCA_PECAS = entity.MarcasPecas.ID_MARCA_PECAS }, transaction);

            if (entity.MarcasVeiculos.ID_MARCA == 0)
                entity.MarcasVeiculos.ID_MARCA = await Create(entity.MarcasVeiculos, connection, transaction);
            else if(!string.IsNullOrEmpty(entity.MarcasVeiculos.DESCRICAO))
                await connection.ExecuteAsync("UPDATE MARCAS_VEICULOS SET DESCRICAO = @DESCRICAO WHERE ID_MARCA = @ID_MARCA", new { DESCRICAO = entity.MarcasVeiculos.DESCRICAO, ID_MARCA = entity.MarcasVeiculos.ID_MARCA }, transaction);

            if (entity.ID_PRECO_PECA == 0)
                entity.ID_PRECO_PECA = await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            await CreateModelosVeiculosPecas(entity.ModelosVeiculos, entity.MarcasVeiculos.ID_MARCA, entity.ID_PRECO_PECA, connection, transaction);

            return entity.ID_PRECO_PECA;
        }

        private async Task CreateModelosVeiculosPecas(List<ModelosVeiculosPecasEntity> modelosVeiculos, int idMarca, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var item in modelosVeiculos)
            {
                if(item.REMOVER)
                    await connection.DeleteAsync(item, transaction: transaction, commandTimeout: 900);
                else
                {
                    if(item.ID_MODELO == 0)
                    {
                        item.modelosVeiculos.ID_MARCA = idMarca;
                        item.ID_MODELO = await Create(item.modelosVeiculos, connection, transaction);
                    }
                    else
                        await connection.ExecuteAsync("UPDATE MODELOS_VEICULOS SET DESCRICAO = @DESCRICAO WHERE ID_MODELO = @ID_MODELO", new { DESCRICAO = item.modelosVeiculos.DESCRICAO, ID_MODELO = item.modelosVeiculos.ID_MODELO }, transaction);

                    if (item.ID_MOD_VEIC_PECAS == 0)
                    {
                        item.ID_PRECO_PECA = idPrecoPeca;
                        await connection.InsertAsync(item, transaction: transaction, commandTimeout: 900);
                    }
                }
            }
        }

        private async Task CrudHistoricoPrecosPecas(List<HistoricosPrecoPecasEntity> hisEntity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var entity in hisEntity)
            {
                entity.ID_ESTOQUE_PECAS = await CrudEstoquePecas(entity.EstoquePecas, idPrecoPeca, connection, transaction);

                if(entity.ID_FORNECEDOR == 0)
                    entity.ID_FORNECEDOR = await Create(entity.Fornecedores, connection, transaction);

                if (entity.ID_HIST_PRECO_PECA == 0)
                {
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
        private async Task<int?> CrudEstoquePecas(EstoquePecasEntity entity, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            if (entity.ID_ESTOQUE_PECAS == 0)
            {                
                entity.ID_PRECO_PECA = idPrecoPeca;
                if(entity.QTDE_ESTOQUE > 0)
                    return await connection.InsertAsync(entity, transaction: transaction, commandTimeout: 900);
            }
            else
                await connection.UpdateAsync(entity, transaction: transaction, commandTimeout: 900);

            return null;
        }
        private async Task CrudListaAnoModeloPreco(List<ListaAnoModeloPrecoEntity> listaAnoModeloPreco, int idPrecoPeca, SqlConnection connection, SqlTransaction transaction)
        {
            foreach (var item in listaAnoModeloPreco)
            {
                if(item.REMOVER)
                    await connection.DeleteAsync(item, transaction: transaction, commandTimeout: 900);
                else
                {
                    if(item.AnoModeloVeiculo.ID_ANO_MOD_VEIC == 0)
                        item.ID_ANO_MOD_VEIC = await Create(item.AnoModeloVeiculo, connection, transaction);
                    else
                       await connection.ExecuteAsync("UPDATE ANO_MODELO_VEICULO SET DESCRICAO = @DESCRICAO WHERE ID_ANO_MOD_VEIC = @ID_ANO_MOD_VEIC", new { DESCRICAO = item.AnoModeloVeiculo.DESCRICAO, ID_MODELO = item.AnoModeloVeiculo.ID_ANO_MOD_VEIC }, transaction);

                    if (item.ID_ANO_MOD_PRECO == 0)
                    {
                        item.ID_PRECO_PECA = idPrecoPeca;                        
                        await connection.InsertAsync(item, transaction: transaction, commandTimeout: 900);
                    }                    
                }
            }
        }
    }
}
