using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasRaras.Data.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoUsuarioProjetoEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsuarioProjetos");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "UsuarioProjetos");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "UsuarioProjetos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioProjetos");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracao",
                table: "UsuarioProjetos");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "UsuarioProjetos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsuarioProjetos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "UsuarioProjetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "UsuarioProjetos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioProjetos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioAlteracao",
                table: "UsuarioProjetos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioInclusao",
                table: "UsuarioProjetos",
                type: "int",
                nullable: true);
        }
    }
}
