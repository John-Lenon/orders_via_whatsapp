using Domain.Entities.Empresas;
using Domain.Entities.Enderecos;
using Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings.Enderecos
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasColumnName("CODIGO")
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Cep)
                .HasColumnName("CEP")
                .HasColumnType("varchar(8)");

            builder.Property(x => x.Uf)
                .IsRequired()
                .HasColumnName("UF")
                .HasColumnType("char(2)");

            builder.Property(x => x.Bairro)
                .IsRequired()
                .HasColumnName("BAIRRO")
                .HasColumnType("varchar(30)");

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasColumnName("CIDADE")
                .HasColumnType("varchar(30)");

            builder.Property(x => x.Logradouro)
                .IsRequired()
                .HasColumnName("LOGRADOURO")
                .HasColumnType("varchar(50)");

            builder.Property(x => x.NumeroLogradouro)
                .IsRequired()
                .HasColumnName("NUMERO_LOGRADOURO")
                .HasColumnType("int");

            builder.Property(x => x.Complemento)
                .HasColumnName("COMPLEMENTO")
                .HasColumnType("varchar(30)");

            builder.ToTable(TableNames.ENDERECO).HasKey(x => x.Id);

            builder.HasOne(endereco => endereco.Empresa)
                   .WithOne(empresa => empresa.Endereco)
                   .HasPrincipalKey<Endereco>(x => x.Id)
                   .HasForeignKey<Empresa>(x => x.IdDoEndereco);
        }
    }
}
