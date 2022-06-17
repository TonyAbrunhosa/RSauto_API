using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Cadastros
{
    public interface IAnoModeloVeiculoService
    {
        Task<ICommandResult> Novo(string nome);
        Task<ICommandResult> Atualizar(AnoModeloVeiculoEntity entity);
        Task<ICommandResult> Remover(int id);
    }
}
