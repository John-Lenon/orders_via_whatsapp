using Domain.Entities;
using Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class PermissaoMapping : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable(TableNames.PERMISSAO);

            builder
               .Property(u => u.Codigo)
               .HasColumnName("CODIGO")
               .IsRequired()
               .HasDefaultValueSql("NEWID()");

            builder
                .Property(p => p.Id)
                .HasColumnName("ID")
                .IsRequired()
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
