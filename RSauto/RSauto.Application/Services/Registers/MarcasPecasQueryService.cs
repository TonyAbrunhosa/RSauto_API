using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class MarcasPecasQueryService : IMarcasPecasQueryService
    {
        private readonly IMarcasPecasQueryRepository _marcasPecasQueryRepository;

        public MarcasPecasQueryService(IMarcasPecasQueryRepository marcasPecasQueryRepository)
        {
            _marcasPecasQueryRepository = marcasPecasQueryRepository;
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _marcasPecasQueryRepository.Listar());
        }
    }
}
