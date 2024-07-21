using Microsoft.AspNetCore.Http;

namespace Application.Utilities
{
    public static class Http
    {
        public static string ObterNomeDominioAcessado(this HttpContext httpContext)
        {
            var origin = httpContext.Request.Headers.Origin.ToString();
            var hostName = string.IsNullOrEmpty(origin) ?
                httpContext.Request.Host.Host :
                origin.Split("//")[1].Split('/')[0].Split(':')[0];
            return hostName;
        }
    }
}
