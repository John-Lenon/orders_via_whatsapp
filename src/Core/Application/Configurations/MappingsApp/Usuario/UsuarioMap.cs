using Domain.DTOs.Usuario;
using Entity = Domain.Entities.Usuario;


namespace Application.Configurations.MappingsApp.Usuario
{
    public static class UsuarioMap
    {
        public static Entity.Usuario MapToNewUsuario(this UsuarioDto usuarioDto)
        {
            return new Entity.Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Telefone = usuarioDto.Telefone,
                Ativo = usuarioDto.Ativo,
            };
        }

        public static void MapToUsuario(this UsuarioDto usuarioDto, Entity.Usuario usuario)
        {
            usuario.Nome = usuarioDto.Nome;
            usuario.Email = usuarioDto.Email;
            usuario.Telefone = usuarioDto.Telefone;
            usuario.Ativo = usuarioDto.Ativo;
        }
    }
}
