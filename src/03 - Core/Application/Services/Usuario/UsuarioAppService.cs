using Application.Interfaces.Usuario;
using Application.Services.Base;
using Domain.DTOs.Usuario;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Entity = Domain.Entities.Usuario;

namespace Application.Services.Usuario
{
    public class UsuarioAppService(IServiceProvider service, IAuthAppService _authAppService)
        : BaseAppService<Entity.Usuario, IUsuarioRepositorio>(service),
            IUsuarioAppService
    {
        public async Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto usuarioDto)
        {
            if(UsuarioDtoIsNull(usuarioDto) || Validator(usuarioDto))
                return null;

            if(await ValidarUsuarioParaCadastrarAsync(usuarioDto))
                return null;

            var (codigoUnicoSenha, SenhaHash) = PasswordHasher.GerarSenhaHash(usuarioDto.Senha);

            var usuario = _mapper.Map<Entity.Usuario>(usuarioDto);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            await _repository.InsertAsync(usuario);

            if(!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao cadastrar.");
                return null;
            }

            return _authAppService.GerarToken(usuario);
        }

        public async Task<UsuarioDto> AtualizarAsync(int idUsuarioLogado, UsuarioDto usuarioDto)
        {
            if(UsuarioDtoIsNull(usuarioDto) || Validator(usuarioDto))
            {
                return null;
            }

            var usuario = await _repository.GetByIdAsync(idUsuarioLogado);

            if(!await ValidarUsuarioParaAtualizarAsync(usuario, usuarioDto, idUsuarioLogado))
            {
                return null;
            }

            var (codigoUnicoSenha, SenhaHash) = PasswordHasher.GerarSenhaHash(usuarioDto.Senha);

            _mapper.Map(usuarioDto, usuario);
            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            _repository.Update(usuario);
            if(!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao atualizar.");
                return null;
            }

            return usuarioDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if(usuario is null)
            {
                Notificar(
                    EnumTipoNotificacao.Erro,
                    "Não foi encontrado um registro com o Id " + id
                );
                return false;
            }

            if(AutorizarAcaoDoUsuario(usuario))
                return false;

            _repository.Delete(usuario);

            if(!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao deletar");
                return false;
            }

            Notificar(EnumTipoNotificacao.Informacao, "Usuário deletado com sucesso.");
            return true;
        }

        #region Supports Methods
        public bool UsuarioDtoIsNull(UsuarioDto usuarioDto)
        {
            if(usuarioDto == null)
            {
                Notificar(EnumTipoNotificacao.Erro, "Modelo de dados inválido.");
                return true;
            }

            return false;
        }

        public async Task<bool> ValidarUsuarioParaCadastrarAsync(UsuarioDto usuarioDto)
        {
            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if(usuarioExistente != null)
            {
                return ReportarCamposJaEmUso(usuarioExistente.Email, usuarioDto.Email);
            }

            return true;
        }

        private async Task<bool> ValidarUsuarioParaAtualizarAsync(
            Entity.Usuario usuario,
            UsuarioDto usuarioDto,
            int idUsuarioLogado
        )
        {
            if(usuario == null)
            {
                Notificar(
                    EnumTipoNotificacao.Erro,
                    $"Não foi encontrado um registro com o Id {idUsuarioLogado}"
                );
                return false;
            }

            if(!AutorizarAcaoDoUsuario(usuario))
            {
                return false;
            }

            var usuarioExistente = await GetUsuarioExistenteAsync(usuarioDto);

            if(usuarioExistente?.Id != idUsuarioLogado)
            {
                return ReportarCamposJaEmUso(usuarioExistente.Email, usuarioDto.Email);
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

        private bool ReportarCamposJaEmUso(string emailExistente, string emailUsuarioDto)
        {
            string campoDuplicado = emailExistente == emailUsuarioDto ? "e-mail" : "telefone";
            Notificar(EnumTipoNotificacao.Erro, $"O {campoDuplicado} fornecido já está em uso.");

            return false;
        }

        private bool AutorizarAcaoDoUsuario(Entity.Usuario usuario)
        {
            var codigoUsuario = _httpContext.User.FindFirst("codigo_usuario")?.Value;

            if(codigoUsuario != usuario.Codigo.ToString())
            {
                if(_authAppService.VerificarPermissao(EnumPermissoes.USU_000004)
                    || _authAppService.VerificarPermissao(EnumPermissoes.USU_000005)
                )
                {
                    return true;
                }

                Notificar(EnumTipoNotificacao.Erro, "Operação não permitida.");
                return false;
            }

            return true;
        }
        #endregion
    }
}
