using FluentValidation;

namespace RSauto.Domain.Entities.Cadastro.MarcasPecas
{
    public class MarcasPecasNewValidate: AbstractValidator<MarcasPecasEntity>
    {
        public MarcasPecasNewValidate()
        {
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o nome da marca da peça.").MinimumLength(3);
        }
    }
}
