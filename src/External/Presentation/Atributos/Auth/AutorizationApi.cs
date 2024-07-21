using Application.Utilities;
using Domain.Enumeradores.Notificacao;
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

            var dominioAuthenticado = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "dominio")?.Value;
            var dominioAtual = context.HttpContext.ObterNomeDominioAcessado();

            if (dominioAuthenticado == null || dominioAuthenticado != dominioAtual)
            {
                var response = new ResponseResultDTO<string>()
                {
                    Mensagens =
                    [
                        new Notificacao(
                            EnumTipoNotificacao.ErroCliente,
                            $"Você não está autenticado neste domínio '{dominioAtual}'. Você foi autenticado no domínio '{dominioAuthenticado}'."
                        )
                    ]
                };
                context.Result = new ObjectResult(response) { StatusCode = 401 };
                return;
            }
        }
    }
}
