using Application.Commands.DTO;
using Application.Commands.Interfaces;
using Application.Commands.Services;
using Application.Commands.Validators;
using Application.Queries.Interfaces;
using Application.Queries.Services;
using Application.Utilities;
using Domain.DTOs;
using Domain.Interfaces.Utilities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations
{
    public static class DependencyInjectionsApplication
    {
        public static void AddAplicationLayerDependencies(this IServiceCollection services)
        {
            services.AddUtilityDependencies();
            services.AddServiceDependencies();
            services.AddValidatorDependencies();
        }

        public static void AddUtilityDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
        }

        public static void AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthCommandService, AuthCommandService>();
            services.AddScoped<IUsuarioCommandService, UsuarioCommandService>();
            services.AddScoped<IProdutoQueryService, ProdutoQueryService>();
            services.AddScoped<IProdutoCommandService, ProdutoCommandService>();
        }

        public static void AddValidatorDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<ProdutoCommandDTO>, ProdutoValidator>();
            services.AddSingleton<IValidator<UsuarioCommandDTO>, UsuarioValidator>();
        }
    }
}
