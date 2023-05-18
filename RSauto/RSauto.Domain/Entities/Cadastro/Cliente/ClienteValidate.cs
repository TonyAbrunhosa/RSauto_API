using FluentValidation;
using RSauto.Domain.Entities.Cadastro.Cliente.Input;

namespace RSauto.Domain.Entities.Cadastro.Cliente
{
    public class ClienteValidate: AbstractValidator<ClienteInput>
    {
        public ClienteValidate()
        {
            When(x => string.IsNullOrEmpty(x.Nome), () => { RuleFor(x => x.RazaoSocial).NotEmpty().NotNull(); });
            When(x => string.IsNullOrEmpty(x.RazaoSocial), () => { RuleFor(x => x.Nome).NotEmpty().NotNull(); });
        }
    }
}
