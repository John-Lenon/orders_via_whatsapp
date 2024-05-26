using Domain.DTOs;
using Domain.Entities;

namespace Application.Configurations.MappingsApp
{
    public static class UsuarioMap
    {
        public static Usuario MapToNewUsuario(this UsuarioCommandDTO usuarioDto)
        {
            return new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Telefone = usuarioDto.Telefone,
                Ativo = usuarioDto.Ativo,
            };
        }

        public static void MapToUsuario(this UsuarioCommandDTO usuarioDto, Usuario usuario)
        {
            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
            usuario.Telefone = usuarioDto.Telefone;
            usuario.Ativo = usuarioDto.Ativo;
        }
    }
}
