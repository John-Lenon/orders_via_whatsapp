using Domain.Entities.Produtos;
using Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Produtos
{
    public class AdicionalProdutoMapping : IEntityTypeConfiguration<AdicionalProduto>
    {
        public void Configure(EntityTypeBuilder<AdicionalProduto> builder)
        {
            builder.ToTable(TableNames.ADICIONALPRODUTO)
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Codigo)
               .HasColumnName("CODIGO")
               .IsRequired()
               .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.CategoriaAdicionalProdutoId)
                .HasColumnType("int")
                .HasColumnName("ID_CATEGORIA_ADC_PRODUTO");

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .HasColumnName("STATUS");

            builder.Property(x => x.Prioridade)
                .HasColumnType("int")
                .HasColumnName("PRIORIDADE");

            builder.Property(x => x.Preco)
                .HasColumnType("decimal")
                .HasColumnName("PRECO");

            builder.HasOne(adicional => adicional.CategoriaAdicionalProduto)
                .WithMany(categoria => categoria.AdicionaisProdutos)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(adicional => adicional.CategoriaAdicionalProdutoId);

            builder.HasMany(adicional => adicional.Produtos)
                .WithMany(produto => produto.AdicionaisProdutos)
                .UsingEntity<Dictionary<string, object>>(
                    "PRODUTO_E_ADICIONAL_PRODUTO",
                    column => column.HasOne<Produto>().WithMany().HasForeignKey("PRODUTO_ID"),
                    column => column.HasOne<AdicionalProduto>().WithMany().HasForeignKey("ADICIONAL_PRODUTO_ID"),
                    table =>
                    {
                        table.ToTable("PRODUTO_E_ADICIONAL_PRODUTO");
                        table.HasKey("PRODUTO_ID", "ADICIONAL_PRODUTO_ID");
                    }
                );
        }
    }
}
