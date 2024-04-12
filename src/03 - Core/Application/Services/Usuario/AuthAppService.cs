﻿using Application.Interfaces.Usuario;
using Application.Services.Base;
using Domain.DTOs.Usuario;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entity = Domain.Entities.Usuario;

namespace Application.Services.Usuario
{
    public class AuthAppService(IServiceProvider service, IConfiguration _configuration)
        : BaseAppService<Entity.Usuario, IUsuarioRepositorio>(service),
            IAuthAppService
    {
        public async Task<UsuarioTokenDto> AutenticarAsync(UsuarioDto userDto)
        {
            var usuario = await _repository
                .Get()
                .Include(c => c.Permissoes)
                .SingleOrDefaultAsync(u => u.Email == userDto.Email);

            if(usuario == null)
            {
                Notificar(EnumTipoNotificacao.Erro, "Email não encontrado.");
                return null;
            }

            bool senhaValida = VerificarSenhaHash(
                userDto.Senha,
                usuario.SenhaHash,
                usuario.CodigoUnicoSenha
            );

            if(!senhaValida)
            {
                Notificar(EnumTipoNotificacao.Erro, "Senha inválida.");
                return null;
            }

            return GerarToken(usuario);
        }

        public bool VerificarPermissao(params EnumPermissoes[] permissoesParaValidar)
        {
            var permissoesUsuario = _httpContext?.User?.Claims?.Select(claim =>
                claim.Value.ToString()
            );

            var possuiPermissao = permissoesParaValidar
                .Select(permissaoNecessaria => permissaoNecessaria.ToString())
                .All(permissaoNecessaria =>
                    permissoesUsuario.Any(permissao => permissao == permissaoNecessaria)
                );

            return possuiPermissao;
        }

        public UsuarioTokenDto GerarToken(Entity.Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenExpirationTime = DateTime.UtcNow.AddDays(
                int.Parse(_configuration["TokenConfiguration:ExpireDays"])
            );

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Email),
                new("codigo_usuario", usuario.Codigo.ToString())
            };

            if(usuario.Permissoes.Count > 0)
            {
                foreach(var permissao in usuario.Permissoes)
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

        private bool VerificarSenhaHash(string senha, string senhaHash, string codigoUnicoSenha) =>
            PasswordHasher.CompararSenhaHash(senha, codigoUnicoSenha) == senhaHash;
    }
}