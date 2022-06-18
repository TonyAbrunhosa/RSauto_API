using Microsoft.Extensions.Logging;
using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.ListaPrecoPecas.Output;
using RSauto.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class ListaPrecoPecasQueryRepository : IListaPrecoPecasQueryRepository
    {
        private readonly SqlCommunication _sql;
        private readonly ILogger<ListaPrecoPecasQueryRepository> _logger;

        public ListaPrecoPecasQueryRepository(SqlCommunication sql, ILogger<ListaPrecoPecasQueryRepository> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        public async Task<ListaPrecoPecasOutput> Buscar(int idPrecoPeca)
        {
			return await _sql.QueryFirstOrDefaultAsyncDapper<ListaPrecoPecasOutput>(@"
				BEGIN
					SELECT
						LPP.ID_PRECO_PECA,
						LPP.ID_MARCA_PECAS,
						MODV.ID_MARCA,
						LPP.ID_MODELO,
						LPP.ID_PECA,
						LPP.STATUS,
						LPP.CODIGO_PECA,
						EP.QTDE_ESTOQUE,
						HISTORICO_PRECO.CUSTO,
						HISTORICO_PRECO.PRECO,
						LAMP.ID_ANO_MOD_PRECO,
						LAMP.ID_ANO_MOD_VEIC,
						LAMP.ID_PRECO_PECA
					FROM LISTA_PRECO_PECAS LPP WITH(NOLOCK)
					LEFT JOIN ESTOQUE_PECAS EP WITH(NOLOCK) ON LPP.ID_PRECO_PECA = EP.ID_PRECO_PECA	
					LEFT JOIN LISTA_ANO_MODELO_PRECO LAMP WITH(NOLOCK) ON LAMP.ID_PRECO_PECA = LPP.ID_PRECO_PECA
					LEFT JOIN MODELOS_VEICULOS MODV WITH(NOLOCK) ON MODV.ID_MODELO = LPP.ID_MODELO
					OUTER APPLY(SELECT TOP 1 HPP.CUSTO, HPP.PRECO FROM HISTORICO_PRECOS_PECAS HPP WITH(NOLOCK) 
						WHERE LPP.ID_PRECO_PECA = HPP.ID_PRECO_PECA
						ORDER BY HPP.ID_HIST_PRECO DESC
					)HISTORICO_PRECO
					WHERE LPP.ID_PRECO_PECA = @ID_PRECO_PECA
				END", new { ID_PRECO_PECA = idPrecoPeca });
        }

        public async Task<IEnumerable<ListarListaPrecoPecasOutput>> Listar()
        {
			return await _sql.QueryAsyncDapper<ListarListaPrecoPecasOutput>(@"
				BEGIN
					SELECT
						LPP.ID_PRECO_PECA,
						P.DESCRICAO [DESCRICAO_PECA],
						MP.DESCRICAO [DESCRICAO_MARCA_PECA],
						MV.NOME [MODELO_VEICULO],
						EP.QTDE_ESTOQUE,
						HISTORICO_PRECO.CUSTO,
						HISTORICO_PRECO.PRECO,
						LAMP.ID_ANO_MOD_PRECO,
						ANV.DESCRICAO [ANO_MODELO],
						CASE WHEN LPP.STATUS = 'A' THEN 'ATIVO' ELSE 'INATIVO' END [STATUS]
					FROM LISTA_PRECO_PECAS LPP WITH(NOLOCK)
					LEFT JOIN ESTOQUE_PECAS EP WITH(NOLOCK) ON LPP.ID_PRECO_PECA = EP.ID_PRECO_PECA	
					LEFT JOIN LISTA_ANO_MODELO_PRECO LAMP WITH(NOLOCK) ON LAMP.ID_PRECO_PECA = LPP.ID_PRECO_PECA
					LEFT JOIN ANO_MODELO_VEICULO ANV WITH(NOLOCK) ON LAMP.ID_ANO_MOD_VEIC = ANV.ID_ANO_MOD_VEIC
					INNER JOIN PECAS P WITH(NOLOCK) ON LPP.ID_PECA = P.ID_PECA
					INNER JOIN MARCAS_PECAS MP WITH(NOLOCK) ON LPP.ID_MARCA_PECAS = MP.ID_MARCA_PECAS
					LEFT JOIN MODELOS_VEICULOS MV WITH(NOLOCK) ON LPP.ID_MODELO = MV.ID_MODELO
					OUTER APPLY(SELECT TOP 1 HPP.CUSTO, HPP.PRECO FROM HISTORICO_PRECOS_PECAS HPP WITH(NOLOCK) 
						WHERE LPP.ID_PRECO_PECA = HPP.ID_PRECO_PECA
						ORDER BY HPP.ID_HIST_PRECO DESC
					)HISTORICO_PRECO
				END");
        }

        public async Task<IEnumerable<PesquisaListaPrecoPecasOutput>> PesquisaListaPrecoPecas(int IdModelo, int IdAnoModeloVeiculo)
        {
            return await _sql.QueryAsyncDapper<PesquisaListaPrecoPecasOutput>(@"
                BEGIN
	                SELECT
		                LPP.ID_PRECO_PECA,
		                P.DESCRICAO [DESCRICAO_PECA],
		                MP.DESCRICAO [DESCRICAO_MARCA_PECA],		
		                HISTORICO_PRECO.CUSTO,
		                HISTORICO_PRECO.PRECO,
		                LAMP.ID_ANO_MOD_PRECO,
		                ANV.DESCRICAO [ANO_MODELO]
	                FROM LISTA_PRECO_PECAS LPP WITH(NOLOCK)	
	                LEFT JOIN LISTA_ANO_MODELO_PRECO LAMP WITH(NOLOCK) ON LAMP.ID_PRECO_PECA = LPP.ID_PRECO_PECA
	                INNER JOIN ANO_MODELO_VEICULO ANV WITH(NOLOCK) ON LAMP.ID_ANO_MOD_VEIC = ANV.ID_ANO_MOD_VEIC
	                INNER JOIN PECAS P WITH(NOLOCK) ON LPP.ID_PECA = P.ID_PECA
	                INNER JOIN MARCAS_PECAS MP WITH(NOLOCK) ON LPP.ID_MARCA_PECAS = MP.ID_MARCA_PECAS	
	                OUTER APPLY(SELECT TOP 1 HPP.CUSTO, HPP.PRECO FROM HISTORICO_PRECOS_PECAS HPP WITH(NOLOCK) 
		                WHERE LPP.ID_PRECO_PECA = HPP.ID_PRECO_PECA
		                ORDER BY HPP.ID_HIST_PRECO DESC
	                )HISTORICO_PRECO
	                WHERE (LPP.ID_MODELO = @ID_MODELO OR LPP.ID_MODELO IS NULL) 
					AND ANV.ID_ANO_MOD_VEIC = @ID_ANO_MOD_VEIC
					AND LPP.STATUS = 'A'
                END", new { ID_MODELO = IdModelo, ID_ANO_MOD_VEIC = IdAnoModeloVeiculo });
        }

        public async Task<bool> PrecoOuCustoFoiAlterado(int idPrecoPeca, decimal custo, decimal preco)
        {
            return await _sql.QueryFirstOrDefaultAsyncDapper<int>(@"
                BEGIN 
	                SELECT
		                TOP 1
		                HPP.ID_HIST_PRECO
	                FROM HISTORICO_PRECOS_PECAS HPP WITH(NOLOCK)
	                LEFT JOIN LISTA_PRECO_PECAS LPP WITH(NOLOCK) ON HPP.ID_PRECO_PECA = LPP.ID_PRECO_PECA
	                WHERE LPP.ID_PRECO_PECA = @ID_PRECO_PECA AND HPP.CUSTO = @CUSTO AND HPP.PRECO = @PRECO
	                ORDER BY HPP.ID_PRECO_PECA DESC
                END", new { ID_PRECO_PECA = idPrecoPeca, CUSTO = custo, PRECO = preco  }) > 0;
        }
    }
}
