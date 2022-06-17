using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Cadastros
{
    public interface IMarcasPecasService
    {
        Task<ICommandResult> Novo(string nome);
        Task<ICommandResult> Atualizar(MarcasPecasEntity entity);
        Task<ICommandResult> Remover(int id);
    }
}
