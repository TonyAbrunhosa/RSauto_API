using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface ICilindradaVeiculosService
    {
        Task<ICommandResult> Create(string name);
        Task<ICommandResult> Update(CilindradaVeiculosEntity entity);
        Task<ICommandResult> Listar();
    }
}
