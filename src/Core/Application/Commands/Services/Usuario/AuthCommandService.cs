using Application.Commands.Interfaces;
using Application.Commands.Services.Base;
using Application.Configurations;
using Application.Resources.Messages;
using Application.Utilities;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Commands.Services
{
    public class AuthCommandService(IServiceProvider service)
        : CommandServiceBase<Usuario, UsuarioCommandDTO, IUsuarioRepository>(service),
            IAuthCommandService
    {
        public async Task<UsuarioTokenCommandDTO> AutenticarAsync(UsuarioCommandDTO userDto)
        {
            if (userDto is null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.ModeloInvalido);
                return null;
            }
            var usuario = await _repository
                .Get()
                .Include(c => c.Permissoes)
                .SingleOrDefaultAsync(u => u.Email == userDto.Email);

            if (usuario == null)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.EmailNaoEncontrado);
                return null;
            }

            bool senhaValida = VerificarSenhaHash(
                userDto.Senha,
                usuario.SenhaHash,
                usuario.CodigoUnicoSenha);

            if (!senhaValida)
            {
                Notificar(EnumTipoNotificacao.ErroCliente, Message.SenhaInvalida);
                return null;
            }

            return GerarToken(usuario);
        }

        public bool PossuiPermissao(params EnumPermissoes[] permissoesNecessarias)
        {
            var permissoesUsuario = _httpContext?.User?.Claims?.Select(claim =>
                claim.Value.ToString()
            );

            var possuiPermissao = permissoesNecessarias.All(permissaoNecessaria =>
                permissoesUsuario.Any(permissao => permissao == permissaoNecessaria.ToString())
            );

            return possuiPermissao;
        }

        public async Task AdicionarPermissaoAoUsuarioAsync(int usuarioId, params EnumPermissoes[] permissoes)
        {
            var usuario = await _repository
                .Get(user => user.Id == usuarioId)
                .Include(p => p.Permissoes)
                .FirstOrDefaultAsync();

            foreach (var permissao in permissoes)
            {
                var possuiPermissao = usuario
                    .Permissoes.Where(p => p.Descricao == permissao.ToString())
                    .FirstOrDefault();

                if (possuiPermissao is null)
                {
                    usuario.Permissoes.Add(new Permissao { Descricao = permissao.ToString() });
                }
            }

            _repository.Update(usuario);
            await CommitAsync();
        }

        public UsuarioTokenCommandDTO GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenExpirationTime = DateTime.UtcNow.AddDays(AppSettings.JwtConfigs.ExpireDays);

            var key = Encoding.ASCII.GetBytes(AppSettings.JwtConfigs.Key);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Email),
                new("codigo_usuario", usuario.Codigo.ToString()),
                new("Permissao", EnumPermissoes.USU_000002.ToString()),
                new("Permissao", EnumPermissoes.USU_000003.ToString())
            };

            if (usuario.Permissoes.Count > 0)
            {
                foreach (var permissao in usuario.Permissoes)
                {
                    claims.Add(new Claim("Permissao", permissao.Descricao));
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
                Audience = AppSettings.JwtConfigs.Audience,
                Issuer = AppSettings.JwtConfigs.Issuer
            };

            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return new UsuarioTokenCommandDTO()
            {
                Authenticated = true,
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenExpirationTime,
            };
        }

        private bool VerificarSenhaHash(string senha, string senhaHash, string codigoUnicoSenha) =>
            new PasswordHasher().CompararSenhaHash(senha, codigoUnicoSenha) == senhaHash;

        protected override Usuario MapToEntity(UsuarioCommandDTO entityDTO)
        {
            throw new NotImplementedException();
        }
    }
}
