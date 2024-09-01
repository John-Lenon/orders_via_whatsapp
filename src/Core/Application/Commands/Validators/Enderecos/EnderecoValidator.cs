using Application.Commands.DTO.Enderecos;
using FluentValidation;

namespace Application.Commands.Validators.Enderecos
{
    public class EnderecoValidator : AbstractValidator<EnderecoCommandDTO>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.Cep)
                .Length(8).WithMessage("O CEP deve conter exatamente {MaxLength} caracteres")
                .NotEmpty().WithMessage("O campo CEP é obrigatório");

            RuleFor(x => x.Uf)
                .Length(2).WithMessage("A UF deve conter {MaxLength} caracteres")
                .NotEmpty().WithMessage("A unidade federativa deve ser informada");

            RuleFor(x => x.Bairro)
                .MinimumLength(1).WithMessage("O bairro deve ter no mínimo {MinLength} caractere")
                .NotEmpty().WithMessage("O bairro deve ser informado");

            RuleFor(x => x.Cidade)
                .MinimumLength(1).WithMessage("A cidade deve conter no mínimo {MinLength} caractere")
                .NotEmpty().WithMessage("A cidade deve ser informada");

            RuleFor(x => x.NumeroLogradouro)
                .NotEmpty().WithMessage("O número do logradouro deve ser informado");

            RuleFor(x => x.Logradouro)
                .MinimumLength(1).WithMessage("O logradouro deve conter no mínimo {MinLength} caractere")
                .NotEmpty().WithMessage("O logradouro deve ser informado");
        }
    }
}
