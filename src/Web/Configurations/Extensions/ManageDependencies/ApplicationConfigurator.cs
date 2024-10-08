﻿using Api.Configurations.Extensions.Swagger;
using Application.Configurations;
using Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Configurations.Extensions;
using System.Text;
using Web.Middlewares;

namespace Api.Configurations.Extensions.ManageDependencies
{
    public static class ApplicationConfigurator
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
            var key = Encoding.ASCII.GetBytes(AppSettings.JwtConfigs.Key);
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
                        ValidIssuer = AppSettings.JwtConfigs.Issuer,
                        ValidAudience = AppSettings.JwtConfigs.Audience
                    };
                });
        }


        public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder webAppBuilder)
        {
            AppSettings.CarregarDados(webAppBuilder.Configuration);
            webAppBuilder.Configuration.AddJsonFile(
                path: $"appsettings.{webAppBuilder.Environment.EnvironmentName}.json",
                optional: false,
                reloadOnChange: true
            );

            webAppBuilder.ConfigureWebApi();
            webAppBuilder.Services.AddHttpContextAccessor();
            webAppBuilder.Services.AddAuthenticationJwt(webAppBuilder.Configuration);
            webAppBuilder.Services.AddAssemblyConfigurations();
            webAppBuilder.Services.AddSwaggerConfiguration();

            return webAppBuilder;
        }

        public static WebApplication AddCorsPolicy(this WebApplication app, WebApplicationBuilder webAppBuilder)
        {
            app.UseCors(configurePolicy =>
            {
                if (webAppBuilder.Environment.IsDevelopment())
                    configurePolicy.AllowAnyOrigin();
                else
                    configurePolicy.WithOrigins(AppSettings.AllowedOrigins);

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
