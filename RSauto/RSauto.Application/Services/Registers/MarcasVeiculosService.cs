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
        private readonly IMarcasVeiculosRepository _marcasVeiculosRepository;
        private readonly MarcasVeiculosNewValidate _validateNew;
        private readonly MarcasVeiculosEditValidate _validateEdit;

        public MarcasVeiculosService(IBaseCrudRepository baseCrudRepository, IMarcasVeiculosRepository marcasVeiculosRepository, MarcasVeiculosNewValidate validateNew, MarcasVeiculosEditValidate validateEdit)
        {
            _baseCrudRepository = baseCrudRepository;
            _marcasVeiculosRepository = marcasVeiculosRepository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
        }

        public async Task<ICommandResult> Update(MarcasVeiculosEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasVeiculosRepository.PossuiMarcaVeiculo(entity.DESCRICAO, entity.ID_MARCA))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Update(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Create(string nome)
        {
            var retorno = _validateNew.Validate(new MarcasVeiculosEntity { DESCRICAO = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasVeiculosRepository.PossuiMarcaVeiculo(nome))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Create(new MarcasVeiculosEntity { ID_MARCA=0, DESCRICAO =nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remove(int id)
        {
            await _baseCrudRepository.Remove(new MarcasVeiculosEntity { ID_MARCA = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
        public async Task<ICommandResult> Listar()
        {
            return new CommandResult(true, "Consulta realizado com sucesso", await _marcasVeiculosRepository.Listar());
        }
    }
}
