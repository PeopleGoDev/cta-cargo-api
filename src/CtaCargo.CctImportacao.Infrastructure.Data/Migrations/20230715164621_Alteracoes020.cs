using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VooTrecho_PortoIATA_PortoDestinoId",
                table: "VooTrecho");

            migrationBuilder.DropIndex(
                name: "IX_VooTrecho_PortoDestinoId",
                table: "VooTrecho");

            migrationBuilder.DropColumn(
                name: "PortoDestinoId",
                table: "VooTrecho");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PortoDestinoId",
                table: "VooTrecho",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_PortoDestinoId",
                table: "VooTrecho",
                column: "PortoDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VooTrecho_PortoIATA_PortoDestinoId",
                table: "VooTrecho",
                column: "PortoDestinoId",
                principalTable: "PortoIATA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
