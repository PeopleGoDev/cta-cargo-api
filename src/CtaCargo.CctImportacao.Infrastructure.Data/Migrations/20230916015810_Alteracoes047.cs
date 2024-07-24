using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes047 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightAirportOfDestiny",
                table: "Master",
                type: "varchar(3)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FlightEstimatedArrival",
                table: "Master",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightAirportOfDestiny",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "FlightEstimatedArrival",
                table: "Master");
        }
    }
}
