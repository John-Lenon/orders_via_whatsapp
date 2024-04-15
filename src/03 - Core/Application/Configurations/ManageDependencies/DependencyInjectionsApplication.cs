using Application.Interfaces.Usuario;
using Application.Services.Usuario;
using Application.Utilities;
using Data.Repository.Usuario;
using Domain.Interfaces.Usuario;
using Domain.Interfaces.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configurations.ManageDependencies
{
    public static class DependencyInjectionsApplication
    {
        public static void AddAllDependencyInjections(this IServiceCollection services)
        {
            services.AddDependecyUtilities();
            services.AddDependecyRepositories();
            services.AddDependecyServices();
        }

        public static void AddDependecyUtilities(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
        }

        public static void AddDependecyRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        }

        public static void AddDependecyServices(this IServiceCollection services)
        {

            services.AddScoped<IAuthAppService, AuthAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        }
    }
}
