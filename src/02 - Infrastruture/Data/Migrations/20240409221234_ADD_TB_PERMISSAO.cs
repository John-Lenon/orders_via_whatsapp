using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TB_PERMISSAO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERMISSAO",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(
                        type: "VARCHAR(50)",
                        maxLength: 50,
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSAO", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PERMISSAO");
        }
    }
}
