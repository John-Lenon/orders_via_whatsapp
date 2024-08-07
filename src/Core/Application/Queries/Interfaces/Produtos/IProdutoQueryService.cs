using Application.Queries.DTO;
using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces.Base;

namespace Application.Queries.Interfaces
{
    public interface IProdutoQueryService : IQueryServiceBase<ProdutoFilterDTO, ProdutoQueryDTO>
    {
    }
}
