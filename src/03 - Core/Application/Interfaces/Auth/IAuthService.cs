using Domain.DTOs.Usuario;
using Domain.Enumeradores.Pemissoes;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<UsuarioTokenDto> AutenticarUsuario(UsuarioDto userDto);
        Task<UsuarioTokenDto> CadastrarUsuario(UsuarioDto userDto);
        bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar);
    }
}
