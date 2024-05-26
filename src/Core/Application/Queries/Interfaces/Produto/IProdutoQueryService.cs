using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces.Base;

namespace Application.Queries.Interfaces.Produto
{
    public interface IProdutoQueryService : IQueryServiceBase<ProdutoFilterDTO, ProdutoQueryDTO>
    {
    }
}
