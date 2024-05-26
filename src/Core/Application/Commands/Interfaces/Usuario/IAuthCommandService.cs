using Domain.DTOs;
using Domain.Entities;
using Domain.Enumeradores.Pemissoes;

namespace Application.Commands.Interfaces
{
    public interface IAuthCommandService
    {
        Task<UsuarioTokenCommandDTO> AutenticarAsync(UsuarioCommandDTO userDto);
        bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar);
        Task AdicionarPermissaoAoUsuarioAsync(int usuarioId, params EnumPermissoes[] permissoes);
        UsuarioTokenCommandDTO GerarToken(Usuario usuario);
    }
}