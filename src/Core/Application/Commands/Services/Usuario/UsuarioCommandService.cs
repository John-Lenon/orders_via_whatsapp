using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Queries.Interfaces.Usuario;
using Application.Resources.Messages;
using Application.Utilities;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commands.Services
{
    public class UsuarioCommandService(IServiceProvider service, IAuthCommandService _authApp)
        : CommandServiceBase<Usuario, UsuarioCommandDTO, IUsuarioRepository>(service),
            IUsuarioCommandService
    {
        private IUsuarioQueryService _usuarioQueryService = service.GetService<IUsuarioQueryService>();

        public async Task<UsuarioTokenCommandDTO> LoginAsync(UsuarioCommandDTO userDto) =>
            await _authApp.AutenticarAsync(userDto);

        public async Task<UsuarioTokenCommandDTO> CadastrarAsync(UsuarioCommandDTO usuarioDto)
        {
            if (!UsuarioDtoIsValid(usuarioDto) || !Validator(usuarioDto))
                return null;

            if (!await ValidarUsuarioParaCadastrarAsync(usuarioDto))
                return null;


            var (codigoUnicoSenha, SenhaHash) = new PasswordHasher().GerarSenhaHash(usuarioDto.Senha);

            var usuario = usuarioDto.MapToEntity<UsuarioCommandDTO, Usuario>();

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            await _repository.InsertAsync(usuario);
            if (!await CommitAsync())
                return null;

            return _authApp.GerarToken(usuario);
        }

        public async Task<UsuarioCommandDTO> AtualizarAsync(int idUsuarioLogado, UsuarioCommandDTO usuarioDto)
        {
            if (!UsuarioDtoIsValid(usuarioDto) || !Validator(usuarioDto))
                return null;

            var usuario = await _repository.GetByIdAsync(idUsuarioLogado);

            if (!await ValidarUsuarioParaAtualizarAsync(usuario, usuarioDto, idUsuarioLogado))
                return null;

            var (codigoUnicoSenha, SenhaHash) = new PasswordHasher().GerarSenhaHash(
                usuarioDto.Senha
            );

            usuario.GetValuesFrom(usuarioDto);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            _repository.Update(usuario);
            if (!await CommitAsync())
                return null;

            return usuarioDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (!UsuarioExiste(usuario))
                return false;

            if (!UsuarioPossuiAutorizacao(usuario))
                return false;

            _repository.Delete(usuario);
            if (!await CommitAsync())
                return false;

            return true;
        }

        #region Supports Methods
        public async Task<bool> ValidarUsuarioParaCadastrarAsync(UsuarioCommandDTO usuarioDto)
        {
            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if (usuarioExistente != null)
            {
                return ReportarCamposJaEmUso(usuarioExistente, usuarioDto);
            }

            return true;
        }

        private async Task<bool> ValidarUsuarioParaAtualizarAsync(Usuario usuario,
            UsuarioCommandDTO usuarioDto,
            int idUsuarioLogado)
        {
            if (!UsuarioExiste(usuario))
                return false;

            if (!UsuarioPossuiAutorizacao(usuario))
                return false;

            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if (usuarioExistente != null && usuarioExistente?.Id != idUsuarioLogado)
            {
                return ReportarCamposJaEmUso(usuarioExistente, usuarioDto);
            }

            return true;
        }

        private bool UsuarioPossuiAutorizacao(Usuario usuario)
        {
            var codeUserAuthenticated = _httpContext.User.FindFirst("codigo_usuario")?.Value;

            if (codeUserAuthenticated != usuario.Codigo.ToString())
            {
                if (_usuarioQueryService.PossuiPermissao(EnumPermissoes.USU_000004)
                    || _usuarioQueryService.PossuiPermissao(EnumPermissoes.USU_000005))
                {
                    return true;
                }

                Notificar(EnumTipoNotificacao.ErroCliente, Message.OperacaoNaoPermitida);
                return false;
            }

            return true;
        }

        private async Task<Usuario> GetUsuarioExistenteAsync(UsuarioCommandDTO userDto)
        {
            return await _repository
                .Get()
                .Where(user => user.Email == userDto.Email || user.Telefone == userDto.Telefone)
                .FirstOrDefaultAsync();
        }

        public bool UsuarioDtoIsValid(UsuarioCommandDTO usuarioDto)
        {
            if (usuarioDto is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.ModeloInvalido);
                return false;
            }

            return true;
        }

        public bool UsuarioExiste(Usuario usuario)
        {
            if (usuario is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, string.Format(Message.RegistroNaoEncontrado, "Usuário"));
                return false;
            }

            return true;
        }

        private bool ReportarCamposJaEmUso(Usuario usuario, UsuarioCommandDTO usuarioDto)
        {
            var campos = new List<(string propUsuario, string propDto, string descricao)>
            {
                (usuario.Email, usuarioDto.Email, "O e-mail"),
                (usuario.Telefone, usuarioDto.Telefone, "O telefone")
            };

            var camposEmUso = campos.Where(campo => campo.propUsuario == campo.propDto).ToList();

            foreach (var campo in camposEmUso)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, string.Format(Message.CampoExistente, campo.descricao));
            }

            return camposEmUso.Count == 0;
        }

        protected override Usuario MapToEntity(UsuarioCommandDTO entityDTO)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
