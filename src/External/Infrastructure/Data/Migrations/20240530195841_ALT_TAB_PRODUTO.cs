using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ALT_TAB_PRODUTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ATIVO",
                table: "PRODUTO");

            migrationBuilder.AddColumn<int>(
                name: "CATEGORIA_ID",
                table: "PRODUTO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "PRODUTO",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PRIORIDADE",
                table: "PRODUTO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "STATUS",
                table: "PRODUTO",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CATEGORIA_ID",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "PRIORIDADE",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "STATUS",
                table: "PRODUTO");

            migrationBuilder.AddColumn<int>(
                name: "ATIVO",
                table: "PRODUTO",
                type: "INT",
                nullable: false,
                defaultValue: 1);
        }
    }
}
