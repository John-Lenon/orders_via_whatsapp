using Data.Context;
using Data.Repository.Base;
using Domain.Entities.Usuario;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Usuarios
{
    public class UsuarioRepositorio(IServiceProvider service)
        : RepositorioBase<Usuario, OrderViaWhatsAppContext>(service),
            IUsuarioRepositorio
    {
        public async Task AdicionarPermissaoAoUsuarioAsync(
            int usuarioId,
            params EnumPermissoes[] permissoes
        )
        {
            var usuario = await Get(user => user.Id == usuarioId)
                .Include(p => p.Permissoes)
                .FirstOrDefaultAsync();

            foreach (var permissao in permissoes)
            {
                var possuiPermissao = usuario
                    .Permissoes.Where(p => p.Descricao == permissao.ToString())
                    .FirstOrDefault();

                if (possuiPermissao is null)
                {
                    usuario.Permissoes.Add(new Permissao { Descricao = permissao.ToString() });
                }
            }

            Update(usuario);
            await SaveChangesAsync();
        }
    }
}
