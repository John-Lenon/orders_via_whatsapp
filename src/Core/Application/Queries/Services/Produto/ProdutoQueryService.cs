using Application.Extensions.Mappers;
using Application.Queries.DTO.Produto;
using Application.Queries.Interfaces.Produto;
using Application.Queries.Services.Base;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Application.Queries.Services
{
    public class ProdutoQueryService(IServiceProvider service) :
        QueryServiceBase<IProdutoRepository, ProdutoFilterDTO, ProdutoQueryDTO, Produto>(service),
        IProdutoQueryService
    {
        protected override ProdutoQueryDTO MapToDTO(Produto entity) => entity.MapToDTO();

        protected override Expression<Func<Produto, bool>> GetFilterExpression(ProdutoFilterDTO filter)
        {
            return produto =>
                   ((filter.Codigo == null) || produto.Codigo == filter.Codigo)
                && ((filter.Ativo == null) || produto.Ativo == filter.Ativo)
                && ((filter.Preco == null) || produto.Preco == filter.Preco)
                && ((filter.Nome == null) || produto.Nome == filter.Nome);
        }

    }
}
