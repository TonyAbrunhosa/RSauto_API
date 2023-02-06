using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class MarcasVeiculosQueryService : IMarcasVeiculosQueryService
    {
        private readonly IMarcasVeiculosQueryRepository _marcasVeiculosQueryRepositorio;

        public MarcasVeiculosQueryService(IMarcasVeiculosQueryRepository marcasVeiculosQueryRepositorio)
        {
            _marcasVeiculosQueryRepositorio = marcasVeiculosQueryRepositorio;
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _marcasVeiculosQueryRepositorio.Listar());
        }
    }
}
