using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alteracoes069 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_House_MasterNumeroXML_Numero_DataExclusao",
                table: "House");

            migrationBuilder.CreateIndex(
                name: "IX_House_MasterNumeroXML_Numero_DataExclusao",
                table: "House",
                columns: new[] { "MasterNumeroXML", "Numero", "DataExclusao" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_House_MasterNumeroXML_Numero_DataExclusao",
                table: "House");

            migrationBuilder.CreateIndex(
                name: "IX_House_MasterNumeroXML_Numero_DataExclusao",
                table: "House",
                columns: new[] { "MasterNumeroXML", "Numero", "DataExclusao" },
                unique: true);
        }
    }
}
