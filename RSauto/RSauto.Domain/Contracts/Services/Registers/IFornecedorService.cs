using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities.Cadastro.Fornecedor.Input;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface IFornecedorService
    {
        Task<ICommandResult> Create(FornecedorInput input);
        Task<ICommandResult> Update(int id, FornecedorInput input);
        Task<ICommandResult> Listar(FornecedorListarInput input);
    }
}
