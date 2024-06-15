using Application.Configurations.MappingsApp;
using Application.Queries.DTO;
using Application.Queries.Interfaces;
using Application.Queries.Services.Base;
using Domain.Entities.Empresa;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Queries.Services
{
    public class EmpresaQueryService(IServiceProvider service) :
        QueryServiceBase<IEmpresaRepository, EmpresaFilterDTO, EmpresaQueryDTO, Empresa>(service),
        IEmpresaQueryService
    {
        protected override EmpresaQueryDTO MapToDTO(Empresa entity) => entity.MapToDTO();

        public override async Task<IEnumerable<EmpresaQueryDTO>> GetAsync(EmpresaFilterDTO filter)
        {
            var listResult = await _repository.Get(GetFilterExpression(filter))
                .Include(empresa => empresa.HorariosDeFuncionamento).ToListAsync();

            return listResult.Select(MapToDTO);
        }

        protected override Expression<Func<Empresa, bool>> GetFilterExpression(EmpresaFilterDTO filter)
        {
            return empresa =>
                   (filter.Codigo == null || empresa.Codigo == filter.Codigo)
                && (filter.Cnpj == null || empresa.Cnpj == filter.Cnpj)
                && (filter.Email == null || empresa.Email == filter.Email)
                && (filter.NomeFantasia == null || empresa.NomeFantasia == filter.NomeFantasia)
                && (filter.RazaoSocial == null || empresa.RazaoSocial == filter.RazaoSocial);

        }

    }
}
