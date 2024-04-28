using Data.Context;
using Data.Repository.Base;
using Domain.Interfaces.Usuario;
using Entity = Domain.Entities.Usuario;

namespace Data.Repository.Usuario
{
    public class UsuarioRepositorio(IServiceProvider service)
        : RepositorioBase<Entity.Usuario, OrderViaWhatsAppContext>(service),
            IUsuarioRepositorio
    {
    }
}
