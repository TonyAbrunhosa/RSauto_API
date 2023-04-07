using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Shared.Communication;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Cadastros
{
    public class BaseCrudRepository: IBaseCrudRepository
    {
        private readonly SqlCommunication _sql;

        public BaseCrudRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public async Task Update<T>(T entity) where T : class
        {
            await _sql.UpdateAsyncDapper<T>(entity);
        }

        public async Task Create<T>(T entity) where T : class 
        {
            await _sql.InsertAsyncDapper<T>(entity);
        }

        public async Task Remove<T>(T entity) where T : class
        {
            await _sql.RemoveAsyncDapper<T>(entity);
        }
    }
}
