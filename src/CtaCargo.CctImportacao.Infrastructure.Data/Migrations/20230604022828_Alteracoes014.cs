using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Empresa",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Empresa");

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Empresa",
                type: "varchar(14)",
                nullable: true);
        }
    }
}
