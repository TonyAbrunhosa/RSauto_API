using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IModelosVeiculosRepository
    {
        Task Novo(ModelosVeiculosEntity entity);
        Task Atualizar(ModelosVeiculosEntity entity);
        Task Remover(ModelosVeiculosEntity entity);
    }
}
