using Domain.Entities.Produtos;
using Domain.Interfaces.Repositories.Produtos;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories.Produtos
{
    public class CategoriaProdutoRepository(IServiceProvider serviceProvider) :
        RepositorioBase<CategoriaProduto, OrderViaWhatsAppContext>(serviceProvider),
            ICategoriaProdutoRepository
    {
    }
}
