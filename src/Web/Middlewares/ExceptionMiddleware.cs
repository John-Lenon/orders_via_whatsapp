using Domain.Enumeradores.Notificacao;
using Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Presentation.Base;
using System.Text.Json;

namespace Web.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _environmentHost;

        public ExceptionMiddleware(IWebHostEnvironment environmentHost)
        {
            _environmentHost = environmentHost;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var response = new ResponseResultDTO<string>();
                response.Mensagens = new Notificacao[]
                {
                    new Notificacao(
                        EnumTipoNotificacao.ErroInterno,
                        $"Erro interno no servidor.{(_environmentHost.IsDevelopment()? " " + ex.Message : "")}")
                };

                httpContext.Response.Headers.TryAdd("content-type", "application/json; charset=utf-8");
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
