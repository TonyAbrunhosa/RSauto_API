using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Registers
{
    public class CilindradaVeiculosService : ICilindradaVeiculosService
    {
        public Task<ICommandResult> Insert(string nome)
        {
            throw new NotImplementedException();
        }        

        public Task<ICommandResult> Update(CilindradaVeiculosEntity request)
        {
            throw new NotImplementedException();
        }

        public Task<ICommandResult> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
