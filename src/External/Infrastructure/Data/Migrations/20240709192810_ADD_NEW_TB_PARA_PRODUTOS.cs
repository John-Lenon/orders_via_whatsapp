using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_NEW_TB_PARA_PRODUTOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CATEGORIA_ADICIONAL_PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    NOME = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    PRIORIDADE = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_ADICIONAL_PRODUTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA_PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    PRIORIDADE = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_PRODUTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ADICIONAL_PRODUTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CATEGORIA_ADC_PRODUTO = table.Column<int>(type: "int", nullable: false),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    PRIORIDADE = table.Column<int>(type: "int", nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADICIONAL_PRODUTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ADICIONAL_PRODUTO_CATEGORIA_ADICIONAL_PRODUTO_ID_CATEGORIA_ADC_PRODUTO",
                        column: x => x.ID_CATEGORIA_ADC_PRODUTO,
                        principalTable: "CATEGORIA_ADICIONAL_PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_E_ADICIONAL_PRODUTO",
                columns: table => new
                {
                    PRODUTO_ID = table.Column<int>(type: "int", nullable: false),
                    ADICIONAL_PRODUTO_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_E_ADICIONAL_PRODUTO", x => new { x.PRODUTO_ID, x.ADICIONAL_PRODUTO_ID });
                    table.ForeignKey(
                        name: "FK_PRODUTO_E_ADICIONAL_PRODUTO_ADICIONAL_PRODUTO_ADICIONAL_PRODUTO_ID",
                        column: x => x.ADICIONAL_PRODUTO_ID,
                        principalTable: "ADICIONAL_PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUTO_E_ADICIONAL_PRODUTO_PRODUTO_PRODUTO_ID",
                        column: x => x.PRODUTO_ID,
                        principalTable: "PRODUTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_CATEGORIA_ID",
                table: "PRODUTO",
                column: "CATEGORIA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ADICIONAL_PRODUTO_ID_CATEGORIA_ADC_PRODUTO",
                table: "ADICIONAL_PRODUTO",
                column: "ID_CATEGORIA_ADC_PRODUTO");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUTO_E_ADICIONAL_PRODUTO_ADICIONAL_PRODUTO_ID",
                table: "PRODUTO_E_ADICIONAL_PRODUTO",
                column: "ADICIONAL_PRODUTO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUTO_CATEGORIA_PRODUTO_CATEGORIA_ID",
                table: "PRODUTO",
                column: "CATEGORIA_ID",
                principalTable: "CATEGORIA_PRODUTO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUTO_CATEGORIA_PRODUTO_CATEGORIA_ID",
                table: "PRODUTO");

            migrationBuilder.DropTable(
                name: "CATEGORIA_PRODUTO");

            migrationBuilder.DropTable(
                name: "PRODUTO_E_ADICIONAL_PRODUTO");

            migrationBuilder.DropTable(
                name: "ADICIONAL_PRODUTO");

            migrationBuilder.DropTable(
                name: "CATEGORIA_ADICIONAL_PRODUTO");

            migrationBuilder.DropIndex(
                name: "IX_PRODUTO_CATEGORIA_ID",
                table: "PRODUTO");
        }
    }
}
