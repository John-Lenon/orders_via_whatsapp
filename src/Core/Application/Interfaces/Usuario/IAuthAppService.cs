using Domain.DTOs.Usuario;
using Domain.Enumeradores.Pemissoes;
using Entity = Domain.Entities.Usuario;

namespace Application.Interfaces.Usuario
{
    public interface IAuthAppService
    {
        Task<UsuarioTokenDto> AutenticarAsync(UsuarioDto userDto);
        bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar);
        Task AdicionarPermissaoAoUsuarioAsync(int usuarioId, params EnumPermissoes[] permissoes);
        UsuarioTokenDto GerarToken(Entity.Usuario usuario);
    }
}