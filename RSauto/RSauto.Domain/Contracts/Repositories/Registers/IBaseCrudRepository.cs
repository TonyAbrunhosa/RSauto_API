using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Repositories.Registers
{
    public interface IBaseCrudRepository
    {
        Task Create<T>(T entity) where T : class;
        Task Remove<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
    }
}
