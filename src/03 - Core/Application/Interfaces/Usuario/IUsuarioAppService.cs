using Domain.DTOs.Usuario;

namespace Application.Interfaces.Usuario
{
    public interface IUsuarioAppService
    {
        Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto userDto);
        Task<UsuarioDto> AtualizarAsync(int usuarioId, UsuarioDto usuarioDto);
        Task<bool> DeleteAsync(int id);
    }
}
