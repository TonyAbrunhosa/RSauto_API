using RSauto.Domain.Contracts.Command;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IModelosVeiculosQueryService
    {
        Task<ICommandResult> Listar();
    }
}
