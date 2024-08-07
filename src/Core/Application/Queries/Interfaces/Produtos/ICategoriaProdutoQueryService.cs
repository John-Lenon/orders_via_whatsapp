using Application.Queries.DTO.Produtos;
using Application.Queries.Interfaces.Base;

namespace Application.Queries.Interfaces.Produtos
{
    public interface ICategoriaProdutoQueryService : IQueryServiceBase<CategoriaProdutoFilterDTO, CategoriaProdutoQueryDTO>
    {
    }
}
