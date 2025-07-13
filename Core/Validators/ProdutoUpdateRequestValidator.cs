using Core.Requests.Update;
using FluentValidation;

namespace Core.Validators
{
    public class ProdutoUpdateRequestValidator : AbstractValidator<ProdutoUpdateRequest>
    {

        public ProdutoUpdateRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O ID do produto é obrigatório.")
                .GreaterThan(0).WithMessage("O ID do produto deve ser maior que zero.");

        }

    }
}
