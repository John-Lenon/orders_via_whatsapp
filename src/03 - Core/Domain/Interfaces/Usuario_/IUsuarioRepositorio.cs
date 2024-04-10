using Domain.Entities.Usuario;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Base;

namespace Domain.Interfaces.Usuario_
{
    public interface IUsuarioRepositorio : IRepositoryBase<Usuario>
    {
        Task AdicionarPermissaoAoUsuarioAsync(int usuarioId, params EnumPermissoes[] permissoes);
    }
}
