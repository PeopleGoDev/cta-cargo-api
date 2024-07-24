using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes039 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "Voo",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "Voo",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "ULDMaster",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "ULDMaster",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "MasterHouseAssociacao",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "MasterHouseAssociacao",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "Master",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "Master",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "House",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMode",
                table: "House",
                type: "varchar(30)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Environment",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "House");

            migrationBuilder.DropColumn(
                name: "InputMode",
                table: "House");
        }
    }
}
