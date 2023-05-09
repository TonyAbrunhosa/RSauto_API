using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IPrecoPecasRepository
    {
        Task<int> Create(PrecoPecasEntity entity);
        Task<bool> Update(PrecoPecasEntity entity);
        Task<bool> UpdateStatus(int idPrecoPeca, bool status);
        Task<IEnumerable<PrecoPecaInput>> GetPrecoPeca(string filtro, int pagina, int qtdPorPagina);
    }
}
