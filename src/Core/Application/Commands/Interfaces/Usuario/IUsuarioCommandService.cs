using Domain.DTOs;

namespace Application.Commands.Interfaces
{
    public interface IUsuarioCommandService
    {
        Task<UsuarioTokenCommandDTO> LoginAsync(UsuarioCommandDTO userDto);
        Task<UsuarioTokenCommandDTO> CadastrarAsync(UsuarioCommandDTO userDto);
        Task<UsuarioCommandDTO> AtualizarAsync(int usuarioId, UsuarioCommandDTO usuarioDto);
        Task<bool> DeleteAsync(int id);
    }
}
