using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes032 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoNumero",
                table: "NCMs",
                type: "VARCHAR(8)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NCMs_Seleciona_CodigoNumero",
                table: "NCMs",
                columns: new[] { "Seleciona", "CodigoNumero" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NCMs_Seleciona_CodigoNumero",
                table: "NCMs");

            migrationBuilder.DropColumn(
                name: "CodigoNumero",
                table: "NCMs");
        }
    }
}
