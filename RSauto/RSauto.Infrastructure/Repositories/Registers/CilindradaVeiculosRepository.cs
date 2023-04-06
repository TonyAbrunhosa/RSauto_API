using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Entities;
using RSauto.Shared.Communication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.Infrastructure.Repositories.Registers
{
    public class CilindradaVeiculosRepository : ICilindradaVeiculosRepository
    {
        private readonly SqlCommunication _sql;

        public CilindradaVeiculosRepository(SqlCommunication sql)
        {
            _sql = sql;
        }

        public Task Insert<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CilindradaVeiculosEntity>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PossuiMarcaPeca(string nome, int id = 0)
        {
            throw new NotImplementedException();
        }

        public Task Remove<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
