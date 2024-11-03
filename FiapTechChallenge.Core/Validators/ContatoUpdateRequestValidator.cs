using FiapTechChallenge.Core.Requests.Update;
using FluentValidation;

namespace FiapTechChallenge.Core.Validators
{
    public class ContatoUpdateRequestValidator : AbstractValidator<ContatoUpdateRequest>
    {
        public ContatoUpdateRequestValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0).WithMessage("O ID deve ser maior que 0.");

            RuleFor(c => c.Nome)
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Nome));

            RuleFor(c => c.DDD)
                .Must(ddd => ddd >= 10 && ddd <= 99).WithMessage("O DDD deve ter 2 dígitos.")
                .When(c => c.DDD > 0);

            RuleFor(c => c.Telefone)
                .Matches(@"^\d{9}$").WithMessage("O telefone deve ser um número válido.")
                .When(c => !string.IsNullOrEmpty(c.Telefone)); 

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("O formato do e-mail é inválido.")
                .When(c => !string.IsNullOrEmpty(c.Email)); 
        }
    }
}
