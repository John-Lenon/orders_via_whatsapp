using Domain.Entities.Produtos;
using Domain.Interfaces.Repositories.Produtos;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories
{
    public class ProdutoRepository(IServiceProvider serviceProvider) :
        RepositorioBase<Produto, OrderViaWhatsAppContext>(serviceProvider),
            IProdutoRepository
    {
    }
}
