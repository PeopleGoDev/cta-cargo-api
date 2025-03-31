using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes067 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterHouseAssociationChildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterHouseAssociationId = table.Column<int>(type: "int", nullable: false),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CriadoPeloId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SubmetidoPeloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterHouseAssociationChildren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_MasterHouseAssociacao_MasterHouseAssociationId",
                        column: x => x.MasterHouseAssociationId,
                        principalTable: "MasterHouseAssociacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociationChildren_Usuario_SubmetidoPeloId",
                        column: x => x.SubmetidoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_CriadoPeloId",
                table: "MasterHouseAssociationChildren",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_EmpresaId_MasterHouseAssociationId_HouseId_DataExclusao",
                table: "MasterHouseAssociationChildren",
                columns: new[] { "EmpresaId", "MasterHouseAssociationId", "HouseId", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_HouseId",
                table: "MasterHouseAssociationChildren",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_MasterHouseAssociationId",
                table: "MasterHouseAssociationChildren",
                column: "MasterHouseAssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_ModificadoPeloId",
                table: "MasterHouseAssociationChildren",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociationChildren_SubmetidoPeloId",
                table: "MasterHouseAssociationChildren",
                column: "SubmetidoPeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterHouseAssociationChildren");
        }
    }
}
