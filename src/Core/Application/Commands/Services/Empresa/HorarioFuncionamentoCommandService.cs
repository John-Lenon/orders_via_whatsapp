using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Utilities;
using Domain.Entities.Empresas;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Services
{
    public class HorarioFuncionamentoCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<HorarioFuncionamento, HorarioFuncionamentoCommandDTO,
            IHorarioFuncionamentoRepository>(serviceProvider), IHorarioFuncionamentoCommandService
    {
        public override async Task UpdateAsync(HorarioFuncionamentoCommandDTO entityDto, Guid? codigo, bool saveChanges = true)
        {
            if (!Validator(entityDto)) return;

            var horarioFuncionamento = await _repository.Get()
                .FirstOrDefaultAsync(e => e.Codigo == codigo);

            if (horarioFuncionamento is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente,
                    "Horário de funcionamento não foi encontrada.");

                return;
            }

            horarioFuncionamento.GetValuesFrom(entityDto);
            _repository.Update(horarioFuncionamento);
            await _repository.SaveChangesAsync();
        }
    }
}
