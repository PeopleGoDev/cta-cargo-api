using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes054 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Voo_CiaAereaId_DataVoo_Numero_DataExclusao",
                table: "Voo");

            migrationBuilder.AddColumn<int>(
                name: "FlightType",
                table: "Voo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Voo_CiaAereaId_DataVoo_Numero_FlightType_DataExclusao",
                table: "Voo",
                columns: new[] { "CiaAereaId", "DataVoo", "Numero", "FlightType", "DataExclusao" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Voo_CiaAereaId_DataVoo_Numero_FlightType_DataExclusao",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "FlightType",
                table: "Voo");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_CiaAereaId_DataVoo_Numero_DataExclusao",
                table: "Voo",
                columns: new[] { "CiaAereaId", "DataVoo", "Numero", "DataExclusao" },
                unique: true);
        }
    }
}
