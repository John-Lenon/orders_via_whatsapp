using Application.Commands.DTO;
using FluentValidation;

namespace Application.Commands.Validators.Empresas
{
    public class HorarioFuncionamentoValidator : AbstractValidator<HorarioFuncionamentoCommandDTO>
    {
        public HorarioFuncionamentoValidator()
        {
            RuleFor(x => x.Hora)
                .InclusiveBetween(1, 23).WithMessage("Hora deve estar entre 1 e 23.");

            RuleFor(x => x.Minutos)
                .InclusiveBetween(0, 59).WithMessage("Minutos deve estar entre 0 e 59.");

            RuleFor(x => x.DiaDaSemana)
                .IsInEnum().WithMessage("Dia da semana inválido.");
        }
    }
}