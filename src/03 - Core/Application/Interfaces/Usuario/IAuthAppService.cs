﻿using Domain.DTOs.Usuario;
using Domain.Enumeradores.Pemissoes;
using Entity = Domain.Entities.Usuario;

namespace Application.Interfaces.Usuario
{
    public interface IAuthAppService
    {
        Task<UsuarioTokenDto> AutenticarAsync(UsuarioDto userDto);
        bool VerificarPermissao(params EnumPermissoes[] permissoesParaValidar);
        UsuarioTokenDto GerarToken(Entity.Usuario usuario);
    }
}