using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_RELACAO_TB_USUARIO_PERMISSAO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_PERMISSAO",
                columns: table => new
                {
                    USUARIO_ID = table.Column<int>(type: "INT", nullable: false),
                    PERMISSAO_ID = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_USUARIO_PERMISSAO",
                        x => new { x.USUARIO_ID, x.PERMISSAO_ID }
                    );
                    table.ForeignKey(
                        name: "FK_USUARIO_PERMISSAO_PERMISSAO_PERMISSAO_ID",
                        column: x => x.PERMISSAO_ID,
                        principalTable: "PERMISSAO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_USUARIO_PERMISSAO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_PERMISSAO_PERMISSAO_ID",
                table: "USUARIO_PERMISSAO",
                column: "PERMISSAO_ID"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "USUARIO_PERMISSAO");
        }
    }
}
