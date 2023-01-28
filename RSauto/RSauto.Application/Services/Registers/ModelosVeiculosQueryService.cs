using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class ModelosVeiculosQueryService : IModelosVeiculosQueryService
    {
        private readonly IModelosVeiculosQueryRepository _modelosVeiculosQueryRepository;

        public ModelosVeiculosQueryService(IModelosVeiculosQueryRepository modelosVeiculosQueryRepository)
        {
            _modelosVeiculosQueryRepository = modelosVeiculosQueryRepository;
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _modelosVeiculosQueryRepository.Listar());
        }
    }
}
