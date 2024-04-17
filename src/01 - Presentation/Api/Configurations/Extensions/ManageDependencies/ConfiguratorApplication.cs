﻿using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Api.Configurations.Extensions.ManageDependencies
{
    public static class ConfiguratorApplication
    {
        public static void AddApiDependencyConfigurator(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddHttpContextAccessor();
            services.AddAuthenticationJwt(config);
            services.AddAssemblyConfigurations();

            services.AddAllDependencyInjections();
        }

        public static void AddAuthenticationJwt(
            this IServiceCollection services,
            IConfiguration config
        )
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
    }
}