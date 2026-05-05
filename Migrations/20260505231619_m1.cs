using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConsultaExterna.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    IdConsulta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoCita = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "text", nullable: false),
                    Anamenesis = table.Column<string>(type: "text", nullable: false),
                    ExamenFisico = table.Column<string>(type: "text", nullable: false),
                    PlanTratamiento = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.IdConsulta);
                });

            migrationBuilder.CreateTable(
                name: "TipoDiagnostico",
                columns: table => new
                {
                    IdTipoDiagnostico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDiagnostico", x => x.IdTipoDiagnostico);
                });

            migrationBuilder.CreateTable(
                name: "NotaEvaluacion",
                columns: table => new
                {
                    IdNotaEvaluacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    IdConsulta = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaEvaluacion", x => x.IdNotaEvaluacion);
                    table.ForeignKey(
                        name: "FK_NotaEvaluacion_Consulta_IdConsulta",
                        column: x => x.IdConsulta,
                        principalTable: "Consulta",
                        principalColumn: "IdConsulta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiagnosticoConsulta",
                columns: table => new
                {
                    IdDiagnosticoConsulta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoFarmacia = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    IdConsulta = table.Column<int>(type: "integer", nullable: false),
                    IdTipoDiagnostico = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticoConsulta", x => x.IdDiagnosticoConsulta);
                    table.ForeignKey(
                        name: "FK_DiagnosticoConsulta_Consulta_IdConsulta",
                        column: x => x.IdConsulta,
                        principalTable: "Consulta",
                        principalColumn: "IdConsulta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosticoConsulta_TipoDiagnostico_IdTipoDiagnostico",
                        column: x => x.IdTipoDiagnostico,
                        principalTable: "TipoDiagnostico",
                        principalColumn: "IdTipoDiagnostico",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoConsulta_IdConsulta",
                table: "DiagnosticoConsulta",
                column: "IdConsulta");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticoConsulta_IdTipoDiagnostico",
                table: "DiagnosticoConsulta",
                column: "IdTipoDiagnostico");

            migrationBuilder.CreateIndex(
                name: "IX_NotaEvaluacion_IdConsulta",
                table: "NotaEvaluacion",
                column: "IdConsulta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosticoConsulta");

            migrationBuilder.DropTable(
                name: "NotaEvaluacion");

            migrationBuilder.DropTable(
                name: "TipoDiagnostico");

            migrationBuilder.DropTable(
                name: "Consulta");
        }
    }
}
