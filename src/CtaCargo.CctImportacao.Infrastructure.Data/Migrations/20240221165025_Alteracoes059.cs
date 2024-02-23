using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes059 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoErroDeletionRFB",
                table: "Master",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataChecagemDeletionRFB",
                table: "Master",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataProtocoloDeletionRFB",
                table: "Master",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescricaoErroDeletionRFB",
                table: "Master",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProtocoloDeletionRFB",
                table: "Master",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SituacaoDeletionRFBId",
                table: "Master",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoErroDeletionRFB",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "DataChecagemDeletionRFB",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "DataProtocoloDeletionRFB",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "DescricaoErroDeletionRFB",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "ProtocoloDeletionRFB",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "SituacaoDeletionRFBId",
                table: "Master");
        }
    }
}
