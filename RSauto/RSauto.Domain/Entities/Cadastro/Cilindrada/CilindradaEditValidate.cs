using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.Cilindrada
{
    public class CilindradaEditValidate: AbstractValidator<CilindradaVeiculosEntity>
    {
        public CilindradaEditValidate()
        {
            RuleFor(x => x.ID_CILINDRADA).NotEmpty().NotNull().WithMessage("Informe o id.").Must(x => x > 0);
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe descrição.").MinimumLength(3);
        }
    }
}
