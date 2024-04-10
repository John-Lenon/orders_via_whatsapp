using Domain.Entities.Produto;
using Domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class OrderViaWhatsAppContext(DbContextOptions<OrderViaWhatsAppContext> options)
        : DbContext(options)
    {
        public DbSet<Produto> Produto { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Permissao> Permissao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderViaWhatsAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public void SetConnectionString(string newStringConnection)
        {
            if (this.Database.CurrentTransaction is not null)
            {
                this.Database.CurrentTransaction.Commit();
            }
            ;
            this.Database.SetConnectionString(newStringConnection);
        }
    }
}
