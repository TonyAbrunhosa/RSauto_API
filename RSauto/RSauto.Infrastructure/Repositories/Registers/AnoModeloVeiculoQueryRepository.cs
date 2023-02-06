using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class AnoModeloVeiculoQueryRepository : IAnoModeloVeiculoQueryRepository
    {
        private readonly SqlCommunication _sql;

        public AnoModeloVeiculoQueryRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<AnoModeloVeiculoEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<AnoModeloVeiculoEntity>(@"BEGIN SELECT ID_ANO_MOD_VEIC, DESCRICAO FROM ANO_MODELO_VEICULO WITH(NOLOCK) END");
        }

        public async Task<bool> PossuiMarcaPeca(string nome, int id = 0)
        {
            return ((await _sql.QueryAsyncDapper<AnoModeloVeiculoEntity>(@"
                BEGIN 
                    SELECT 
                        TOP 1 
                        ID_ANO_MOD_VEIC 
                    FROM ANO_MODELO_VEICULO 
                    WHERE DESCRICAO = @nome 
                    AND (@id = 0 OR ID_ANO_MOD_VEIC != @id)
                END", new { nome = nome, id = id }))?.Count() ?? 0) > 0;
        }
    }
}
