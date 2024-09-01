using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TB_ENDERECO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_ENDERECO",
                table: "EMPRESA",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ENDERECO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CEP = table.Column<string>(type: "varchar(8)", nullable: true),
                    UF = table.Column<string>(type: "char(2)", nullable: false),
                    BAIRRO = table.Column<string>(type: "varchar(30)", nullable: false),
                    CIDADE = table.Column<string>(type: "varchar(30)", nullable: false),
                    LOGRADOURO = table.Column<string>(type: "varchar(50)", nullable: false),
                    NUMERO_LOGRADOURO = table.Column<int>(type: "int", nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "varchar(30)", nullable: true),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESA_ID_ENDERECO",
                table: "EMPRESA",
                column: "ID_ENDERECO",
                unique: true,
                filter: "[ID_ENDERECO] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_EMPRESA_ENDERECO_ID_ENDERECO",
                table: "EMPRESA",
                column: "ID_ENDERECO",
                principalTable: "ENDERECO",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMPRESA_ENDERECO_ID_ENDERECO",
                table: "EMPRESA");

            migrationBuilder.DropTable(
                name: "ENDERECO");

            migrationBuilder.DropIndex(
                name: "IX_EMPRESA_ID_ENDERECO",
                table: "EMPRESA");

            migrationBuilder.DropColumn(
                name: "ID_ENDERECO",
                table: "EMPRESA");
        }
    }
}
