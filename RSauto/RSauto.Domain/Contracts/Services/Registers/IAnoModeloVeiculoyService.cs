using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IAnoModeloVeiculoService
    {
        Task<ICommandResult> Create(string nome);
        Task<ICommandResult> Update(AnoModeloVeiculoEntity entity);
        Task<ICommandResult> Listar();
    }
}
