using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoErroAssociacaoRFB",
                table: "House",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoErroAssociacaoRFB",
                table: "House",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProtocoloAssociacaoRFB",
                table: "House",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SituacaoAssociacaoRFBId",
                table: "House",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoErroAssociacaoRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "DescricaoErroAssociacaoRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ProtocoloAssociacaoRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "SituacaoAssociacaoRFBId",
                table: "House");
        }
    }
}
