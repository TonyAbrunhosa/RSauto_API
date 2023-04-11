using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IPrecoPecasRepository
    {
        Task<int> Create(PrecoPecasEntity entity);
        Task<bool> Update(PrecoPecasEntity entity);
        Task<bool> UpdateStatus(int idPrecoPeca, bool status);
    }
}
