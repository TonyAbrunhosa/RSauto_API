using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.MarcasPecas;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class MarcasPecasService: IMarcasPecasService
    {
        private readonly IBaseCrudRepository _baseCrudRepository;
        private readonly IMarcasPecasQueryRepository _marcasPecasQueryRepository;
        private readonly MarcasPecasNewValidate _validateNew;
        private readonly MarcasPecasEditValidate _validateEdit;

        public MarcasPecasService(IBaseCrudRepository baseCrudRepository, IMarcasPecasQueryRepository marcasPecasQueryRepository, MarcasPecasNewValidate validateNew, MarcasPecasEditValidate validateEdit)
        {
            _baseCrudRepository = baseCrudRepository;
            _marcasPecasQueryRepository = marcasPecasQueryRepository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
        }

        public async Task<ICommandResult> Update(MarcasPecasEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasPecasQueryRepository.PossuiMarcaPeca(entity.DESCRICAO, entity.ID_MARCA_PECAS))
                return new CommandResult(false, "Já possui uma marca de peca com a descrição informada");

            await _baseCrudRepository.Update(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Insert(string nome)
        {
            var retorno = _validateNew.Validate(new MarcasPecasEntity { DESCRICAO = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _marcasPecasQueryRepository.PossuiMarcaPeca(nome))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Insert(new MarcasPecasEntity { ID_MARCA_PECAS = 0, DESCRICAO = nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remove(int id)
        {
            await _baseCrudRepository.Remove(new MarcasPecasEntity { ID_MARCA_PECAS = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
    }
}
