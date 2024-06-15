using Application.Queries.DTO;
using Application.Queries.Interfaces.Base;

namespace Application.Queries.Interfaces
{
    public interface IEmpresaQueryService : IQueryServiceBase<EmpresaFilterDTO, EmpresaQueryDTO>
    {
    }
}
