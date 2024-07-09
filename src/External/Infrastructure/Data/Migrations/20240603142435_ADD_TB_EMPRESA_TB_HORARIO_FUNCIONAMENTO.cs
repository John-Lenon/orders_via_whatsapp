using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_TB_EMPRESA_TB_HORARIO_FUNCIONAMENTO : Migration
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
                    ENDERECO_DO_LOGOTIPO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ENDERECO_DA_CAPA_DE_FUNDO = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    STATUS_DE_FUNCIONAMENTO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HORARIO_FUNCIONAMENTO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HORA = table.Column<int>(type: "int", nullable: false),
                    MINUTOS = table.Column<int>(type: "int", nullable: false),
                    DIA_DA_SEMANA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMPRESA_ID = table.Column<int>(type: "int", nullable: false),
                    CODIGO = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HORARIO_FUNCIONAMENTO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HORARIO_FUNCIONAMENTO_EMPRESA_EMPRESA_ID",
                        column: x => x.EMPRESA_ID,
                        principalTable: "EMPRESA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HORARIO_FUNCIONAMENTO_EMPRESA_ID",
                table: "HORARIO_FUNCIONAMENTO",
                column: "EMPRESA_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HORARIO_FUNCIONAMENTO");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
