using Application.Interfaces.Usuario;
using Application.Utilities;
using Data.Configurations;
using Data.Context;
using Domain.Entities.Usuario;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Api.Configurations.Extensions.DBSetup
{
    public static class BancoDadosExtensions
    {
        public static void ConfigurarBancoDeDados(
            this IServiceProvider serviceProvider,
            CompanyConnectionStrings companies
        )
        {
            foreach(var company in companies.List)
            {
                using var serviceScope = serviceProvider.CreateScope();
                var service = serviceScope.ServiceProvider;
                var dbContext = service.GetRequiredService<OrderViaWhatsAppContext>();

                dbContext.Database.SetConnectionString(company.ConnnectionString);
                dbContext.Database.Migrate();

                PrepareUserAdmin(service);

            }
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
            var authAppService = service.GetRequiredService<IAuthAppService>();

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

            var permissoes = new EnumPermissoes[]
            {
                EnumPermissoes.USU_000001,
                EnumPermissoes.USU_000002,
                EnumPermissoes.USU_000003,
                EnumPermissoes.USU_000004,
                EnumPermissoes.USU_000005
            };

            usuarioRepository.InsertAsync(usuario).Wait();
            if(!usuarioRepository.SaveChangesAsync().Result)
                return;

            authAppService.AdicionarPermissaoAoUsuarioAsync(usuario.Id, permissoes).Wait();
        }
    }
}
