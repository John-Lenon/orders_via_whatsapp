using Application.Interfaces.Auth;
using Application.Services.Auth;
using Application.Utilities;
using Data.Repository.Usuarios;
using Domain.Interfaces.Usuarios;
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
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void AddDependecyServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
