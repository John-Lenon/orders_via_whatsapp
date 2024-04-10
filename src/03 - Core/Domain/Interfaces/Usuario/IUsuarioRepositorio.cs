using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Base;
using Entity = Domain.Entities.Usuario;

namespace Domain.Interfaces.Usuario
{
    public interface IUsuarioRepositorio : IRepositoryBase<Entity.Usuario>
    {
        Task AdicionarPermissaoAoUsuarioAsync(int usuarioId, params EnumPermissoes[] permissoes);
    }
}
