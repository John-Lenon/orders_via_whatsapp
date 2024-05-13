using Domain.Interfaces.Usuario;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;
using Entity = Domain.Entities.Usuario;

namespace Infrastructure.Data.Repository.Usuario
{
    public class UsuarioRepositorio(IServiceProvider service)
        : RepositorioBase<Entity.Usuario, OrderViaWhatsAppContext>(service),
            IUsuarioRepositorio
    {
    }
}
