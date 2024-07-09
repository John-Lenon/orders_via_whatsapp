using Application.Commands.DTO;
using Application.Commands.Interfaces.Base;

namespace Application.Commands.Interfaces
{
    public interface IHorarioFuncionamentoCommandService : ICommandServiceBase<HorarioFuncionamentoCommandDTO>
    {
        Task UpdateAsync(HorarioFuncionamentoCommandDTO entityDto, Guid codigo);
    }
}
