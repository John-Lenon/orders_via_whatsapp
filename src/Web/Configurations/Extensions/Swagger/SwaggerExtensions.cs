using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Presentation.Configurations.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Configurations.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                foreach(var version in ApiConfig.ListVersions)
                {
                    options.SwaggerDoc(
                        $"v{version}",
                        new OpenApiInfo { Title = $"API v{version}", Version = version }
                    );
                }

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                options.CustomSchemaIds(x => x.FullName);

                options.AddSecuritySchema();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        public static void AddSecuritySchema(this SwaggerGenOptions options)
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = $"Insira o token JWT desta maneira \"Bearer <seu token>\" ",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            options.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securitySchema, new[] { "Bearer" } }
            };

            options.AddSecurityRequirement(securityRequirement);
        }

        public static void ConfigureSwaggerUI(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach(var version in ApiConfig.ListVersions)
                {
                    options.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"API v{version}");
                }

                options.RoutePrefix = "doc";
                options.DocumentTitle = "Order Via WhatsApp API";
            });
        }
    }
}
