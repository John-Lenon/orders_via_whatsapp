using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TB_EMPRESA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_FANTASIA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RAZAO_SOCIAL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    NUMERO_DO_WHATSAPP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DOMINIO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    STATUS_DE_FUNCIONAMENTO = table.Column<int>(type: "int", nullable: false),
                    ENDERECO_DO_LOGOTIPO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ENDERECO_DA_CAPA_DE_FUNDO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
