using FluentValidation;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;

namespace RSauto.Domain.Entities.Cadastro.ModelosVeiculos
{
    public class ModelosVeiculosValidate : AbstractValidator<ModelosVeiculosInput>
    {
        public ModelosVeiculosValidate()
        {
            RuleFor(x => x.NOME).NotEmpty().NotNull().WithMessage("Informe o nome do modelo.").MinimumLength(3);
            RuleFor(x => x.ID_MARCA).NotEmpty().NotNull().WithMessage("Informe o id modelo.").Must(x => x > 0);
        }
    }
}
