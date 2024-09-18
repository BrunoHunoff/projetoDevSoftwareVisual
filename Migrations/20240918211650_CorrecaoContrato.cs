using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoApiSoftwareVisual.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficios_Contratos_ContratoId",
                table: "Beneficios");

            migrationBuilder.DropIndex(
                name: "IX_Beneficios_ContratoId",
                table: "Beneficios");

            migrationBuilder.DropColumn(
                name: "ContratoId",
                table: "Beneficios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContratoId",
                table: "Beneficios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficios_ContratoId",
                table: "Beneficios",
                column: "ContratoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficios_Contratos_ContratoId",
                table: "Beneficios",
                column: "ContratoId",
                principalTable: "Contratos",
                principalColumn: "Id");
        }
    }
}
