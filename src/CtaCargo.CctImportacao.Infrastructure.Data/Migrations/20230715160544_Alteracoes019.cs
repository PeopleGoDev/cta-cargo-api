using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VooTrechoId",
                table: "ULDMaster",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VooTrecho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VooId = table.Column<int>(type: "int", nullable: false),
                    PortoDestinoId = table.Column<int>(type: "int", nullable: true),
                    AeroportoDestinoCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    DataHoraSaidaEstimada = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CriadoPeloId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VooTrecho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VooTrecho_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VooTrecho_PortoIATA_PortoDestinoId",
                        column: x => x.PortoDestinoId,
                        principalTable: "PortoIATA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VooTrecho_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VooTrecho_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VooTrecho_Voo_VooId",
                        column: x => x.VooId,
                        principalTable: "Voo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooTrechoId",
                table: "ULDMaster",
                column: "VooTrechoId");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_CriadoPeloId",
                table: "VooTrecho",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_EmpresaId",
                table: "VooTrecho",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_ModificadoPeloId",
                table: "VooTrecho",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_PortoDestinoId",
                table: "VooTrecho",
                column: "PortoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_VooId",
                table: "VooTrecho",
                column: "VooId");

            migrationBuilder.AddForeignKey(
                name: "FK_ULDMaster_VooTrecho_VooTrechoId",
                table: "ULDMaster",
                column: "VooTrechoId",
                principalTable: "VooTrecho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ULDMaster_VooTrecho_VooTrechoId",
                table: "ULDMaster");

            migrationBuilder.DropTable(
                name: "VooTrecho");

            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_VooTrechoId",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "VooTrechoId",
                table: "ULDMaster");
        }
    }
}
