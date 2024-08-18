using Application.Commands.Interfaces;
using Asp.Versioning;
using Domain.DTOs;
using Domain.Enumeradores.Pemissoes;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Atributos.Auth;
using Presentation.Base;
using Presentation.Configurations.Extensions;

namespace Presentation.V1
{
    [ApiController]
    [RouterController("usuario")]
    [ApiVersion(ApiConfig.V1)]
    public class UsuarioController(IServiceProvider serviceProvider, IUsuarioCommandService _usuarioServiceApp)
        : MainController(serviceProvider)
    {
        [HttpPost("login")]
        public async Task<UsuarioTokenCommandDTO> LoginAsync(UsuarioCommandDTO userDto) =>
            await _usuarioServiceApp.LoginAsync(userDto);

        [HttpPost("cadastrar")]
        [PermissoesApi(EnumPermissoes.USU_000001)]
        public async Task<UsuarioTokenCommandDTO> CadastrarAsync(UsuarioCommandDTO userDto) =>
            await _usuarioServiceApp.CadastrarAsync(userDto);

        [HttpPost("atualizar")]
        [PermissoesApi(EnumPermissoes.USU_000002)]
        public async Task<UsuarioCommandDTO> AtualizarAsync(int usuarioId, UsuarioCommandDTO userDto) =>
            await _usuarioServiceApp.AtualizarAsync(usuarioId, userDto);
    }
}
