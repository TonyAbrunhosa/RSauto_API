using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class AnoModeloVeiculoQueryService : IAnoModeloVeiculoQueryService
    {
        private readonly IAnoModeloVeiculoQueryRepository _anoModeloVeiculoQueryRepository;

        public AnoModeloVeiculoQueryService(IAnoModeloVeiculoQueryRepository anoModeloVeiculoQueryRepository)
        {
            _anoModeloVeiculoQueryRepository = anoModeloVeiculoQueryRepository;
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _anoModeloVeiculoQueryRepository.Listar());
        }
    }
}
