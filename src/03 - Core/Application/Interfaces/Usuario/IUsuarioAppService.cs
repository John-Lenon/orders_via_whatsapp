using Domain.DTOs.Usuario;
using Domain.Enumeradores.Pemissoes;

namespace Application.Interfaces.Auth
{
    public interface IUsuarioAppService
    {
        Task<UsuarioTokenDto> AutenticarAsync(UsuarioDto userDto);
        Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto userDto);
        Task<UsuarioDto> AtualizarAsync(int usuarioId, UsuarioDto usuarioDto);
        Task<bool> DeleteAsync(int id);
        bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar);
    }
}
