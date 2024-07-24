using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes063 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_EmpresaId_EMail_DataExclusao",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId_Account_DataExclusao",
                table: "Usuario",
                columns: new[] { "EmpresaId", "Account", "DataExclusao" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuario_EmpresaId_Account_DataExclusao",
                table: "Usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId_EMail_DataExclusao",
                table: "Usuario",
                columns: new[] { "EmpresaId", "EMail", "DataExclusao" },
                unique: true);
        }
    }
}
