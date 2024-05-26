using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_NEW_COLUMN_CODIGO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CODIGO",
                table: "PRODUTO",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "CODIGO",
                table: "PERMISSAO",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CODIGO",
                table: "PRODUTO");

            migrationBuilder.DropColumn(
                name: "CODIGO",
                table: "PERMISSAO");
        }
    }
}
