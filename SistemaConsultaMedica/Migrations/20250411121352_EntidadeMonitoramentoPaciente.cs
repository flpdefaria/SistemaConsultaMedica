using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaConsultaMedica.Migrations
{
    /// <inheritdoc />
    public partial class EntidadeMonitoramentoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformacoesComplementaresPaciente_Pacientes_IdPaciente",
                table: "InformacoesComplementaresPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InformacoesComplementaresPaciente",
                table: "InformacoesComplementaresPaciente");

            migrationBuilder.RenameTable(
                name: "InformacoesComplementaresPaciente",
                newName: "InformacoesComplementaresPacientes");

            migrationBuilder.RenameIndex(
                name: "IX_InformacoesComplementaresPaciente_IdPaciente",
                table: "InformacoesComplementaresPacientes",
                newName: "IX_InformacoesComplementaresPacientes_IdPaciente");

            migrationBuilder.AlterColumn<string>(
                name: "MedicamentosEmUso",
                table: "InformacoesComplementaresPacientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CirurgiasRealizadas",
                table: "InformacoesComplementaresPacientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alergias",
                table: "InformacoesComplementaresPacientes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InformacoesComplementaresPacientes",
                table: "InformacoesComplementaresPacientes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MonitoramentoPaciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PressaoArterial = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Temperatura = table.Column<decimal>(type: "decimal(3,1)", nullable: false),
                    SaturacaoOxigenio = table.Column<byte>(type: "TINYINT", nullable: false),
                    FrequenciaCardiaca = table.Column<byte>(type: "TINYINT", nullable: false),
                    DataAfericao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoramentoPaciente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoramentoPaciente_Pacientes_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoramentoPaciente_IdPaciente",
                table: "MonitoramentoPaciente",
                column: "IdPaciente");

            migrationBuilder.AddForeignKey(
                name: "FK_InformacoesComplementaresPacientes_Pacientes_IdPaciente",
                table: "InformacoesComplementaresPacientes",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InformacoesComplementaresPacientes_Pacientes_IdPaciente",
                table: "InformacoesComplementaresPacientes");

            migrationBuilder.DropTable(
                name: "MonitoramentoPaciente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InformacoesComplementaresPacientes",
                table: "InformacoesComplementaresPacientes");

            migrationBuilder.RenameTable(
                name: "InformacoesComplementaresPacientes",
                newName: "InformacoesComplementaresPaciente");

            migrationBuilder.RenameIndex(
                name: "IX_InformacoesComplementaresPacientes_IdPaciente",
                table: "InformacoesComplementaresPaciente",
                newName: "IX_InformacoesComplementaresPaciente_IdPaciente");

            migrationBuilder.AlterColumn<string>(
                name: "MedicamentosEmUso",
                table: "InformacoesComplementaresPaciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CirurgiasRealizadas",
                table: "InformacoesComplementaresPaciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Alergias",
                table: "InformacoesComplementaresPaciente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InformacoesComplementaresPaciente",
                table: "InformacoesComplementaresPaciente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InformacoesComplementaresPaciente_Pacientes_IdPaciente",
                table: "InformacoesComplementaresPaciente",
                column: "IdPaciente",
                principalTable: "Pacientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
