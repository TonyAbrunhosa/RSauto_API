using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IAnoModeloVeiculoRepository
    {
        Task<IEnumerable<AnoModeloVeiculoEntity>> Listar();
        Task<bool> PossuiMarcaPeca(string nome, int id = 0);
    }
}
