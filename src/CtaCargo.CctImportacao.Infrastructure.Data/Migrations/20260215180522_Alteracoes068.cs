using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alteracoes068 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorAgenteFC",
                table: "House",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorAgentePP",
                table: "House",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTransportadorFC",
                table: "House",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorTransportadorPP",
                table: "House",
                type: "MONEY",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorAgenteFC",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ValorAgentePP",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ValorTransportadorFC",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ValorTransportadorPP",
                table: "House");
        }
    }
}
