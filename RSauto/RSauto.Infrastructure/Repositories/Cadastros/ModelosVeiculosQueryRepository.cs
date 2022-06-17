using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class ModelosVeiculosQueryRepository : IModelosVeiculosQueryRepository
    {
        private readonly SqlCommunication _sql;

        public ModelosVeiculosQueryRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<ModelosVeiculosEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<ModelosVeiculosEntity>(@"BEGIN SELECT ID_MODELO, NOME, ID_MARCA FROM MODELOS_VEICULOS END");
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
