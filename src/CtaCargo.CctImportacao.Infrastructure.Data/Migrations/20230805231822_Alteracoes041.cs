using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes041 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoErroDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataChecagemDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataProtocoloDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoErroDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProtocoloDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SituacaoDeletionAssociacaoRFBId",
                table: "MasterHouseAssociacao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoErroDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "DataChecagemDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "DataProtocoloDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "DescricaoErroDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "ProtocoloDeletionAssociacaoRFB",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "SituacaoDeletionAssociacaoRFBId",
                table: "MasterHouseAssociacao");
        }
    }
}
