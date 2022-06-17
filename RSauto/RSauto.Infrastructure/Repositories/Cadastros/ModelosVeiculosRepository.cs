using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class ModelosVeiculosRepository : IModelosVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public ModelosVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task Atualizar(ModelosVeiculosEntity entity)
        {
            await _sql.UpdateAsyncDapper(entity);
        }

        public async Task Novo(ModelosVeiculosEntity entity)
        {
            await _sql.InsertAsyncDapper(entity);
        }

        public async Task Remover(ModelosVeiculosEntity entity)
        {
            await _sql.RemoveAsyncDapper(entity);
        }
    }
}
