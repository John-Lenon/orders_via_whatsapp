using Domain.Interfaces.Usuario;
using Infrastructure.Data.Repository.Usuario;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Configurations
{
    public static class DependencyInjectionsData
    {
        public static void AddDataLayerDependencies(this IServiceCollection services)
        {
            services.AddDependecyRepositories();
        }

        public static void AddDependecyRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
