﻿using Domain.Enumeradores.Notificacao;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Base;

namespace Presentation.Atributos.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AutorizationApi : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var response = new ResponseResultDTO<string>()
                {
                    Mensagens =
                    [
                        new Notificacao(
                            EnumTipoNotificacao.ErroCliente,
                            "Acesso não autorizado. Você precisa estar autenticado."
                        )
                    ]
                };

                context.Result = new ObjectResult(response) { StatusCode = 401 };
                return;
            }
        }
    }
}
