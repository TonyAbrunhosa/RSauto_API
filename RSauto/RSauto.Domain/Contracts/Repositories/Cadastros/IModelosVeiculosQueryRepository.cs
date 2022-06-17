using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IModelosVeiculosQueryRepository
    {
        Task<IEnumerable<ModelosVeiculosEntity>> Listar();
        Task<bool> PossuiModeloVeiculo(string nome, int id = 0);
    }
}
