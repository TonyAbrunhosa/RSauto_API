using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IPrecoPecasService
    {
        Task<CommandResult> Create(PrecoPecasCreateInput input);
        Task<CommandResult> Update(int id, PrecoPecasUpdateInput input);
        Task<CommandResult> UpdateStatus(int idPrecoPeca, bool status);
    }
}
