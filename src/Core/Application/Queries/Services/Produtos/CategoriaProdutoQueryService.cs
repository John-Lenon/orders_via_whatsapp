using Application.Queries.DTO.Produtos;
using Application.Queries.Interfaces.Produtos;
using Application.Queries.Services.Base;
using Domain.Entities.Produtos;
using Domain.Interfaces.Repositories.Produtos;
using System.Linq.Expressions;

namespace Application.Queries.Services.Produtos
{
    public class CategoriaProdutoQueryService(IServiceProvider service) :
        QueryServiceBase<ICategoriaProdutoRepository, CategoriaProdutoFilterDTO, CategoriaProdutoQueryDTO, CategoriaProduto>(service),
        ICategoriaProdutoQueryService
    {
        protected override Expression<Func<CategoriaProduto, bool>> GetFilterExpression(CategoriaProdutoFilterDTO filter)
        {
            return produto =>
                   (filter.Codigo == null || produto.Codigo == filter.Codigo)
                && (filter.Nome == null || produto.Nome == filter.Nome);
        }
    }
}
