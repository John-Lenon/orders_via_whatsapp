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
    public class UsuarioController(
        IServiceProvider serviceProvider,
        IUsuarioAppService _usuarioServiceApp
    ) : MainController(serviceProvider)
    {
        [HttpPost("registrar")]
        [PermissoesApi(EnumPermissoes.USU_000001)]
        public async Task<UsuarioTokenDto> RegistrarAsync(UsuarioDto userDto) =>
            await _usuarioServiceApp.CadastrarAsync(userDto);

        [HttpPost("atualizar")]
        [PermissoesApi(EnumPermissoes.USU_000002)]
        public async Task<UsuarioDto> AtualizarAsync(int usuarioId, UsuarioDto userDto) =>
            await _usuarioServiceApp.AtualizarAsync(usuarioId, userDto);

        [HttpDelete("deletar")]
        [PermissoesApi(EnumPermissoes.USU_000003)]
        public async Task<bool> DeletarAsync(int usuarioId) =>
            await _usuarioServiceApp.DeleteAsync(usuarioId);

        [HttpPost("login")]
        public async Task<UsuarioTokenDto> LoginAsync(UsuarioDto userDto) =>
            await _usuarioServiceApp.AutenticarAsync(userDto);
    }
}
