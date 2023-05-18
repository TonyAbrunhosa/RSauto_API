using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities.Cadastro.Cliente.Input;
using RSauto.Shared.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SqlCommunication _sql;

        public ClienteRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<object>> Listar(ClienteListarInput input)
        {
            return await _sql.QueryAsyncDapper<object>(@"
			BEGIN
				SELECT
					ID_CLIENTE [Id],
					NOME [Nome],
					RAZAO_SOCIAL [RazaoSocial],
					CPF_CNPJ [Documento],
					TELEFONE [Telefone],
					CELULAR [Celular],
					EMAIL [Email],
					CEP [Cep],
					LOGRADOURO [Logradouro],
					NUMERO [Numero],
					COMPLEMENTO [Complemento],
					CIDADE [Cidade],
					ESTADO [Estado]
				FROM CLIENTES WITH(NOLOCK)
				WHERE (@RAZAO_SOCIAL = '' OR RAZAO_SOCIAL LIKE '%' + @RAZAO_SOCIAL + '%') AND
				(@NOME = '' OR NOME LIKE '%' + @NOME + '%') AND
				(@CPF_CNPJ = '' OR CPF_CNPJ LIKE '%' + @CPF_CNPJ + '%')
				ORDER BY ID_CLIENTE
				OFFSET (@PAGE - 1) * @PAGE_SIZE ROWS
				FETCH NEXT @PAGE_SIZE ROWS ONLY;
			END", new { NOME = input.Nome, RAZAO_SOCIAL = input.RazaoSocial, CPF_CNPJ = input.Documento, PAGE = input.Page, PAGE_SIZE = input.PageSize });
        }
    }
}
