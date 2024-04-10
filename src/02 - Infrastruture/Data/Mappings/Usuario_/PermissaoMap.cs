using Domain.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings.Usuario_
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable("PERMISSAO");
            builder.Property(p => p.Id).IsRequired().HasColumnType("INT").ValueGeneratedOnAdd();

            builder
                .Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
