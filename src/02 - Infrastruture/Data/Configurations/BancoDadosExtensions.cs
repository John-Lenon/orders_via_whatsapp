using Data.Context;
using Domain.Entities.Usuario;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Configurations
{
    public static class BancoDadosExtensions
    {
        public static void ConfigurarBancoDeDados(
            this IServiceProvider serviceProvider,
            CompanyConnectionStrings companies
        )
        {
            using var serviceScope = serviceProvider.CreateScope();
            var service = serviceScope.ServiceProvider;

            var dbContext = service.GetRequiredService<OrderViaWhatsAppContext>();

            foreach(var company in companies.List)
            {
                dbContext.Database.SetConnectionString(company.ConnnectionString);
                dbContext.Database.Migrate();
            }

            PrepareUserAdmin(service);
        }

        public static void ConfigurarBancoDeDados(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<OrderViaWhatsAppContext>(options =>
            {
                options.UseSqlServer();
            });
        }

        public static void PrepareUserAdmin(IServiceProvider service)
        {
            var usuarioRepository = service.GetRequiredService<IUsuarioRepositorio>();

            string email = "master@gmail.com";
            string senha = "Master@123456";

            if(usuarioRepository.Get(u => u.Email == email).FirstOrDefault() != null)
                return;

            var (codigoUnicoSenha, senhaHash) = new PasswordHasher().GerarSenhaHash(senha);

            var usuario = new Usuario
            {
                Nome = "Master",
                Email = email,
                Telefone = "(31) 99999-9999",
                SenhaHash = senhaHash,
                CodigoUnicoSenha = codigoUnicoSenha,
                Permissoes = []
            };

            var permissoes = new EnumPermissoes[] { EnumPermissoes.USU_000001 };

            usuarioRepository.InsertAsync(usuario).Wait();
            if(!usuarioRepository.SaveChangesAsync().Result)
                return;

            usuarioRepository.AdicionarPermissaoAoUsuarioAsync(usuario.Id, permissoes).Wait();
        }
    }
}
