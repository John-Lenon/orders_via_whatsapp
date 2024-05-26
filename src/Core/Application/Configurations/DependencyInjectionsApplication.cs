using Application.Commands.Interfaces;
using Application.Commands.Services;
using Application.Queries.Interfaces.Produto;
using Application.Queries.Services;
using Application.Utilities;
using Domain.Interfaces.Utilities;
using Microsoft.Extensions.DependencyInjection;

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
        }

        public static void AddDependecyServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthCommandService, AuthCommandService>();
            services.AddScoped<IUsuarioCommandService, UsuarioCommandService>();
            services.AddScoped<IProdutoQueryService, ProdutoQueryService>();
            services.AddScoped<IProdutoCommandService, ProdutoCommandService>();
        }
    }
}
