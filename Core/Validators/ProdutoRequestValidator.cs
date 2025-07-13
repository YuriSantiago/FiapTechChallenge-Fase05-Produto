using Core.Requests.Create;
using FluentValidation;

namespace Core.Validators
{
    public class ProdutoRequestValidator : AbstractValidator<ProdutoRequest>
    {

        public ProdutoRequestValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres."); ;

            RuleFor(c => c.Descricao)
               .NotEmpty().WithMessage("A descrição é obrigatória.")
               .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

            RuleFor(c => c.Preco)
                .NotEmpty().WithMessage("O preço é obrigatório.")
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

            RuleFor(x => x.Disponivel)
                .NotNull().WithMessage("O campo 'Disponível' deve ser informado.");

            RuleFor(x => x.CategoriaId)
                .NotEmpty().WithMessage("O ID da categoria é obrigatório.")
                .GreaterThan(0).WithMessage("O ID da categoria deve ser maior que zero.");
        }

    }
}
