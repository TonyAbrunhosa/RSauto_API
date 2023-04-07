using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface ICilindradaVeiculosRepository
    {        
        Task<IEnumerable<CilindradaVeiculosEntity>> Listar();
        Task<bool> PossuiCilindrada(string nome, int iD_CILINDRADA = 0);
    }
}
