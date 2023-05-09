using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IPrecoPecasService
    {
        Task<CommandResult> Create(PrecoPecaInput input);
        Task<CommandResult> Update(int id, PrecoPecaInput input);
        Task<CommandResult> UpdateStatus(int idPrecoPeca, bool status);
        Task<CommandResult> GetPrecoPeca(string filtro, int pagina, int qtdPorPagina);
    }
}
