using Application.Configurations;
using Domain.Enumeradores.Notificacao;
using Domain.Utilities;
using Infrastructure.Data.Context;
using Presentation.Base;
using System.Text.Json;

namespace Web.Middlewares
{
    public class IdentificadorDataBaseMiddleware : IMiddleware
    {
        private readonly OrderViaWhatsAppContext _context;
        private readonly CompanyConnectionStrings _companyConnections;

        public IdentificadorDataBaseMiddleware(OrderViaWhatsAppContext dbContext, CompanyConnectionStrings companyConnections)
        {
            _context = dbContext;
            _companyConnections = companyConnections;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var connectionString = await IdentificarStringConexaoAsync(context);
            if (string.IsNullOrEmpty(connectionString)) return;

            _context.SetConnectionString(connectionString);
            await next(context);

        }

        private async Task<string> IdentificarStringConexaoAsync(HttpContext httpContext)
        {
            var origin = httpContext.Request.Headers["Origin"].ToString();
            var hostName = string.IsNullOrEmpty(origin) ?
                httpContext.Request.Host.Host :
                origin.Split("//")[1].Split('/')[0];

            var empresaLocalizada = _companyConnections.List.FirstOrDefault(empresa =>
                empresa.NomeDominio == hostName
            );

            if (empresaLocalizada == null)
            {
                var response = new ResponseResultDTO<string>();
                response.Mensagens =
                [
                    new Notificacao(
                        EnumTipoNotificacao.ErroCliente,
                        $"A empresa com nome de domínio '{hostName}' não foi encontrada")
                ];

                httpContext.Response.Headers.TryAdd("content-type", "application/json; charset=utf-8");
                httpContext.Response.StatusCode = 404;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                return null;
            }
            return empresaLocalizada.ConnnectionString;
        }
    }
}
