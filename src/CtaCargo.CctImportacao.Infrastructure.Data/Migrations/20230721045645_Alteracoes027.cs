using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ULDMaster_Voo_VooId",
                table: "ULDMaster");

            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooId",
                table: "ULDMaster");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooId",
                table: "ULDMaster",
                column: "VooId");

            migrationBuilder.AddForeignKey(
                name: "FK_ULDMaster_Voo_VooId",
                table: "ULDMaster",
                column: "VooId",
                principalTable: "Voo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
