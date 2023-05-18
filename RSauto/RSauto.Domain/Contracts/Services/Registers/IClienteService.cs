using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities.Cadastro.Cliente.Input;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IClienteService
    {
        Task<ICommandResult> Create(ClienteInput input);
        Task<ICommandResult> Update(int id, ClienteInput input);
        Task<ICommandResult> Listar(ClienteListarInput input);
    }
}
