using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes029 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TotalParcial",
                table: "ULDMaster",
                type: "varchar(1)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PesoUN",
                table: "ULDMaster",
                type: "varchar(3)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PesoUN",
                table: "ULDMaster");

            migrationBuilder.AlterColumn<string>(
                name: "TotalParcial",
                table: "ULDMaster",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldNullable: true);
        }
    }
}
