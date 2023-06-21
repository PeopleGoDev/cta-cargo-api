using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "Master",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VolumeUN",
                table: "Master",
                type: "varchar(3)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "VolumeUN",
                table: "Master");
        }
    }
}
