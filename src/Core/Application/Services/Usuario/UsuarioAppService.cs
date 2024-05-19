using Application.Configurations.MappingsApp.Usuario;
using Application.Interfaces.Usuario;
using Application.Resources.Messages;
using Application.Services.Base;
using Application.Utilities;
using Domain.DTOs.Usuario;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;
using Entity = Domain.Entities.Usuario;

namespace Application.Services.Usuario
{
    public class UsuarioAppService(IServiceProvider service, IAuthAppService _authApp)
        : BaseAppService<Entity.Usuario, IUsuarioRepositorio>(service),
            IUsuarioAppService
    {
        public async Task<UsuarioTokenDto> LoginAsync(UsuarioDto userDto) =>
            await _authApp.AutenticarAsync(userDto);

        public async Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto usuarioDto)
        {
            if(!UsuarioDtoIsValid(usuarioDto) || !Validator(usuarioDto))
                return null;

            if(!await ValidarUsuarioParaCadastrarAsync(usuarioDto))
                return null;

            var (codigoUnicoSenha, SenhaHash) = new PasswordHasher().GerarSenhaHash(
                usuarioDto.Senha
            );

            var usuario = usuarioDto.MapToNewUsuario();
            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            await _repository.InsertAsync(usuario);
            if(!await CommitAsync())
                return null;

            return _authApp.GerarToken(usuario);
        }

        public async Task<UsuarioDto> AtualizarAsync(int idUsuarioLogado, UsuarioDto usuarioDto)
        {
            if(!UsuarioDtoIsValid(usuarioDto) || !Validator(usuarioDto))
                return null;

            var usuario = await _repository.GetByIdAsync(idUsuarioLogado);

            if(!await ValidarUsuarioParaAtualizarAsync(usuario, usuarioDto, idUsuarioLogado))
                return null;

            var (codigoUnicoSenha, SenhaHash) = new PasswordHasher().GerarSenhaHash(
                usuarioDto.Senha
            );

            usuarioDto.MapToUsuario(usuario);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            _repository.Update(usuario);
            if(!await CommitAsync())
                return null;

            return usuarioDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if(!UsuarioExiste(usuario))
                return false;

            if(!UsuarioPossuiAutorizacao(usuario))
                return false;

            _repository.Delete(usuario);
            if(!await CommitAsync())
                return false;

            return true;
        }

        #region Supports Methods
        public async Task<bool> ValidarUsuarioParaCadastrarAsync(UsuarioDto usuarioDto)
        {
            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if(usuarioExistente != null)
            {
                return ReportarCamposJaEmUso(usuarioExistente, usuarioDto);
            }

            return true;
        }

        private async Task<bool> ValidarUsuarioParaAtualizarAsync(
            Entity.Usuario usuario,
            UsuarioDto usuarioDto,
            int idUsuarioLogado
        )
        {
            if(!UsuarioExiste(usuario))
                return false;

            if(!UsuarioPossuiAutorizacao(usuario))
                return false;

            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if(usuarioExistente != null && usuarioExistente?.Id != idUsuarioLogado)
            {
                return ReportarCamposJaEmUso(usuarioExistente, usuarioDto);
            }

            return true;
        }

        private bool UsuarioPossuiAutorizacao(Entity.Usuario usuario)
        {
            var codeUserAuthenticated = _httpContext.User.FindFirst("codigo_usuario")?.Value;

            if(codeUserAuthenticated != usuario.Codigo.ToString())
            {
                if(
                    _authApp.PossuiPermissao(EnumPermissoes.USU_000004)
                    || _authApp.PossuiPermissao(EnumPermissoes.USU_000005)
                )
                {
                    return true;
                }

                Notificar(EnumTipoNotificacao.ErroCliente, Message.OperacaoNaoPermitida);
                return false;
            }

            return true;
        }

        private async Task<Entity.Usuario> GetUsuarioExistenteAsync(UsuarioDto userDto)
        {
            return await _repository
                .Get()
                .Where(user => user.Email == userDto.Email || user.Telefone == userDto.Telefone)
                .FirstOrDefaultAsync();
        }

        public bool UsuarioDtoIsValid(UsuarioDto usuarioDto)
        {
            if(usuarioDto is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.ModeloInvalido);
                return false;
            }

            return true;
        }

        public bool UsuarioExiste(Entity.Usuario usuario)
        {
            if(usuario is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, string.Format(Message.RegistroNaoEncontrado, "Usuário"));
                return false;
            }

            return true;
        }

        private bool ReportarCamposJaEmUso(Entity.Usuario usuario, UsuarioDto usuarioDto)
        {
            var campos = new List<(string propUsuario, string propDto, string descricao)>
            {
                (usuario.Email, usuarioDto.Email, "O e-mail"),
                (usuario.Telefone, usuarioDto.Telefone, "O telefone")
            };

            var camposEmUso = campos.Where(campo => campo.propUsuario == campo.propDto).ToList();

            foreach(var campo in camposEmUso)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, string.Format(Message.CampoExistente, campo.descricao));
            }

            return camposEmUso.Count == 0;
        }

        #endregion
    }
}
