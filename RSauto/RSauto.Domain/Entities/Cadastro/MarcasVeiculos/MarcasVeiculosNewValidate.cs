using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.MarcasVeiculos
{
    public class MarcasVeiculosNewValidate : AbstractValidator<MarcasVeiculosEntity>
    {
        public MarcasVeiculosNewValidate()
        {
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o nome da marca.").MinimumLength(3);
        }
    }
}
