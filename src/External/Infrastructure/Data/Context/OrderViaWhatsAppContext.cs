using Domain.Entities;
using Domain.Entities.Empresas;
using Domain.Entities.Enderecos;
using Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class OrderViaWhatsAppContext(DbContextOptions<OrderViaWhatsAppContext> options)
        : DbContext(options)
    {
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<HorarioFuncionamento> HorarioFuncionamento { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderViaWhatsAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public void SetConnectionString(string newStringConnection)
        {
            Database.CurrentTransaction?.Commit();

            Database.SetConnectionString(newStringConnection);
        }
    }
}
