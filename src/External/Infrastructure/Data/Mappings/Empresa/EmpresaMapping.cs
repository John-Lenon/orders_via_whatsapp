﻿using Domain.Entities.Empresas;
using Domain.Enumeradores.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("EMPRESA");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID").IsRequired();
            builder.Property(e => e.Codigo).HasColumnName("CODIGO").IsRequired().HasDefaultValueSql("NEWID()"); ;
            builder.Property(e => e.Email).HasColumnName("EMAIL").HasMaxLength(100);
            builder.Property(e => e.Dominio).HasColumnName("DOMINIO").HasMaxLength(50);
            builder.Property(e => e.Cnpj).HasColumnName("CNPJ").IsRequired().HasMaxLength(14);

            builder
                .Property(e => e.NomeFantasia)
                .HasColumnName("NOME_FANTASIA")
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(e => e.RazaoSocial)
                .HasColumnName("RAZAO_SOCIAL")
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(e => e.NumeroDoWhatsapp)
                .HasColumnName("NUMERO_DO_WHATSAPP")
                .HasMaxLength(15);

            builder.Property(e => e.StatusDeFuncionamento)
              .HasColumnName("STATUS_DE_FUNCIONAMENTO")
              .IsRequired()
              .HasConversion(new EnumToStringConverter<EnumStatusDeFuncionamento>())
              .HasMaxLength(50);

            builder
                .Property(e => e.EnderecoDoLogotipo)
                .HasColumnName("ENDERECO_DO_LOGOTIPO")
                .HasMaxLength(250);

            builder
                .Property(e => e.EnderecoDaCapaDeFundo)
                .HasColumnName("ENDERECO_DA_CAPA_DE_FUNDO")
                .HasMaxLength(250);

            builder.Property(x => x.IdDoEndereco)
                .HasColumnName("ID_ENDERECO")
                .HasColumnType("int");

            builder
                .HasMany(e => e.HorariosDeFuncionamento)
                .WithOne(h => h.Empresa)
                .HasForeignKey(h => h.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
