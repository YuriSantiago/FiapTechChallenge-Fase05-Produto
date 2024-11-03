using FiapTechChallenge.Core.Requests.Create;
using FluentValidation;

namespace FiapTechChallenge.Core.Validators
{
    public class RegiaoRequestValidator : AbstractValidator<RegiaoRequest>
    {

        public RegiaoRequestValidator()
        {
            RuleFor(regiao => regiao.DDD)
               .NotEmpty().WithMessage("O DDD é obrigatório.")
               .Must(ddd => ddd >= 10 && ddd <= 99).WithMessage("O DDD deve ter exatamente 2 dígitos.");

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");
        }

    }
}
