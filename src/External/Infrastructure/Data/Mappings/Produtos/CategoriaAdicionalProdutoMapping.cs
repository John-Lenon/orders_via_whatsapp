using Domain.Entities.Produtos;
using Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Produtos
{
    public class CategoriaAdicionalProdutoMapping : IEntityTypeConfiguration<CategoriaAdicionalProduto>
    {
        public void Configure(EntityTypeBuilder<CategoriaAdicionalProduto> builder)
        {
            builder.ToTable(TableNames.CATEGORIAADICIONALPRODUTO)
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Codigo)
               .HasColumnName("CODIGO")
               .IsRequired()
               .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasColumnType("VARCHAR(80)")
                .IsRequired();

            builder.Property(x => x.Status)
               .HasColumnType("int")
               .HasColumnName("STATUS");

            builder.Property(x => x.Prioridade)
                .HasColumnType("int")
                .HasColumnName("PRIORIDADE");
        }
    }
}
