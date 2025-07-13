using Core.Requests.Delete;
using FluentValidation;

namespace Core.Validators
{
    public class ProdutoDeleteRequestValidator : AbstractValidator<ProdutoDeleteRequest>
    {

        public ProdutoDeleteRequestValidator()
        {
            RuleFor(x => x.Id)
                 .GreaterThan(0).WithMessage("O ID do produto deve ser maior que zero.");
        }


    }
}
