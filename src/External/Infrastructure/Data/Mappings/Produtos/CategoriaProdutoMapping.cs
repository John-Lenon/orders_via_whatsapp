using Domain.Entities.Produtos;
using Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Produtos
{
    public class CategoriaProdutoMapping : IEntityTypeConfiguration<CategoriaProduto>
    {
        public void Configure(EntityTypeBuilder<CategoriaProduto> builder)
        {
            builder.ToTable(TableNames.CATEGORIAPRODUTO)
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

            builder.HasMany(categoria => categoria.Produtos)
                .WithOne(produto => produto.CategoriaProduto)
                .HasPrincipalKey(categoria => categoria.Id)
                .HasForeignKey(produto => produto.CategoriaId);
        }
    }
}
