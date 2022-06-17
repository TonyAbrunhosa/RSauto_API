using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSauto.Domain.Entities.Cadastro.AnoModeloVeiculo
{
    public class AnoModeloVeiculoNewValidate: AbstractValidator<AnoModeloVeiculoEntity>
    {
        public AnoModeloVeiculoNewValidate()
        {
            RuleFor(x => x.DESCRICAO).NotEmpty().NotNull().WithMessage("Informe o ano modelo.").MinimumLength(3);
        }
    }
}
