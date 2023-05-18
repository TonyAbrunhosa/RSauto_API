using RSauto.Domain.Entities.Cadastro.Cliente.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IClienteRepository
    {
        Task<IEnumerable<object>> Listar(ClienteListarInput input);
    }
}
