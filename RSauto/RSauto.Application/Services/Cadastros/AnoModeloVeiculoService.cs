using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Repositories.Cadastros;
using RSauto.Domain.Contracts.Services.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo;
using RSauto.Domain.Entities.Command;
using RSauto.Shared.Utilities;
using System.Threading.Tasks;

namespace RSauto.Application.Services.Cadastros
{
    public class AnoModeloVeiculoService : IAnoModeloVeiculoService
    {
        private readonly IAnoModeloVeiculoRepository _anoModeloVeiculoRepository;
        private readonly IAnoModeloVeiculoQueryRepository _anoModeloVeiculoQueryRepository;
        private readonly AnoModeloVeiculoNewValidate _validateNew;
        private readonly AnoModeloVeiculoEditValidate _validateEdit;

        public AnoModeloVeiculoService(IAnoModeloVeiculoRepository anoModeloVeiculoRepository, IAnoModeloVeiculoQueryRepository anoModeloVeiculoQueryRepository, AnoModeloVeiculoNewValidate validateNew, AnoModeloVeiculoEditValidate validateEdit)
        {
            _anoModeloVeiculoRepository = anoModeloVeiculoRepository;
            _anoModeloVeiculoQueryRepository = anoModeloVeiculoQueryRepository;
            _validateNew = validateNew;
            _validateEdit = validateEdit;
        }

        public async Task<ICommandResult> Atualizar(AnoModeloVeiculoEntity entity)
        {
            var retorno = _validateEdit.Validate(entity);
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _anoModeloVeiculoQueryRepository.PossuiMarcaPeca(entity.DESCRICAO, entity.ID_ANO_MOD_VEIC))
                return new CommandResult(false, "Já possui o ano/modelo informado.");

            await _anoModeloVeiculoRepository.Atualizar(entity);
            return new CommandResult(true, "Cadastro atualizado com sucesso.");
        }

        public async Task<ICommandResult> Novo(string nome)
        {
            var retorno = _validateNew.Validate(new AnoModeloVeiculoEntity { DESCRICAO = nome });
            if (!retorno.IsValid)
                return new CommandResult(false, "Atenção", ReturnErrors.CreateObjetError(retorno.Errors));

            if (await _anoModeloVeiculoQueryRepository.PossuiMarcaPeca(nome))
                return new CommandResult(false, "Já possui uma marca com a descrição informada");

            await _anoModeloVeiculoRepository.Novo(new AnoModeloVeiculoEntity { ID_ANO_MOD_VEIC = 0, DESCRICAO = nome });
            return new CommandResult(true, "Cadastro realizado com sucesso.");
        }

        public async Task<ICommandResult> Remover(int id)
        {
            await _anoModeloVeiculoRepository.Remover(new AnoModeloVeiculoEntity { ID_ANO_MOD_VEIC = id });
            return new CommandResult(true, "Cadastro removido com sucesso.");
        }
    }
}
