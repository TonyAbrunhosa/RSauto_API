using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class CilindradaVeiculosRepository : ICilindradaVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public CilindradaVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<CilindradaVeiculosEntity>> Listar()
        {
            return await _sql.QueryAsyncDapper<CilindradaVeiculosEntity>(@"BEGIN SELECT ID_CILINDRADA, DESCRICAO FROM CILINDRADA_VEICULOS WITH(NOLOCK) END");
        }

        public async Task<bool> PossuiCilindrada(string nome, int id = 0)
        {
            return ((await _sql.QueryAsyncDapper<CilindradaVeiculosEntity>(@"
                BEGIN 
                    SELECT 
                        TOP 1 
                        ID_CILINDRADA 
                    FROM CILINDRADA_VEICULOS  WITH(NOLOCK)
                    WHERE DESCRICAO = @nome 
                    AND (@id = 0 OR ID_CILINDRADA != @id)
                END", new { nome = nome, id = id }))?.Count() ?? 0) > 0;
        }
    }
}
