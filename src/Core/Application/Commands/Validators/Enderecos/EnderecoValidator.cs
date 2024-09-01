using Application.Commands.DTO.Enderecos;
using FluentValidation;

namespace Application.Commands.Validators.Enderecos
{
    public class EnderecoValidator : AbstractValidator<EnderecoCommandDTO>
    {
        public EnderecoValidator()
        {
            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("A unidade federativa deve ser informada");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O Bairro deve ser informado");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A cidade deve ser informada");

            RuleFor(x => x.NumeroLogradouro)
                .NotEmpty().WithMessage("O número do logradouro deve ser informado");

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("O logradouro deve ser informado");
        }
    }
}
