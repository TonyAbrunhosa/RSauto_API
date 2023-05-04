using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class MarcasVeiculosRepository : IMarcasVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public MarcasVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<MarcasVeiculosEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<MarcasVeiculosEntity>(@"BEGIN SELECT ID_MARCA, DESCRICAO FROM MARCAS_VEICULOS END");
        }

        public async Task<bool> PossuiMarcaVeiculo(string nome, int id = 0)
        {
            return ((await _sql.QueryAsyncDapper<MarcasVeiculosEntity>(@"
                BEGIN 
                    SELECT 
                        TOP 1 
                        ID_MARCA 
                    FROM MARCAS_VEICULOS 
                    WHERE DESCRICAO = @nome 
                    AND (@id = 0 OR ID_MARCA != @id)
                END", new { nome = nome, id = id }))?.Count() ?? 0) > 0;
        }
    }
}
