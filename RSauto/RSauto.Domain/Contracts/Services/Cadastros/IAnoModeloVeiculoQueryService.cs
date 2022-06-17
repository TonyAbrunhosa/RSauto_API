using RSauto.Domain.Contracts.Command;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Cadastros
{
    public interface IAnoModeloVeiculoQueryService
    {
        Task<ICommandResult> Listar();
    }
}
