using Application.Commands.Interfaces;
using Application.Commands.Services;
using Application.Interfaces.Utilities;
using Application.Queries.Interfaces.Produto;
using Application.Queries.Services;
using Application.Utilities;
using Domain.Interfaces.Utilities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Configurations
{
    public static class DependencyInjectionsApplication
    {
        public static void AddAplicationLayerDependencies(this IServiceCollection services)
        {
            services.AddDependecyUtilities();
            services.AddDependecyServices();
        }

        public static void AddDependecyUtilities(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFileService, FileService>();
        }

        public static void AddDependecyServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthCommandService, AuthCommandService>();
            services.AddScoped<IUsuarioCommandService, UsuarioCommandService>();
            services.AddScoped<IProdutoQueryService, ProdutoQueryService>();
            services.AddScoped<IProdutoCommandService, ProdutoCommandService>();
        }

        public static void AddAssemblyConfigurations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
