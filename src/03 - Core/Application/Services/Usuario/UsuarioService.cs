using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Auth;
using Application.Services.Base;
using Domain.DTOs.Usuario;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Entity = Domain.Entities.Usuario;

namespace Application.Services.Usuario
{
    public class UsuarioService(
        IServiceProvider service,
        IConfiguration _configuration,
        IHttpContextAccessor _httpContext
    ) : BaseAppService<Entity.Usuario, IUsuarioRepositorio>(service), IUsuarioService
    {
        protected readonly HttpContext _httpContext = _httpContext.HttpContext;

        public async Task<UsuarioTokenDto> AutenticarAsync(UsuarioDto userDto)
        {
            var usuario = await _repository
                .Get()
                .Include(c => c.Permissoes)
                .SingleOrDefaultAsync(u => u.Email == userDto.Email);

            if (usuario == null)
            {
                Notificar(EnumTipoNotificacao.Erro, "Email não encontrado.");
                return null;
            }

            bool senhaValida = VerificarSenhaHash(
                userDto.Senha,
                usuario.SenhaHash,
                usuario.CodigoUnicoSenha
            );

            if (!senhaValida)
            {
                Notificar(EnumTipoNotificacao.Erro, "Senha inválida.");
                return null;
            }

            return GerarToken(usuario);
        }

        public async Task<UsuarioTokenDto> CadastrarAsync(UsuarioDto usuarioDto)
        {
            if (Validator(usuarioDto))
                return null;

            if (await VerificarCredenciaisExistentesAsync(usuarioDto))
                return null;

            var (codigoUnicoSenha, SenhaHash) = PasswordHasher.GerarSenhaHash(usuarioDto.Senha);

            var usuario = _mapper.Map<Entity.Usuario>(usuarioDto);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            await _repository.InsertAsync(usuario);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao cadastrar.");
                return null;
            }

            return GerarToken(usuario);
        }

        public async Task<UsuarioDto> AtualizarAsync(int usuarioId, UsuarioDto usuarioDto)
        {
            if (Validator(usuarioDto))
                return null;

            var usuario = await _repository.GetByIdAsync(usuarioId);

            if (usuario is null)
            {
                Notificar(
                    EnumTipoNotificacao.Erro,
                    $"Não foi encontrado um registro com o Id " + usuarioId
                );

                return null;
            }

            if (await VerificarCredenciaisExistentesAsync(usuarioDto, usuarioId))
                return null;

            var (codigoUnicoSenha, SenhaHash) = PasswordHasher.GerarSenhaHash(usuarioDto.Senha);

            _mapper.Map(usuarioDto, usuario);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            _repository.Update(usuario);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao atualizar.");
                return null;
            }

            return usuarioDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var usuario = await _repository.GetByIdAsync(id);

            if (usuario is null)
            {
                Notificar(
                    EnumTipoNotificacao.Erro,
                    "Não foi encontrado um registro com o Id " + id
                );
                return false;
            }

            _repository.Delete(usuario);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao deletar");
                return false;
            }

            Notificar(EnumTipoNotificacao.Informacao, "Usuário deletado com sucesso.");
            return true;
        }

        public bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar)
        {
            var permissoes = _httpContext?.User?.Claims?.Select(claim => claim.Value.ToString());

            var possuiPermissao = permissoesParaValidar
                .Select(permissao => permissao.ToString())
                .All(permissao => permissoes.Any(x => x == permissao));

            return possuiPermissao;
        }

        #region Supports Methods

        private UsuarioTokenDto GerarToken(Entity.Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenExpirationTime = DateTime.UtcNow.AddDays(
                int.Parse(_configuration["TokenConfiguration:ExpireDays"])
            );

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);

            var claims = new List<Claim> { new(ClaimTypes.Name, usuario.Email) };

            if (usuario.Permissoes.Count > 0)
            {
                foreach (var permissao in usuario.Permissoes)
                {
                    claims.Add(new Claim("Permission", permissao.Descricao));
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = tokenExpirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Audience = _configuration["TokenConfiguration:Audience"],
                Issuer = _configuration["TokenConfiguration:Issuer"]
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return new UsuarioTokenDto()
            {
                Authenticated = true,
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenExpirationTime,
            };
        }

        private bool VerificarSenhaHash(string senha, string senhaHash, string salt) =>
            PasswordHasher.CompararSenhaHash(senha, salt) == senhaHash;

        private async Task<bool> VerificarCredenciaisExistentesAsync(
            UsuarioDto usuarioDto,
            int usuarioId = 0
        )
        {
            var usuarioExistente = await _repository
                .Get()
                .FirstOrDefaultAsync(u =>
                    u.Email == usuarioDto.Email || u.Telefone == usuarioDto.Telefone
                );

            if (usuarioExistente != null)
            {
                if (usuarioExistente.Id == usuarioId)
                    return false;

                string campoDuplicado =
                    usuarioExistente.Email == usuarioDto.Email ? "e-mail" : "telefone";

                var mensagemErro = $"O {campoDuplicado} fornecido já existe.";

                Notificar(EnumTipoNotificacao.Erro, mensagemErro);
                return true;
            }

            return false;
        }
        #endregion
    }
}
