using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class AnoModeloVeiculoRepository : IAnoModeloVeiculoRepository
    {
        private readonly SqlCommunication _sql;

        public AnoModeloVeiculoRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task Atualizar(AnoModeloVeiculoEntity entity)
        {
            await _sql.UpdateAsyncDapper(entity);
        }

        public async Task Novo(AnoModeloVeiculoEntity entity)
        {
            await _sql.InsertAsyncDapper(entity);
        }

        public async Task Remover(AnoModeloVeiculoEntity entity)
        {
            await _sql.RemoveAsyncDapper(entity);
        }
    }
}
