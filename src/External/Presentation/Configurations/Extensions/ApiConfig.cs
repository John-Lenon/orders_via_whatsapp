using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Base;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Presentation.Configurations.Extensions
{
    public static class ApiConfig
    {
        public const string V1 = "1.0";
        public const string V2 = "2.0";

        public static string[] ListVersions { get; private set; } = [V1, V2];

        public static WebApplicationBuilder ConfigureWebApi(this WebApplicationBuilder webAppBuilder)
        {
            var presentationAssembly = typeof(MainController).Assembly;
            webAppBuilder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new ApiVersioningFilter());

            })
            .AddApplicationPart(presentationAssembly)
            .AddJsonOptions(opt =>
            {
                var options = opt.JsonSerializerOptions;

                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

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

    public class ApiVersioningFilter : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var version = controller.Attributes.OfType<ApiVersionAttribute>().FirstOrDefault();

            if (version != null)
            {
                controller.ApiExplorer.GroupName = $"v{version.Versions.FirstOrDefault()}";
            }
        }
    }
}
