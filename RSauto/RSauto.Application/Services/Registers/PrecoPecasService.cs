using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Cadastro.PrecoPecas;
using RSauto.Domain.Entities.Cadastro.PrecoPecas.Input;
using RSauto.Domain.Entities.Command;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Registers
{
    public class PrecoPecasService : IPrecoPecasService
    {
        private readonly IPrecoPecasRepository _repository;

        public PrecoPecasService(IPrecoPecasRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResult> UpdateStatus(int idPrecoPeca, bool status)
        {
            if(await _repository.UpdateStatus(idPrecoPeca, status))
                return new CommandResult(true, "Cadastro atualizado com sucesso.");

            return new CommandResult(false, "Erro ao atualizar o cadastro.");
        }

        public async Task<CommandResult> Create(PrecoPecasCreateInput input)
        {
            await _repository.Create(input.PrecoPecasCreateEntity());

            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<CommandResult> Update(int id, PrecoPecasUpdateInput input)
        {
            await _repository.Update(input.PrecoPecasUpdateEntity(id));

            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }
    }
}
