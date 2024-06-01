﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(OrderViaWhatsAppContext))]
    partial class OrderViaWhatsAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Empresa.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("CNPJ");

                    b.Property<Guid>("Codigo")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO");

                    b.Property<string>("Dominio")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DOMINIO");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("EnderecoDaCapaDeFundo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("ENDERECO_DA_CAPA_DE_FUNDO");

                    b.Property<string>("EnderecoDoLogotipo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("ENDERECO_DO_LOGOTIPO");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NOME_FANTASIA");

                    b.Property<string>("NumeroDoWhatsapp")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("NUMERO_DO_WHATSAPP");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("RAZAO_SOCIAL");

                    b.Property<int>("StatusDeFuncionamento")
                        .HasColumnType("int")
                        .HasColumnName("STATUS_DE_FUNCIONAMENTO");

                    b.HasKey("Id");

                    b.ToTable("EMPRESA", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Empresa.HorarioFuncionamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("DiaDaSemana")
                        .HasColumnType("int")
                        .HasColumnName("DIA_DA_SEMANA");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int")
                        .HasColumnName("EMPRESA_ID");

                    b.Property<int>("Hora")
                        .HasColumnType("int")
                        .HasColumnName("HORA");

                    b.Property<int>("Minutos")
                        .HasColumnType("int")
                        .HasColumnName("MINUTOS");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("HORARIO_FUNCIONAMENTO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Permissao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("DESCRICAO");

                    b.HasKey("Id");

                    b.ToTable("PERMISSAO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CaminhoImagem")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CAMINHO_IMAGEM");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int")
                        .HasColumnName("CATEGORIA_ID");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("NOME");

                    b.Property<decimal>("Preco")
                        .HasMaxLength(50)
                        .HasColumnType("DECIMAL(19,2)")
                        .HasColumnName("PRECO");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int")
                        .HasColumnName("PRIORIDADE");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("STATUS");

                    b.HasKey("Id")
                        .HasName("PK_ID");

                    b.ToTable("PRODUTO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("BIT")
                        .HasColumnName("ATIVO");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("CodigoUnicoSenha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CODIGO_UNICO_SENHA");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("NOME");

                    b.Property<string>("SenhaHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("SENHA_HASH");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("Id");

                    b.ToTable("USUARIO", (string)null);
                });

            modelBuilder.Entity("USUARIO_PERMISSAO", b =>
                {
                    b.Property<int>("USUARIO_ID")
                        .HasColumnType("INT");

                    b.Property<int>("PERMISSAO_ID")
                        .HasColumnType("INT");

                    b.HasKey("USUARIO_ID", "PERMISSAO_ID");

                    b.HasIndex("PERMISSAO_ID");

                    b.ToTable("USUARIO_PERMISSAO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Empresa.HorarioFuncionamento", b =>
                {
                    b.HasOne("Domain.Entities.Empresa.Empresa", "Empresa")
                        .WithMany("HorariosDeFuncionamento")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("USUARIO_PERMISSAO", b =>
                {
                    b.HasOne("Domain.Entities.Permissao", null)
                        .WithMany()
                        .HasForeignKey("PERMISSAO_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("USUARIO_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Empresa.Empresa", b =>
                {
                    b.Navigation("HorariosDeFuncionamento");
                });
#pragma warning restore 612, 618
        }
    }
}
