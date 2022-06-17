using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class MarcasVeiculosRepository : IMarcasVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public MarcasVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task Atualizar(MarcasVeiculosEntity entity)
        {
            await _sql.UpdateAsyncDapper(entity);
        }

        public async Task Novo(MarcasVeiculosEntity entity)
        {
            await _sql.InsertAsyncDapper(entity);
        }

        public async Task Remover(MarcasVeiculosEntity entity)
        {
            await _sql.RemoveAsyncDapper(entity);
        }
    }
}
