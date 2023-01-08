using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorasRaras.Data.Migrations
{
    /// <inheritdoc />
    public partial class alteracoesAtributosMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHoras",
                table: "Tarefas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraFinal",
                table: "Tarefas",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<float>(
                name: "HorasTotal",
                table: "Tarefas",
                type: "real",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projetos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_Nome",
                table: "Projetos",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projetos_Nome",
                table: "Projetos");

            migrationBuilder.DropColumn(
                name: "HorasTotal",
                table: "Tarefas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraFinal",
                table: "Tarefas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalHoras",
                table: "Tarefas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Projetos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
