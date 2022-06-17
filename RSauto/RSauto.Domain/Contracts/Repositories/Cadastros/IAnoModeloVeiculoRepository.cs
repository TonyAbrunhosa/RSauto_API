using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IAnoModeloVeiculoRepository
    {
        Task Novo(AnoModeloVeiculoEntity entity);
        Task Atualizar(AnoModeloVeiculoEntity entity);
        Task Remover(AnoModeloVeiculoEntity entity);
    }
}
