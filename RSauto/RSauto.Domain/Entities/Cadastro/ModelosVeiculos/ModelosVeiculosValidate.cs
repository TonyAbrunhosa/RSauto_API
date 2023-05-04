using FluentValidation;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;

namespace RSauto.Domain.Entities.Cadastro.ModelosVeiculos
{
    public class ModelosVeiculosValidate : AbstractValidator<ModelosVeiculosInput>
    {
        public ModelosVeiculosValidate()
        {
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o descrição do modelo do veiculo.").MinimumLength(3);
            RuleFor(x => x.ID_MARCA).NotEmpty().NotNull().WithMessage("Informe o id modelo.").Must(x => x > 0);
        }
    }
}
