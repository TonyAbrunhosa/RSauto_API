using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities.Token.Input;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services
{
    public interface ITokenService
    {
        Task<ICommandResult> ObterToken(DadosTokenInput input);
    }
}
