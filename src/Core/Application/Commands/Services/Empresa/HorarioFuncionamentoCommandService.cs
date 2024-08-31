using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations.MappingsApp;
using Application.Resources.Messages;
using Domain.Entities.Empresa;
using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Services
{
    public class HorarioFuncionamentoCommandService(IServiceProvider serviceProvider)
        : CommandServiceBase<HorarioFuncionamento, HorarioFuncionamentoCommandDTO,
            IHorarioFuncionamentoRepository>(serviceProvider), IHorarioFuncionamentoCommandService
    {
        protected override HorarioFuncionamento MapToEntity(HorarioFuncionamentoCommandDTO entityDTO) =>
            entityDTO.MapToEntity();

        public async Task UpdateAsync(HorarioFuncionamentoCommandDTO entityDto, Guid codigo)
        {
            if (!Validator(entityDto)) return;

            var horarioFuncionamento = await _repository.Get()
                .FirstOrDefaultAsync(e => e.Codigo == codigo);

            if (horarioFuncionamento is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.HorarioFuncionamentoNaoEncontrado);

                return;
            }

            horarioFuncionamento.MapUpdateEntity(entityDto);

            _repository.Update(horarioFuncionamento);
            await _repository.SaveChangesAsync();
        }
    }
}
