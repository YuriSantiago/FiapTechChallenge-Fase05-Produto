using FiapTechChallenge.Core.Requests.Create;
using FluentValidation;

namespace FiapTechChallenge.Core.Validators
{
    public class ContatoRequestValidator : AbstractValidator<ContatoRequest>
    {

        public ContatoRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

            RuleFor(c => c.DDD)
               .NotEmpty().WithMessage("O DDD é obrigatório.")
               .Must(ddd => ddd >= 10 && ddd <= 99).WithMessage("O DDD deve ter 2 dígitos.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{9}$").WithMessage("O telefone deve ser um número válido.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail é inválido.");
        }

    }
}
