using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Cadastros
{
    public interface IModelosVeiculosService
    {
        Task<ICommandResult> Novo(ModelosVeiculosInput input);
        Task<ICommandResult> Atualizar(int id, ModelosVeiculosInput input);
        Task<ICommandResult> Remover(int id);
    }
}
