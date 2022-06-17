using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.MarcasPecas
{
    public class MarcasPecasEditValidate: AbstractValidator<MarcasPecasEntity>
    {
        public MarcasPecasEditValidate()
        {
            RuleFor(x => x.ID_MARCA_PECAS).NotEmpty().NotNull().WithMessage("Informe o id.").Must(x => x > 0);
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o nome da marca da peças.").MinimumLength(3);
        }
    }
}
