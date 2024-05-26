using Domain.DTOs;
using FluentValidation;

namespace Application.Commands.Validators
{
    public class ValidateUsuario : AbstractValidator<UsuarioCommandDTO>
    {
        public ValidateUsuario()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O {PropertyName} é obrigatório.")
                .Length(5, 80)
                .WithMessage(
                    "O {PropertyName} deve ter entre {MinLength} a {MaxLength} caracteres."
                );

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O {PropertyName} é obrigatório.")
                .EmailAddress()
                .WithMessage("O {PropertyName} deve ser válido.")
                .Length(3, 80)
                .WithMessage(
                    "O {PropertyName} deve ter entre {MinLength} a {MaxLength} caracteres."
                );

            RuleFor(x => x.Telefone)
                .NotEmpty()
                .WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
                .WithMessage("O telefone deve ser um número válido no formato (XX) XXXXX-XXXX.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A {PropertyName} é obrigatória.")
                .Matches("[A-Z]")
                .WithMessage("A {PropertyName} deve conter ao menos uma letra maiúscula.")
                .Matches("[0-9]")
                .WithMessage("A {PropertyName} deve conter ao menos um número.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("A {PropertyName} deve conter ao menos um caractere especial.")
                .Length(4, 25)
                .WithMessage(
                    "O {PropertyName} deve ter entre {MinLength} a {MaxLength} caracteres."
                );
        }
    }
}
