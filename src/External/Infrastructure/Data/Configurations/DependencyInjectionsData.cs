using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Produtos;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Produtos;
using Infrastructure.Data.Repository;
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
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IHorarioFuncionamentoRepository, HorarioFuncionamentoRepository>();
            services.AddScoped<ICategoriaProdutoRepository, CategoriaProdutoRepository>();
        }
    }
}
