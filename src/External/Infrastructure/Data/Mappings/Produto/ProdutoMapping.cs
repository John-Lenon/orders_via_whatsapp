using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO").HasKey(e => e.Id).HasName("PK_ID");

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder
                .Property(e => e.Nome)
                .HasColumnName("NOME")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(e => e.Preco)
                .HasColumnName("PRECO")
                .HasColumnType("DECIMAL(19,2)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(e => e.CaminhoImagem)
                .HasColumnName("CAMINHO_IMAGEM")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(e => e.Ativo)
                .HasColumnName("ATIVO")
                .HasColumnType("INT")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
