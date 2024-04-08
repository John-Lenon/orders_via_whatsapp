using Application.Configurations;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configurations
{
    public static class BancoDadosExtensions
    {
        public static IServiceProvider ConfigurarBancoDeDados(this IServiceProvider serviceProvider, CompanyConnectionStrings companies)        
        {
            using var serviceScope = serviceProvider.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<OrderViaWhatsAppContext>();

            foreach (var company in companies.List)
            {
                dbContext.Database.SetConnectionString(company.ConnnectionString);
                dbContext.Database.Migrate();
            }
            return serviceProvider;
        }

        public static IServiceCollection ConfigurarBancoDeDados(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<OrderViaWhatsAppContext>(options =>
            {
                options.UseSqlServer();
            });
            return serviceCollection;
        }
    }
}
