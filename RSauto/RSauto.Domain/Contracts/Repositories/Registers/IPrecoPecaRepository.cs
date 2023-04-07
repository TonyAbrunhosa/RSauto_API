using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IListaPrecoPecasRepository
    {
        Task<int> Create(PrecoPecasEntity entity);
        Task<bool> Update(PrecoPecasEntity entity);
        Task AlterarStaus(int idPrecoPeca, int status);
    }
}
