using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IListaPrecoPecaRepository
    {
        Task<int> Novo(ListaPrecoPecasEntity entity);
        Task<bool> Atualizar(ListaPrecoPecasEntity entity);
        Task AlterarStaus(int idPrecoPeca, int status);
    }
}
