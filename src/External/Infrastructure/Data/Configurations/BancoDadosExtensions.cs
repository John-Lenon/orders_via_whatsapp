using Application.Commands.Interfaces;
using Application.Configurations;
using Application.Utilities;
using Domain.Entities;
using Domain.Enumeradores.Pemissoes;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Configurations
{
    public static class BancoDadosExtensions
    {
        public async static Task ConfigurarBancoDeDadosAsync(this WebApplication webApplication)
        {
            var serviceProvider = webApplication.Services;
            var companies = serviceProvider.GetRequiredService<CompanyConnectionStrings>();
            foreach (var company in companies.List)
            {
                using var serviceScope = serviceProvider.CreateScope();
                var service = serviceScope.ServiceProvider;
                var dbContext = service.GetRequiredService<OrderViaWhatsAppContext>();

                dbContext.Database.SetConnectionString(company.ConnnectionString);
                dbContext.Database.Migrate();
                await PrepareUserAdminAsync(service);
            }
        }

        #region Private Methods

        public static void SetDatabaseProvider(this WebApplicationBuilder webAppBuilder)
        {
            var appSettingsSection = webAppBuilder.Configuration.GetSection(nameof(CompanyConnectionStrings));
            var connectionStringBancoBase = AppSettings.CompanyConnectionStrings.GetDefaultString();

            webAppBuilder.Services.AddDbContext<OrderViaWhatsAppContext>(options =>
            {
                options.UseSqlServer(connectionStringBancoBase);
            });
        }

        private async static Task PrepareUserAdminAsync(IServiceProvider service)
        {
            var usuarioRepository = service.GetRequiredService<IUsuarioRepository>();
            var authAppService = service.GetRequiredService<IAuthCommandService>();

            string email = "master@gmail.com";
            string senha = "Master@123456";

            if (usuarioRepository.Get(u => u.Email == email).FirstOrDefault() != null)
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

            await usuarioRepository.InsertAsync(usuario);
            if (!usuarioRepository.SaveChangesAsync().Result)
                return;

            authAppService.AdicionarPermissaoAoUsuarioAsync(usuario.Id, permissoes).Wait();
        }
        #endregion
    }
}

