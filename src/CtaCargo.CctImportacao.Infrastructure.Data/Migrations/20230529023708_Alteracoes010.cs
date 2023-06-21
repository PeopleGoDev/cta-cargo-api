using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NCMs_Descricao",
                table: "NCMs");

            migrationBuilder.AddColumn<bool>(
                name: "Seleciona",
                table: "NCMs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_NCMs_Seleciona_Descricao",
                table: "NCMs",
                columns: new[] { "Seleciona", "Descricao" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NCMs_Seleciona_Descricao",
                table: "NCMs");

            migrationBuilder.DropColumn(
                name: "Seleciona",
                table: "NCMs");

            migrationBuilder.CreateIndex(
                name: "IX_NCMs_Descricao",
                table: "NCMs",
                column: "Descricao");
        }
    }
}
