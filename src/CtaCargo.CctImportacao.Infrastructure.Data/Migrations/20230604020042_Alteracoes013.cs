using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalParcial",
                table: "Master");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Empresa",
                type: "varchar(14)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "TotalParcial",
                table: "Master",
                type: "varchar(1)",
                nullable: true);
        }
    }
}
