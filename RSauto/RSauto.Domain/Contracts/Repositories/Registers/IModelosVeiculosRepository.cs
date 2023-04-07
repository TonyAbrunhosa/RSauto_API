using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IModelosVeiculosRepository
    {
        Task<IEnumerable<ModelosVeiculosEntity>> Listar();
        Task<bool> PossuiModeloVeiculo(string nome, int id = 0);
    }
}
