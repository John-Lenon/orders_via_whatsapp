using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.Utilities;
using Application.Utilities;

namespace Application.Configurations
{
    public static class DependencyInjectionsApplication
    {
        public static IServiceCollection AddDependencyInjectionsApp(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            return services;
        }
    }
}
