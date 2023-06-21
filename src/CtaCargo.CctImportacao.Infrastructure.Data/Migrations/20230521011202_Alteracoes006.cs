using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataChecagemAssociacaoRFB",
                table: "House",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReenviarAssociacao",
                table: "House",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataChecagemAssociacaoRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ReenviarAssociacao",
                table: "House");
        }
    }
}
