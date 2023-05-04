using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class ModelosVeiculosService : IModelosVeiculosService
    {
        private readonly IBaseCrudRepository _baseCrudRepository;
        private readonly IModelosVeiculosRepository _modelosVeiculosRepository;
        private readonly ModelosVeiculosValidate _valida;

        public ModelosVeiculosService(IBaseCrudRepository baseCrudRepository, IModelosVeiculosRepository modelosVeiculosRepository, ModelosVeiculosValidate valida)
        {
            _baseCrudRepository = baseCrudRepository;
            _modelosVeiculosRepository = modelosVeiculosRepository;
            _valida = valida;
        }

        public async Task<ICommandResult> Update(int id, ModelosVeiculosInput input)
        {
            var retorno = _valida.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _modelosVeiculosRepository.PossuiModeloVeiculo(input.DESCRICAO, input.ID_MARCA))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Update(new ModelosVeiculosEntity { ID_MODELO = id, DESCRICAO = input.DESCRICAO, ID_MARCA = input.ID_MARCA  });
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Create(ModelosVeiculosInput input)
        {
            var retorno = _valida.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _modelosVeiculosRepository.PossuiModeloVeiculo(input.DESCRICAO, input.ID_MARCA))
                return new CommandResult(false, "Já possui um modelo com a descrição informada");

            await _baseCrudRepository.Create(new ModelosVeiculosEntity { DESCRICAO = input.DESCRICAO, ID_MARCA = input.ID_MARCA });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remove(int id)
        {
            await _baseCrudRepository.Remove(new ModelosVeiculosEntity { ID_MODELO = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }

        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _modelosVeiculosRepository.Listar());
        }
    }
}
