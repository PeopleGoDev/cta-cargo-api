using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortoIataDestinoId",
                table: "VooTrecho",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_PortoIataDestinoId",
                table: "VooTrecho",
                column: "PortoIataDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VooTrecho_PortoIATA_PortoIataDestinoId",
                table: "VooTrecho",
                column: "PortoIataDestinoId",
                principalTable: "PortoIATA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VooTrecho_PortoIATA_PortoIataDestinoId",
                table: "VooTrecho");

            migrationBuilder.DropIndex(
                name: "IX_VooTrecho_PortoIataDestinoId",
                table: "VooTrecho");

            migrationBuilder.DropColumn(
                name: "PortoIataDestinoId",
                table: "VooTrecho");
        }
    }
}
