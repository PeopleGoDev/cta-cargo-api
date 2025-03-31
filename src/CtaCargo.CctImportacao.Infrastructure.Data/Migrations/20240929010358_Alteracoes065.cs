using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes065 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgenteDeCargaId",
                table: "MasterHouseAssociacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessDate",
                table: "MasterHouseAssociacao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_AgenteDeCargaId",
                table: "MasterHouseAssociacao",
                column: "AgenteDeCargaId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterHouseAssociacao_AgenteDeCarga_AgenteDeCargaId",
                table: "MasterHouseAssociacao",
                column: "AgenteDeCargaId",
                principalTable: "AgenteDeCarga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterHouseAssociacao_AgenteDeCarga_AgenteDeCargaId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropIndex(
                name: "IX_MasterHouseAssociacao_AgenteDeCargaId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "AgenteDeCargaId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "ProcessDate",
                table: "MasterHouseAssociacao");
        }
    }
}
