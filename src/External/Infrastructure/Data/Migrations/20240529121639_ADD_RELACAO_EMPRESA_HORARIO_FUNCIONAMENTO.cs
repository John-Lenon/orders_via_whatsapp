using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_RELACAO_EMPRESA_HORARIO_FUNCIONAMENTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HORARIO_FUNCIONAMENTO_EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO",
                column: "EMPRESA_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_HORARIO_FUNCIONAMENTO_EMPRESA_EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO",
                column: "EMPRESA_ID",
                principalTable: "EMPRESA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HORARIO_FUNCIONAMENTO_EMPRESA_EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO");

            migrationBuilder.DropIndex(
                name: "IX_HORARIO_FUNCIONAMENTO_EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO");

            migrationBuilder.DropColumn(
                name: "EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO");
        }
    }
}
