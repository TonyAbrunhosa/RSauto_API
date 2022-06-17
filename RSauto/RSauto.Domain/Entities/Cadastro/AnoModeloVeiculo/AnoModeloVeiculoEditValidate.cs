using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo
{
    public class AnoModeloVeiculoEditValidate: AbstractValidator<AnoModeloVeiculoEntity>
    {
        public AnoModeloVeiculoEditValidate()
        {
            RuleFor(x => x.ID_ANO_MOD_VEIC).NotEmpty().NotNull().WithMessage("Informe o id.").Must(x => x > 0);
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o ano modelo.").MinimumLength(3);
        }
    }
}
