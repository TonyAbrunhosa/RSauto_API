using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IMarcasVeiculosService
    {
        Task<ICommandResult> Insert(string nome);
        Task<ICommandResult> Update(MarcasVeiculosEntity entity);
        Task<ICommandResult> Remove(int id);
    }
}
