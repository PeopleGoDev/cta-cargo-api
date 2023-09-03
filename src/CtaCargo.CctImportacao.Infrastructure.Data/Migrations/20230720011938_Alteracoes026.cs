using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes026 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfiguracaoAdicional",
                table: "Configura",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfiguracaoAdicional",
                table: "Configura");
        }
    }
}
