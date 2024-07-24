using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes042 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraSaidaAtual",
                table: "VooTrecho",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "XmlIssueDate",
                table: "Voo",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tranferencia",
                table: "ULDMaster",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "XmlIssueDate",
                table: "MasterHouseAssociacao",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "XmlIssueDate",
                table: "Master",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "XmlIssueDate",
                table: "House",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataHoraSaidaAtual",
                table: "VooTrecho");

            migrationBuilder.DropColumn(
                name: "XmlIssueDate",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "Tranferencia",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "XmlIssueDate",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "XmlIssueDate",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "XmlIssueDate",
                table: "House");
        }
    }
}
