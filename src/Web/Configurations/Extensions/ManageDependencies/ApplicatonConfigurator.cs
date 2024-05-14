using FluentValidation;
using Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Configurations.Extensions;
using System.Reflection;
using System.Text;
using Web.Middlewares;

namespace Api.Configurations.Extensions.ManageDependencies
{
    public static class ApplicatonConfigurator
    {
        public static void AddServicesDependencyInjectionSystem(this WebApplicationBuilder webAppBuilder)
        {
            CarregarStringConexaoBancoDados(webAppBuilder);
            webAppBuilder.Services.AddAplicationLayerDependencies();
            webAppBuilder.Services.AddDataLayerDependencies();

            webAppBuilder.Services.AddTransient<ExceptionMiddleware>();
            webAppBuilder.Services.AddTransient<IdentificadorDataBaseMiddleware>();
        }

        public static void AddAuthenticationJwt(this IServiceCollection services, IConfiguration config)
        {
            var key = Encoding.ASCII.GetBytes(config["Jwt:key"]);
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = config["TokenConfiguration:Issuer"],
                        ValidAudience = config["TokenConfiguration:Audience"]
                    };
                });
        }

        public static void AddAssemblyConfigurations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder webAppBuilder)
        {
            webAppBuilder.Configuration.AddJsonFile(
                path: $"appsettings.{webAppBuilder.Environment.EnvironmentName}.json",
                optional: false,
                reloadOnChange: true
            );

            webAppBuilder.ConfigureWebApi();
            webAppBuilder.Services.AddHttpContextAccessor();
            webAppBuilder.Services.AddAuthenticationJwt(webAppBuilder.Configuration);
            webAppBuilder.Services.AddAssemblyConfigurations();

            return webAppBuilder;
        }

        public static WebApplication AddCorsPolicy(this WebApplication app, WebApplicationBuilder webAppBuilder)
        {
            var allowedOrigins = webAppBuilder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
            app.UseCors(configurePolicy =>
            {
                configurePolicy.WithOrigins(allowedOrigins);
                configurePolicy.AllowAnyMethod();
                configurePolicy.AllowAnyHeader();
            });

            return app;
        }

        private static void CarregarStringConexaoBancoDados(WebApplicationBuilder webAppBuilder)
        {
            var appSettingsSection = webAppBuilder.Configuration.GetSection(nameof(CompanyConnectionStrings));
            var companyConnectionStrings = appSettingsSection.Get<CompanyConnectionStrings>();
            webAppBuilder.Services.AddSingleton(companyConnectionStrings);
        }
    }
}
