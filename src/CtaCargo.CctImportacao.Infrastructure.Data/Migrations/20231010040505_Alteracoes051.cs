using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes051 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrefixoAeronave",
                table: "Voo",
                type: "varchar(35)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrossVolumeMeasureUnit",
                table: "ULDMaster",
                type: "varchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossVolumeMeasureValue",
                table: "ULDMaster",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortOfDestiny",
                table: "ULDMaster",
                type: "varchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortOfOrign",
                table: "ULDMaster",
                type: "varchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryDescription",
                table: "ULDMaster",
                type: "varchar(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrefixoAeronave",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "GrossVolumeMeasureUnit",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "GrossVolumeMeasureValue",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "PortOfDestiny",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "PortOfOrign",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "SummaryDescription",
                table: "ULDMaster");
        }
    }
}
