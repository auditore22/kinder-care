using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kinder_care.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEstadoAddPresente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "asistencia");

            migrationBuilder.DropColumn(
                name: "hora_entrada",
                table: "asistencia");

            migrationBuilder.DropColumn(
                name: "hora_salida",
                table: "asistencia");

            migrationBuilder.AlterColumn<DateTime>(
                name: "fecha",
                table: "asistencia",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<bool>(
                name: "presente",
                table: "asistencia",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "presente",
                table: "asistencia");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "fecha",
                table: "asistencia",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "estado",
                table: "asistencia",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "hora_entrada",
                table: "asistencia",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "hora_salida",
                table: "asistencia",
                type: "time",
                nullable: true);
        }
    }
}
