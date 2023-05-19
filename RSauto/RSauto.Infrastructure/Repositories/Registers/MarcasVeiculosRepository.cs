using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RSauto.Domain.Entities.Cadastro.PrecoPecas.Input.PrecoPecaInput.VeiculoInput;
using static RSauto.Domain.Entities.Cadastro.PrecoPecas.Input.PrecoPecaInput;
using static Slapper.AutoMapper;

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
            var dados = await _sql.QueryAsyncDapper<dynamic>(@"
                BEGIN 
                    SELECT 
                        MAV.ID_MARCA, 
                        MAV.DESCRICAO,
		                MOV.ID_MODELO [MODELOS_ID_MODELO],
		                MOV.DESCRICAO [MODELOS_DESCRICAO]
                    FROM MARCAS_VEICULOS MAV WITH(NOLOCK)
                    LEFT JOIN MODELOS_VEICULOS MOV WITH(NOLOCK) ON MAV.ID_MARCA = MOV.ID_MARCA
                END");

            Configuration.AddIdentifier(typeof(MarcasVeiculosEntity), "ID_MARCA");
            Configuration.AddIdentifier(typeof(ModelosVeiculosEntity), "ID_MODELO");

            return MapDynamic<MarcasVeiculosEntity>(dados);
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
