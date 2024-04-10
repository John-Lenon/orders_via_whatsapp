using Domain.DTOs.Usuario;
using FluentValidation;

namespace Application.Validators.Usuarios
{
    public class ValidateUsuarioDto : AbstractValidator<UsuarioDto>
    {
        public ValidateUsuarioDto()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O email é obrigatório.")
                .EmailAddress()
                .WithMessage("O email deve ser válido.")
                .Length(3, 25)
                .WithMessage("O email deve ter entre 3 a 25 caracteres.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A senha é obrigatória.")
                .Length(3, 25)
                .WithMessage("O senha deve ter entre 4 a 25 caracteres.")
                .Matches("[A-Z]")
                .WithMessage("A senha deve conter ao menos uma letra maiúscula.")
                .Matches("[0-9]")
                .WithMessage("A senha deve conter ao menos um número.")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage("A senha deve conter ao menos um caractere especial.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("O nome é obrigatório.")
                .Length(5, 80)
                .WithMessage("O nome deve ter entre 5 a 80 caracteres.");

            RuleFor(x => x.Telefone)
                .NotEmpty()
                .WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
                .WithMessage("O telefone deve ser um número válido no formato (XX) XXXXX-XXXX.");
        }
    }
}
