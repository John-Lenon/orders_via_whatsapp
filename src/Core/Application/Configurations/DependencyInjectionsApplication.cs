using Application.Commands.Interfaces;
using Application.Commands.Interfaces.Produtos;
using Application.Commands.Services;
using Application.Commands.Services.Produtos;
using Application.Queries.Interfaces;
using Application.Queries.Interfaces.Produtos;
using Application.Queries.Interfaces.Usuario;
using Application.Queries.Services;
using Application.Queries.Services.Produtos;
using Application.Utilities;
using Application.Utilities.Utilities;
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
            services.AddDependecyCommandServices();
            services.AddDependecyQueryServices();
        }

        public static void AddDependecyUtilities(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFileService, FileService>();
        }

        public static void AddDependecyCommandServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthCommandService, AuthCommandService>();
            services.AddScoped<IUsuarioCommandService, UsuarioCommandService>();
            services.AddScoped<IHorarioFuncionamentoCommandService, HorarioFuncionamentoCommandService>();
            services.AddScoped<IEmpresaCommandService, EmpresaCommandService>();
            services.AddScoped<IProdutoCommandService, ProdutoCommandService>();
            services.AddScoped<ICategoriaProdutoCommandService, CategoriaProdutoCommandService>();
        }

        public static void AddDependecyQueryServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoQueryService, ProdutoQueryService>();
            services.AddScoped<IEmpresaQueryService, EmpresaQueryService>();
            services.AddScoped<IHorarioFuncionamentoQueryService, HorarioFuncionamentoQueryService>();
            services.AddScoped<IUsuarioQueryService, UsuarioQueryService>();
            services.AddScoped<ICategoriaProdutoQueryService, CategoriaProdutoQueryService>();
        }

        public static void AddAssemblyConfigurations(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
