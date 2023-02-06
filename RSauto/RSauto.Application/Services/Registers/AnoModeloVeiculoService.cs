using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Registers;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class AnoModeloVeiculoService : IAnoModeloVeiculoService
    {
        private readonly IBaseCrudRepository _baseCrudRepository;
        private readonly IAnoModeloVeiculoQueryRepository _anoModeloVeiculoQueryRepository;
        private readonly AnoModeloVeiculoNewValidate _validateNew;
        private readonly AnoModeloVeiculoEditValidate _validateEdit;

        public AnoModeloVeiculoService(IAnoModeloVeiculoQueryRepository anoModeloVeiculoQueryRepository, AnoModeloVeiculoNewValidate validateNew, AnoModeloVeiculoEditValidate validateEdit, IBaseCrudRepository baseCrudRepository)
        {
            _anoModeloVeiculoQueryRepository = anoModeloVeiculoQueryRepository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
            _baseCrudRepository = baseCrudRepository;
        }

        public async Task<ICommandResult> Update(AnoModeloVeiculoEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _anoModeloVeiculoQueryRepository.PossuiMarcaPeca(entity.DESCRICAO, entity.ID_ANO_MOD_VEIC))
                return new CommandResult(false, "Já possui o ano/modelo informado.");

            await _baseCrudRepository.Update(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Insert(string nome)
        {
            var retorno = _validateNew.Validate(new AnoModeloVeiculoEntity { DESCRICAO = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _anoModeloVeiculoQueryRepository.PossuiMarcaPeca(nome))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _baseCrudRepository.Insert(new AnoModeloVeiculoEntity { ID_ANO_MOD_VEIC = 0, DESCRICAO = nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remove(int id)
        {
            await _baseCrudRepository.Remove(new AnoModeloVeiculoEntity { ID_ANO_MOD_VEIC = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
    }
}
