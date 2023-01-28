using RSauto.Domain.Contracts.Command;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IAnoModeloVeiculoQueryService
    {
        Task<ICommandResult> Listar();
    }
}
