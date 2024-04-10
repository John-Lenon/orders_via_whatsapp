using Api.Attributes;
using Api.Base;
using Api.Extensions.Atributos;
using Application.Interfaces.Auth;
using Domain.DTOs.Usuario;
using Domain.Enumeradores.Pemissoes;
using Microsoft.AspNetCore.Mvc;

namespace Api.V1.Usuario
{
    [ApiController]
    [RouterController("usuario")]
    public class UsuarioController(IServiceProvider serviceProvider, IAuthService _authService)
        : MainController(serviceProvider)
    {
        [HttpPost("registrar")]
        [PermissoesApi(EnumPermissoes.USU_000001)]
        public async Task<UsuarioTokenDto> Registrar(UsuarioDto userDto) =>
            await _authService.CadastrarUsuario(userDto);

        [HttpPost("login")]
        public async Task<UsuarioTokenDto> Login(UsuarioDto userDto) =>
            await _authService.AutenticarUsuario(userDto);
    }
}
