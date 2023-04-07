using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.Cilindrada;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Registers
{
    public class CilindradaVeiculosService : ICilindradaVeiculosService
    {
        private readonly IBaseCrudRepository _baseCrudRepository;
        private readonly ICilindradaVeiculosRepository _repository;
        private readonly CilindradaNewValidate _validateNew;
        private readonly CilindradaEditValidate _validateEdit;

        public CilindradaVeiculosService(IBaseCrudRepository baseCrudRepository, ICilindradaVeiculosRepository repository, CilindradaNewValidate validateNew, CilindradaEditValidate validateEdit)
        {
            _baseCrudRepository = baseCrudRepository;
            _repository = repository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
        }

        public async Task<ICommandResult> Create(string nome)
        {
            var retorno = _validateNew.Validate(new CilindradaVeiculosEntity { DESCRICAO = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _repository.PossuiCilindrada(nome))
                return new CommandResult(false, "Já possui uma Cilindrada com a descrição informada");

            await _baseCrudRepository.Create(new CilindradaVeiculosEntity { ID_CILINDRADA = 0, DESCRICAO = nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _repository.Listar());
        }

        public async Task<ICommandResult> Update(CilindradaVeiculosEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _repository.PossuiCilindrada(entity.DESCRICAO, entity.ID_CILINDRADA))
                return new CommandResult(false, "Já possui uma Cilindrada com a descrição informada");

            await _baseCrudRepository.Update(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }
    }
}
