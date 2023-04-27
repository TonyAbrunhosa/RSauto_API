using FluentValidation;
using RSauto.Domain.Entities;

namespace RSauto.Domain.Entities.Cadastro.MarcasVeiculos
{
    public class MarcasVeiculosEditValidate : AbstractValidator<MarcasVeiculosEntity>
    {
        public MarcasVeiculosEditValidate()
        {
            RuleFor(x => x.ID_MARCA).NotEmpty().NotNull().WithMessage("Informe o id.").Must(x => x > 0);
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o nome da marca.").MinimumLength(3);
        }
    }
}
