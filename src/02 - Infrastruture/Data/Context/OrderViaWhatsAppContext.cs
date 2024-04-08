using Domain.Entities.Produto;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class OrderViaWhatsAppContext : DbContext
    {
        public DbSet<Produto> Produto { get; set; }

        public OrderViaWhatsAppContext(DbContextOptions<OrderViaWhatsAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderViaWhatsAppContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public void SetConnectionString(string newStringConnection)
        {
            if(this.Database.CurrentTransaction is not null)
            {
                this.Database.CurrentTransaction.Commit();
            };
            this.Database.SetConnectionString(newStringConnection);
        }
    }
}
