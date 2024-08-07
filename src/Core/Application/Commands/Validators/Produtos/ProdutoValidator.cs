using Application.Commands.DTO.Produtos;
using FluentValidation;

namespace Application.Commands.Validators
{
    public class ProdutoValidator : AbstractValidator<ProdutoCommandDTO>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O {PropertyName} é obrigatório.")
                .Length(5, 80)
                .WithMessage(
                    "O {PropertyName} deve ter entre {MinLength} a {MaxLength} caracteres."
                );

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("O {PropertyName} é obrigatório.")
                .MaximumLength(100).WithMessage("O campo {PropertyName} deve possuir no máximo {MaxLength} caracteres.");

            RuleFor(x => x.CodigoCategoria)
                .NotEmpty().WithMessage("A categoria do produto deve ser informada.");
        }

    }
}
