using Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Produtos
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO").HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("ID")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Codigo)
                   .HasColumnName("CODIGO")
                   .IsRequired()
                   .HasDefaultValueSql("NEWID()");

            builder.Property(e => e.Nome)
                   .HasColumnName("NOME")
                   .HasColumnType("VARCHAR(50)")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Preco)
                   .HasColumnName("PRECO")
                   .HasColumnType("DECIMAL(19,2)")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.CaminhoImagem)
                   .HasColumnName("CAMINHO_IMAGEM")
                   .HasColumnType("VARCHAR(100)")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(e => e.CategoriaId)
                   .HasColumnName("CATEGORIA_ID")
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(e => e.Prioridade)
                    .HasColumnName("PRIORIDADE")
                    .HasColumnType("int")
                    .IsRequired();

            builder.Property(e => e.Status)
                   .HasColumnName("STATUS")
                   .HasColumnType("int")
                   .IsRequired();
        }
    }
}
