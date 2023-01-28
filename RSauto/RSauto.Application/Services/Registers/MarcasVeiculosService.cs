using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.MarcasVeiculos;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class MarcasVeiculosService : IMarcasVeiculosService
    {
        private readonly IBaseCrudRepository _baseCrudRepository;
        private readonly IMarcasVeiculosQueryRepository _marcasVeiculosQueryRepository;
        private readonly MarcasVeiculosNewValidate _validateNew;
        private readonly MarcasVeiculosEditValidate _validateEdit;

        public MarcasVeiculosService(IBaseCrudRepository marcasVeiculosRepository, IMarcasVeiculosQueryRepository marcasVeiculosQueryRepository, MarcasVeiculosNewValidate validateNew, MarcasVeiculosEditValidate validateEdit)
        {
            _baseCrudRepository = marcasVeiculosRepository;
            _marcasVeiculosQueryRepository = marcasVeiculosQueryRepository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
        }

        public async Task<ICommandResult> Update(MarcasVeiculosEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasVeiculosQueryRepository.PossuiMarcaVeiculo(entity.NOME, entity.ID_MARCA))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Update(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Insert(string nome)
        {
            var retorno = _validateNew.Validate(new MarcasVeiculosEntity { NOME = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasVeiculosQueryRepository.PossuiMarcaVeiculo(nome))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Insert(new MarcasVeiculosEntity { ID_MARCA=0, NOME=nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remove(int id)
        {
            await _baseCrudRepository.Remove(new MarcasVeiculosEntity { ID_MARCA = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
    }
}
