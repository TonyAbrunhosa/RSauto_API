using RSauto.Domain.Contracts.Repositories;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Token.Input;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly SqlCommunication _sql;

        public TokenRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public Task<LoginsEntity> BuscarUsuario(DadosTokenInput input)
        {
            return _sql.QueryFirstOrDefaultAsyncDapper<LoginsEntity>(@"
            BEGIN
	            SELECT
		                ID_USUARIO, NOME_USUARIO, LOGIN_USUARIO, SENHA_USUARIO, DATA_CRIACAO
	            FROM LOGINS WITH(NOLOCK)
	            WHERE LOGIN_USUARIO = @login AND SENHA_USUARIO = @senha
            END", new { login = input.Login, senha = input.SenhaHash });
        }
    }
}
