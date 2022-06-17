using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Contracts.Services.Cadastros;
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
        private readonly IModelosVeiculosRepository _modelosVeiculosRepository;
        private readonly IModelosVeiculosQueryRepository _modelosVeiculosQueryRepository;
        private readonly ModelosVeiculosValidate _valida;

        public ModelosVeiculosService(IModelosVeiculosRepository modelosVeiculosRepository, IModelosVeiculosQueryRepository modelosVeiculosQueryRepository, ModelosVeiculosValidate valida)
        {
            _modelosVeiculosRepository = modelosVeiculosRepository;
            _modelosVeiculosQueryRepository = modelosVeiculosQueryRepository;
            _valida = valida;
        }

        public async Task<ICommandResult> Atualizar(int id, ModelosVeiculosInput input)
        {
            var retorno = _valida.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _modelosVeiculosQueryRepository.PossuiModeloVeiculo(input.NOME, input.ID_MARCA))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _modelosVeiculosRepository.Atualizar(new ModelosVeiculosEntity { ID_MODELO = id, NOME = input.NOME, ID_MARCA = input.ID_MARCA  });
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Novo(ModelosVeiculosInput input)
        {
            var retorno = _valida.Validate(input);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _modelosVeiculosQueryRepository.PossuiModeloVeiculo(input.NOME, input.ID_MARCA))
                return new CommandResult(false, "Já possui um modelo com a descrição informada");

            await _modelosVeiculosRepository.Novo(new ModelosVeiculosEntity { NOME = input.NOME, ID_MARCA = input.ID_MARCA });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remover(int id)
        {
            await _modelosVeiculosRepository.Remover(new ModelosVeiculosEntity { ID_MODELO = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
    }
}
