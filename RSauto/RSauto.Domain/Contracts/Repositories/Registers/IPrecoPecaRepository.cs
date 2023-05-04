using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IPrecoPecasRepository
    {
        Task<int> Create(PrecoPecasEntity entity);
        Task<bool> Update(PrecoPecasEntity entity);
        Task<bool> UpdateStatus(int idPrecoPeca, bool status);
        Task<IEnumerable<PrecoPecasEntity>> GetPrecoPeca(string filtro, int pagina, int qtdPorPagina);
    }
}
