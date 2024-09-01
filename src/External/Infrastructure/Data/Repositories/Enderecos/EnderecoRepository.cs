using Domain.Entities.Enderecos;
using Domain.Interfaces.Repositories.Enderecos;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories.Enderecos
{
    public class EnderecoRepository(IServiceProvider serviceProvider) :
       RepositorioBase<Endereco, OrderViaWhatsAppContext>(serviceProvider),
           IEnderecoRepository
    {
    }
}
