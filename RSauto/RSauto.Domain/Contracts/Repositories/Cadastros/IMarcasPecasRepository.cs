using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Cadastros
{
    public interface IMarcasPecasRepository
    {
        Task Novo(MarcasPecasEntity entity);
        Task Atualizar(MarcasPecasEntity entity);
        Task Remover(MarcasPecasEntity entity);
    }
}
