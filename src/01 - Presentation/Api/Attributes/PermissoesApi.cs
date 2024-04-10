﻿using Api.Base;
using Domain.Enumeradores.Notificacao;
using Domain.Enumeradores.Pemissoes;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class PermissoesApi(params EnumPermissoes[] enumPermissoes)
        : Attribute,
            IAuthorizationFilter
    {
        private IEnumerable<string> EnumPermissoes { get; } =
            enumPermissoes.Select(x => x.ToString());

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var possuiTodasPermissoes = EnumPermissoes.All(permissao =>
                context.HttpContext.User.Claims.Any(claim => claim.Value == permissao)
            );

            if (!possuiTodasPermissoes)
            {
                var response = new ResponseResultDTO<string>()
                {
                    Mensagens =
                    [
                        new Notificacao(
                            EnumTipoNotificacao.Erro,
                            "Você não tem permissão para acessar esse recurso."
                        )
                    ]
                };

                context.Result = new ObjectResult(response) { StatusCode = 401 };
                return;
            }
        }
    }
}
