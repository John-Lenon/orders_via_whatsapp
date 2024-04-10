using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces.Auth;
using Application.Services.Base;
using Domain.DTOs.Usuario;
using Domain.Entities.Usuario;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Auth
{
    public class AuthService(
        IServiceProvider service,
        IConfiguration _configuration,
        IHttpContextAccessor _httpContext
    ) : BaseService<Usuario, IUsuarioRepositorio>(service), IAuthService
    {
        protected readonly HttpContext _httpContext = _httpContext.HttpContext;

        public async Task<UsuarioTokenDto> CadastrarUsuario(UsuarioDto usuarioDto)
        {
            if (Validator(usuarioDto))
                return null;

            if (await VerificarExistenciaDeUsuarioAsync(usuarioDto))
                return null;

            var (codigoUnicoSenha, SenhaHash) = PasswordHasher.GerarSenhaHash(usuarioDto.Senha);

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            usuario.SenhaHash = SenhaHash;
            usuario.CodigoUnicoSenha = codigoUnicoSenha;

            await _repository.InsertAsync(usuario);

            if (!await _repository.SaveChangesAsync())
            {
                Notificar(EnumTipoNotificacao.ErroInterno, "Ocorreu um erro ao resgistrar.");
                return null;
            }

            return GerarToken(usuario);
        }

        public async Task<UsuarioTokenDto> AutenticarUsuario(UsuarioDto userDto)
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

        public bool PossuiPermissao(params EnumPermissoes[] permissoesParaValidar)
        {
            var permissoes = _httpContext?.User?.Claims?.Select(claim => claim.Value.ToString());

            var possuiPermissao = permissoesParaValidar
                .Select(permissao => permissao.ToString())
                .All(permissao => permissoes.Any(x => x == permissao));

            return possuiPermissao;
        }

        #region Supports Methods
        private bool VerificarSenhaHash(string senha, string senhaHash, string salt) =>
            PasswordHasher.CompararSenhaHash(senha, salt) == senhaHash;

        private async Task<bool> VerificarExistenciaDeUsuarioAsync(UsuarioDto usuarioDto)
        {
            var usuarioExistente = await _repository
                .Get()
                .FirstOrDefaultAsync(u =>
                    u.Nome == usuarioDto.Nome
                    || u.Email == usuarioDto.Email
                    || u.Telefone == usuarioDto.Telefone
                );

            if (usuarioExistente != null)
            {
                var campoDuplicado =
                    usuarioExistente.Nome == usuarioDto.Nome
                        ? "nome"
                        : usuarioExistente.Email == usuarioDto.Email
                            ? "e-mail"
                            : "telefone";

                var mensagemErro = $"O {campoDuplicado} fornecido já está existe.";

                Notificar(EnumTipoNotificacao.Erro, mensagemErro);
                return true;
            }

            return false;
        }

        private UsuarioTokenDto GerarToken(Usuario usuario)
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
        #endregion
    }
}
