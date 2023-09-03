using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_MasterId_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_MasterId",
                table: "ULDMaster",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster",
                columns: new[] { "VooId", "MasterNumero", "ULDCaracteristicaCodigo", "ULDId", "ULDIdPrimario", "DataExclusao" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_MasterId",
                table: "ULDMaster");

            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_MasterId_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster",
                columns: new[] { "MasterId", "ULDCaracteristicaCodigo", "ULDId", "ULDIdPrimario", "DataExclusao" },
                unique: true);
        }
    }
}
