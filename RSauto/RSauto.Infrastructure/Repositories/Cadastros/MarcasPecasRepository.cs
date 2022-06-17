using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class MarcasPecasRepository : IMarcasPecasRepository
    {
        private readonly SqlCommunication _sql;

        public MarcasPecasRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task Atualizar(MarcasPecasEntity entity)
        {
            await _sql.UpdateAsyncDapper(entity);
        }

        public async Task Novo(MarcasPecasEntity entity)
        {
            await _sql.InsertAsyncDapper(entity);
        }

        public async Task Remover(MarcasPecasEntity entity)
        {
            await _sql.RemoveAsyncDapper(entity);
        }
    }
}
