using Api.Base;
using Api.Extensions.Atributos;
using Api.Extensions.Atributos.Auth;
using Application.Interfaces.Usuario;
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
        [HttpPost("login")]
        public async Task<UsuarioTokenDto> LoginAsync(UsuarioDto userDto) =>
            await _usuarioServiceApp.LoginAsync(userDto);

        [HttpPost("cadastrar")]
        [PermissoesApi(EnumPermissoes.USU_000001)]
        public async Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto userDto) =>
            await _usuarioServiceApp.CadastrarAsync(userDto);

        [HttpPost("atualizar")]
        [PermissoesApi(EnumPermissoes.USU_000002)]
        public async Task<UsuarioDto> AtualizarAsync(int usuarioId, UsuarioDto userDto) =>
            await _usuarioServiceApp.AtualizarAsync(usuarioId, userDto);

        [HttpDelete("deletar")]
        [PermissoesApi(EnumPermissoes.USU_000003)]
        public async Task<bool> DeletarAsync(int usuarioId) =>
            await _usuarioServiceApp.DeleteAsync(usuarioId);
    }
}
