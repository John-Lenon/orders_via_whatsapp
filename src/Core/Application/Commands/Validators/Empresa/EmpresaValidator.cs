using Application.Commands.DTO;
using Application.Commands.Validators.Empresa;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Commands.Validators
{
    public class EmpresaValidator : AbstractValidator<EmpresaCommandDTO>
    {
        public EmpresaValidator()
        {
            RuleFor(x => x.NomeFantasia)
              .NotEmpty().WithMessage("Nome fantasia é obrigatório.")
              .Length(2, 100).WithMessage("Nome fantasia deve ter entre 2 e 100 caracteres.");

            RuleFor(x => x.RazaoSocial)
                .NotEmpty().WithMessage("Razão social é obrigatória.")
                .Length(2, 100).WithMessage("Razão social deve ter entre 2 e 100 caracteres.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório.")
                .Must(IsValidCnpj).WithMessage("CNPJ inválido.");

            RuleFor(x => x.NumeroDoWhatsapp)
                .NotEmpty().WithMessage("Número do WhatsApp é obrigatório.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Número do WhatsApp inválido.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Dominio)
             .NotEmpty().WithMessage("Domínio é obrigatório.")
             .Matches("^[a-zA-Z]+$").WithMessage("Domínio deve conter apenas letras.");

            RuleFor(x => x.StatusDeFuncionamento)
                .IsInEnum().WithMessage("Status de funcionamento inválido.");

            RuleForEach(x => x.HorariosDeFuncionamento)
                .SetValidator(new HorarioFuncionamentoValidator());
        }

        private bool IsValidCnpj(string cnpj)
        {
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            if (cnpj.Length != 14)
                return false;

            int[] multiplicador1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicador2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj += digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}
