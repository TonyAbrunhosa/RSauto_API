using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IMarcasVeiculosQueryRepository
    {
        Task<IEnumerable<MarcasVeiculosEntity>> Listar();
        Task<bool> PossuiMarcaVeiculo(string nome, int id = 0);
    }
}
