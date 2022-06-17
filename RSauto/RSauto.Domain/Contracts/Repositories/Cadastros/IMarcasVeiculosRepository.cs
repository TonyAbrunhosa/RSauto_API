using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IMarcasVeiculosRepository
    {
        Task Novo(MarcasVeiculosEntity entity);
        Task Atualizar(MarcasVeiculosEntity entity);
        Task Remover(MarcasVeiculosEntity entity);
    }
}
