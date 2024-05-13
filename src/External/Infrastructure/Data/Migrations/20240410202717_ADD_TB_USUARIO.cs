using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TB_USUARIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table
                        .Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CODIGO = table.Column<Guid>(
                        type: "uniqueidentifier",
                        nullable: false,
                        defaultValueSql: "NEWID()"
                    ),
                    NOME = table.Column<string>(
                        type: "VARCHAR(80)",
                        maxLength: 80,
                        nullable: false
                    ),
                    EMAIL = table.Column<string>(
                        type: "VARCHAR(50)",
                        maxLength: 50,
                        nullable: false
                    ),
                    TELEFONE = table.Column<string>(
                        type: "VARCHAR(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    ATIVO = table.Column<bool>(type: "BIT", nullable: false),
                    SENHA_HASH = table.Column<string>(
                        type: "VARCHAR(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    CODIGO_UNICO_SENHA = table.Column<string>(
                        type: "VARCHAR(100)",
                        maxLength: 100,
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "USUARIO");
        }
    }
}
