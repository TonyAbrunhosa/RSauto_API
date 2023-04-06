using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface ICilindradaVeiculosRepository: IBaseCrudRepository
    {
        Task<IEnumerable<CilindradaVeiculosEntity>> Listar();
        Task<bool> PossuiMarcaPeca(string nome, int id = 0);
    }
}
