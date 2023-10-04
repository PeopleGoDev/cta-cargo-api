using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes043 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoErroDeletionRFB",
                table: "House",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataChecagemDeletionRFB",
                table: "House",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataProtocoloDeletionRFB",
                table: "House",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoErroDeletionRFB",
                table: "House",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProtocoloDeletionRFB",
                table: "House",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SituacaoDeletionRFBId",
                table: "House",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoErroDeletionRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "DataChecagemDeletionRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "DataProtocoloDeletionRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "DescricaoErroDeletionRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "ProtocoloDeletionRFB",
                table: "House");

            migrationBuilder.DropColumn(
                name: "SituacaoDeletionRFBId",
                table: "House");
        }
    }
}
