using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VooTrecho_DataExclusao_VooId",
                table: "VooTrecho");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_DataExclusao_VooId",
                table: "VooTrecho",
                columns: new[] { "DataExclusao", "VooId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VooTrecho_DataExclusao_VooId",
                table: "VooTrecho");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_DataExclusao_VooId",
                table: "VooTrecho",
                columns: new[] { "DataExclusao", "VooId" },
                unique: true);
        }
    }
}
