using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IMarcasPecasService
    {
        Task<ICommandResult> Insert(string nome);
        Task<ICommandResult> Update(MarcasPecasEntity entity);
        Task<ICommandResult> Remove(int id);
    }
}
