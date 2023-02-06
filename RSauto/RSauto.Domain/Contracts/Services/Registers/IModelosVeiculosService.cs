using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IModelosVeiculosService
    {
        Task<ICommandResult> Insert(ModelosVeiculosInput input);
        Task<ICommandResult> Update(int id, ModelosVeiculosInput input);
        Task<ICommandResult> Remove(int id);
    }
}
