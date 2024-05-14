using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Base;

namespace Presentation.Configurations.Extensions
{
    public static class ApiConfig
    {
        public const string V1 = "1.0";

        public static WebApplicationBuilder ConfigureWebApi(this WebApplicationBuilder webAppBuilder)
        {
            var presentationAssembly = typeof(MainController).Assembly;
            webAppBuilder.Services.AddControllers().AddApplicationPart(presentationAssembly);

            webAppBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            webAppBuilder.Services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddApiExplorer(options =>
            {
                options.SubstituteApiVersionInUrl = true;
                options.GroupNameFormat = "'v'VVV";
            });

            return webAppBuilder;
        }
    }
}
