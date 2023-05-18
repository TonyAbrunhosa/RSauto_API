using RSauto.Domain.Entities.Cadastro.Fornecedor.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IFornecedorRepository
    {
        Task<IEnumerable<object>> Listar(FornecedorListarInput input);
    }
}
