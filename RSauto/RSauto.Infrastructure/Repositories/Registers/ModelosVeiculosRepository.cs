using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class ModelosVeiculosRepository : IModelosVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public ModelosVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<ModelosVeiculosEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<ModelosVeiculosEntity>(@"BEGIN SELECT ID_MODELO, DESCRICAO, ID_MARCA FROM MODELOS_VEICULOS END");
        }

        public async Task<bool> PossuiModeloVeiculo(string nome, int id = 0)
        {
            return ((await _sql.QueryAsyncDapper<ModelosVeiculosEntity>(@"
                BEGIN 
                    SELECT 
                        TOP 1 
                        ID_MODELO 
                    FROM MODELOS_VEICULOS 
                    WHERE NOME = @nome 
                    AND (@id = 0 OR ID_MARCA != @id)
                END", new { nome = nome, id = id }))?.Count() ?? 0) > 0;
        }
    }
}
