using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.Cilindrada
{
    public class CilindradaNewValidate: AbstractValidator<CilindradaVeiculosEntity>
    {
        public CilindradaNewValidate()
        {
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe a descrição.").MinimumLength(3);
        }
    }
}
