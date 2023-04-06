using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Entities;
using System.Threading.Tasks;

namespace RSauto.Domain.Contracts.Services.Registers
{
    public interface ICilindradaVeiculosService
    {
        Task<ICommandResult> Insert(string nome);
        Task<ICommandResult> Update(CilindradaVeiculosEntity request);
        Task<ICommandResult> Listar();
    }
}
