using Application.Queries.DTO.Produto;
using Application.Queries.Services.Base;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Application.Queries.Services
{
    public class ProdutoQueryService : QueryServiceBase<IProdutoRepository, ProdutoQueryDTO, Produto>
    {
        public ProdutoQueryService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override Expression<Func<Produto, bool>> GetFilterExpression(ProdutoQueryDTO filter)
        {
            return produto =>
                   ((filter.Codigo == null) || produto.Codigo == filter.Codigo)
                && ((filter.Ativo == null) || produto.Ativo == filter.Ativo)
                && ((filter.Preco == null) || produto.Preco == filter.Preco)
                && ((filter.Nome == null) || produto.Nome == filter.Nome);
        }
    }
}
