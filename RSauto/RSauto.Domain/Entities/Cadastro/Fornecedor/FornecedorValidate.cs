using FluentValidation;
using RSauto.Domain.Entities.Cadastro.Fornecedor.Input;

namespace RSauto.Domain.Entities.Cadastro.Fornecedor
{
    public class FornecedorValidate: AbstractValidator<FornecedorInput>
    {
        public FornecedorValidate()
        {
            When(x => string.IsNullOrEmpty(x.Nome), () => { RuleFor(x => x.RazaoSocial).NotEmpty().NotNull(); });
            When(x => string.IsNullOrEmpty(x.RazaoSocial), () => { RuleFor(x => x.Nome).NotEmpty().NotNull(); });
        }
    }
}
