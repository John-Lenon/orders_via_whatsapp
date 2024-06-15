using Application.Configurations.MappingsApp;
using Application.Queries.DTO;
using Application.Queries.Interfaces;
using Application.Queries.Services.Base;
using Domain.Entities.Empresa;
using Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Application.Queries.Services
{
    public class HorarioFuncionamentoQueryService(IServiceProvider service) :
        QueryServiceBase<IHorarioFuncionamentoRepository, HorarioFuncionamentoFilterDTO,
            HorarioFuncionamentoQueryDTO, HorarioFuncionamento>(service), IHorarioFuncionamentoQueryService
    {
        protected override HorarioFuncionamentoQueryDTO MapToDTO(HorarioFuncionamento entity) => entity.MapToDTO();

        protected override Expression<Func<HorarioFuncionamento, bool>> GetFilterExpression(HorarioFuncionamentoFilterDTO filter)
        {
            return empresa =>
                   (filter.Codigo == null || empresa.Codigo == filter.Codigo)
                && (filter.Hora == 0 || empresa.Hora == filter.Hora)
                && (filter.Minutos == 0 || empresa.Minutos == filter.Minutos)
                && (filter.DiaDaSemana == 0 || empresa.DiaDaSemana == filter.DiaDaSemana);
        }
    }
}