using Domain.Interfaces.Base;
using Entity = Domain.Entities.Usuario;

namespace Domain.Interfaces.Usuario
{
    public interface IUsuarioRepositorio : IRepositoryBase<Entity.Usuario>
    {
    }
}
