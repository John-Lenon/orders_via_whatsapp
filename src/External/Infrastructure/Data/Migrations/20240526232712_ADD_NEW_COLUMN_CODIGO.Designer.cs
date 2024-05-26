﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(OrderViaWhatsAppContext))]
    [Migration("20240526232712_ADD_NEW_COLUMN_CODIGO")]
    partial class ADD_NEW_COLUMN_CODIGO
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<int>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT")
                        .HasDefaultValue(1)
                        .HasColumnName("ATIVO");

                    b.Property<string>("CaminhoImagem")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("CAMINHO_IMAGEM");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("NOME");

                    b.Property<decimal>("Preco")
                        .HasMaxLength(50)
                        .HasColumnType("DECIMAL(19,2)")
                        .HasColumnName("PRECO");

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
#pragma warning restore 612, 618
        }
    }
}
