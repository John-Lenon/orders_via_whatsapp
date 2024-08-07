using Application.Commands.DTO.Produtos;
using FluentValidation;

namespace Application.Commands.Validators.Produtos
{
    public class CategoriaProdutoValidator : AbstractValidator<CategoriaProdutoCommandDTO>
    {
        public CategoriaProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O {PropertyName} é obrigatório.")
                .Length(5, 80)
                .WithMessage("O {PropertyName} deve ter entre {MinLength} a {MaxLength} caracteres.");

            RuleFor(x => x.Status)
                    .NotEmpty().WithMessage("O {PropertyName} é obrigatório.");
        }
    }
}
