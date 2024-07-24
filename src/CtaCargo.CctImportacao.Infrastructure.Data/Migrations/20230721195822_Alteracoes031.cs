using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes031 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster");

            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooTrechoId",
                table: "ULDMaster");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooTrechoId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster",
                columns: new[] { "VooTrechoId", "MasterNumero", "ULDCaracteristicaCodigo", "ULDId", "ULDIdPrimario", "DataExclusao" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooTrechoId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooId_MasterNumero_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster",
                columns: new[] { "VooId", "MasterNumero", "ULDCaracteristicaCodigo", "ULDId", "ULDIdPrimario", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooTrechoId",
                table: "ULDMaster",
                column: "VooTrechoId");
        }
    }
}
