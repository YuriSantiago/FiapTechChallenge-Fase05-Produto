using FiapTechChallenge.Core.Requests.Update;
using FluentValidation;

namespace FiapTechChallenge.Core.Validators
{
    public class RegiaoUpdateRequestValidator : AbstractValidator<RegiaoUpdateRequest>
    {

        public RegiaoUpdateRequestValidator()
        {
            RuleFor(request => request.Id)
               .GreaterThan(0).WithMessage("O Id deve ser maior que 0.");

            RuleFor(regiao => regiao.DDD)
             .Must(ddd => ddd >= 10 && ddd <= 99).WithMessage("O DDD deve ter exatamente 2 dígitos.");

            RuleFor(c => c.Descricao)
                .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Descricao));
        }

    }
}
