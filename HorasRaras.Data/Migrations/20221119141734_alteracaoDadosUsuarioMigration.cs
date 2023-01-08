using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasRaras.Data.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoDadosUsuarioMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "HashSenhaExpiracao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashSenhaExpiracao",
                table: "Usuarios");
        }
    }
}
