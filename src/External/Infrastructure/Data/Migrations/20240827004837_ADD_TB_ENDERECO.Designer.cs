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
    [Migration("20240827004837_ADD_TB_ENDERECO")]
    partial class ADD_TB_ENDERECO
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Empresas.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)")
                        .HasColumnName("CNPJ");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

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

                    b.Property<int?>("IdDoEndereco")
                        .HasColumnType("int")
                        .HasColumnName("ID_ENDERECO");

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

                    b.Property<string>("StatusDeFuncionamento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("STATUS_DE_FUNCIONAMENTO");

                    b.HasKey("Id");

                    b.HasIndex("IdDoEndereco")
                        .IsUnique()
                        .HasFilter("[ID_ENDERECO] IS NOT NULL");

                    b.ToTable("EMPRESA", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Empresas.HorarioFuncionamento", b =>
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

                    b.Property<string>("DiaDaSemana")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
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

            modelBuilder.Entity("Domain.Entities.Enderecos.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("BAIRRO");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(8)")
                        .HasColumnName("CEP");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CIDADE");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(30)")
                        .HasColumnName("COMPLEMENTO");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("LOGRADOURO");

                    b.Property<int>("NumeroLogradouro")
                        .HasColumnType("int")
                        .HasColumnName("NUMERO_LOGRADOURO");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("char(2)")
                        .HasColumnName("UF");

                    b.HasKey("Id");

                    b.ToTable("ENDERECO", (string)null);
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

            modelBuilder.Entity("Domain.Entities.Produtos.AdicionalProduto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaAdicionalProdutoId")
                        .HasColumnType("int")
                        .HasColumnName("ID_CATEGORIA_ADC_PRODUTO");

                    b.Property<Guid>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CODIGO")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal")
                        .HasColumnName("PRECO");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int")
                        .HasColumnName("PRIORIDADE");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaAdicionalProdutoId");

                    b.ToTable("ADICIONAL_PRODUTO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Produtos.CategoriaAdicionalProduto", b =>
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

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("NOME");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int")
                        .HasColumnName("PRIORIDADE");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("CATEGORIA_ADICIONAL_PRODUTO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Produtos.CategoriaProduto", b =>
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

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(80)")
                        .HasColumnName("NOME");

                    b.Property<int>("Prioridade")
                        .HasColumnType("int")
                        .HasColumnName("PRIORIDADE");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("STATUS");

                    b.HasKey("Id");

                    b.ToTable("CATEGORIA_PRODUTO", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Produtos.Produto", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

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

            modelBuilder.Entity("PRODUTO_E_ADICIONAL_PRODUTO", b =>
                {
                    b.Property<int>("PRODUTO_ID")
                        .HasColumnType("int");

                    b.Property<int>("ADICIONAL_PRODUTO_ID")
                        .HasColumnType("int");

                    b.HasKey("PRODUTO_ID", "ADICIONAL_PRODUTO_ID");

                    b.HasIndex("ADICIONAL_PRODUTO_ID");

                    b.ToTable("PRODUTO_E_ADICIONAL_PRODUTO", (string)null);
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

            modelBuilder.Entity("Domain.Entities.Empresas.Empresa", b =>
                {
                    b.HasOne("Domain.Entities.Enderecos.Endereco", "Endereco")
                        .WithOne("Empresa")
                        .HasForeignKey("Domain.Entities.Empresas.Empresa", "IdDoEndereco");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Domain.Entities.Empresas.HorarioFuncionamento", b =>
                {
                    b.HasOne("Domain.Entities.Empresas.Empresa", "Empresa")
                        .WithMany("HorariosDeFuncionamento")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Domain.Entities.Produtos.AdicionalProduto", b =>
                {
                    b.HasOne("Domain.Entities.Produtos.CategoriaAdicionalProduto", "CategoriaAdicionalProduto")
                        .WithMany("AdicionaisProdutos")
                        .HasForeignKey("CategoriaAdicionalProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaAdicionalProduto");
                });

            modelBuilder.Entity("Domain.Entities.Produtos.Produto", b =>
                {
                    b.HasOne("Domain.Entities.Produtos.CategoriaProduto", "CategoriaProduto")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoriaProduto");
                });

            modelBuilder.Entity("PRODUTO_E_ADICIONAL_PRODUTO", b =>
                {
                    b.HasOne("Domain.Entities.Produtos.AdicionalProduto", null)
                        .WithMany()
                        .HasForeignKey("ADICIONAL_PRODUTO_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Produtos.Produto", null)
                        .WithMany()
                        .HasForeignKey("PRODUTO_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("Domain.Entities.Empresas.Empresa", b =>
                {
                    b.Navigation("HorariosDeFuncionamento");
                });

            modelBuilder.Entity("Domain.Entities.Enderecos.Endereco", b =>
                {
                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Domain.Entities.Produtos.CategoriaAdicionalProduto", b =>
                {
                    b.Navigation("AdicionaisProdutos");
                });

            modelBuilder.Entity("Domain.Entities.Produtos.CategoriaProduto", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
