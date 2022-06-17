using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class MarcasPecasQueryRepository : IMarcasPecasQueryRepository
    {
        private readonly SqlCommunication _sql;

        public MarcasPecasQueryRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<MarcasPecasEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<MarcasPecasEntity>(@"BEGIN SELECT ID_MARCA_PECAS, DESCRICAO FROM MARCAS_PECAS WITH(NOLOCK) END");
        }

        public async Task<bool> PossuiMarcaPeca(string nome, int id = 0)
        {
            return ((await _sql.QueryAsyncDapper<MarcasVeiculosEntity>(@"
                BEGIN 
                    SELECT 
                        TOP 1 
                        ID_MARCA_PECAS 
                    FROM MARCAS_PECAS 
                    WHERE DESCRICAO = @nome 
                    AND (@id = 0 OR ID_MARCA_PECAS != @id)
                END", new { nome = nome, id = id }))?.Count() ?? 0) > 0;
        }
    }
}
