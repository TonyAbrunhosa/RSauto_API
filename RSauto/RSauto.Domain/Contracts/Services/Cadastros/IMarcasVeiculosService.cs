using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Cadastros
{
    public interface IMarcasVeiculosService
    {
        Task<ICommandResult> Novo(string nome);
        Task<ICommandResult> Atualizar(MarcasVeiculosEntity entity);
        Task<ICommandResult> Remover(int id);
    }
}
