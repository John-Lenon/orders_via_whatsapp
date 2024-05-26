using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder
                .Property(u => u.Id)
                .HasColumnName("ID")
                .IsRequired()
                .HasColumnType("INT")
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Codigo)
                .HasColumnName("CODIGO")
                .IsRequired()
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasColumnType("VARCHAR(80)")
                .HasMaxLength(80)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(u => u.Telefone)
                .HasColumnName("TELEFONE")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.SenhaHash)
                .HasColumnName("SENHA_HASH")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(u => u.CodigoUnicoSenha)
                .HasColumnName("CODIGO_UNICO_SENHA")
                .HasColumnType("VARCHAR(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Ativo).HasColumnName("ATIVO").HasColumnType("BIT").IsRequired();

            builder
                .HasMany(u => u.Permissoes)
                .WithMany(p => p.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "USUARIO_PERMISSAO",
                    column => column.HasOne<Permissao>().WithMany().HasForeignKey("PERMISSAO_ID"),
                    column =>
                        column.HasOne<Usuario>().WithMany().HasForeignKey("USUARIO_ID"),
                    table =>
                    {
                        table.ToTable("USUARIO_PERMISSAO");
                        table.HasKey("USUARIO_ID", "PERMISSAO_ID");
                    }
                );
        }
    }
}
