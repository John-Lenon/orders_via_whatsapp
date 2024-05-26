using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repository
{
    public class UsuarioRepository(IServiceProvider service)
        : RepositorioBase<Usuario, OrderViaWhatsAppContext>(service),
            IUsuarioRepository
    {
    }
}
