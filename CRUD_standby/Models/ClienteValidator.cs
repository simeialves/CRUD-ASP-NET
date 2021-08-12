using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_standby.Models
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Razao_social)
                .NotNull().WithMessage("Digite a Razão Social");

            RuleFor(x => x.Cnpj)
                .NotNull().WithMessage("Digite o CNPJ");

            RuleFor(x => x.Data_fundacao)
                .NotNull().WithMessage("Digite a Data de Fundação");

            RuleFor(x => x.Capital)
                .NotNull().WithMessage("Digite o Capital");
        }
    }
}
