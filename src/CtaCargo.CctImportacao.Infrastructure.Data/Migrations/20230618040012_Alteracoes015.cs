using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MasterHouseAssociacaoId",
                table: "House",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MasterHouseAssociacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    MasterNumber = table.Column<string>(type: "varchar(15)", nullable: true),
                    OriginLocation = table.Column<string>(type: "varchar(3)", nullable: true),
                    FinalDestinationLocation = table.Column<string>(type: "varchar(3)", nullable: true),
                    ConsigmentItemQuantity = table.Column<int>(nullable: true),
                    PackageQuantity = table.Column<int>(nullable: true),
                    TotalPieceQuantity = table.Column<int>(nullable: true),
                    MessageHeaderDocumentId = table.Column<string>(type: "varchar(50)", nullable: true),
                    ProtocoloAssociacaoRFB = table.Column<string>(type: "varchar(50)", nullable: true),
                    CodigoErroAssociacaoRFB = table.Column<string>(type: "varchar(40)", nullable: true),
                    DescricaoErroAssociacaoRFB = table.Column<string>(type: "text", nullable: true),
                    SituacaoAssociacaoRFBId = table.Column<int>(nullable: false),
                    DataProtocoloAssociacaoRFB = table.Column<DateTime>(nullable: true),
                    DataChecagemAssociacaoRFB = table.Column<DateTime>(nullable: true),
                    ReenviarAssociacao = table.Column<bool>(nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterHouseAssociacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociacao_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociacao_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterHouseAssociacao_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_House_MasterHouseAssociacaoId",
                table: "House",
                column: "MasterHouseAssociacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_CriadoPeloId",
                table: "MasterHouseAssociacao",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_EmpresaId",
                table: "MasterHouseAssociacao",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_ModificadoPeloId",
                table: "MasterHouseAssociacao",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_DataExclusao_MasterNumber",
                table: "MasterHouseAssociacao",
                columns: new[] { "DataExclusao", "MasterNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_House_MasterHouseAssociacao_MasterHouseAssociacaoId",
                table: "House",
                column: "MasterHouseAssociacaoId",
                principalTable: "MasterHouseAssociacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_House_MasterHouseAssociacao_MasterHouseAssociacaoId",
                table: "House");

            migrationBuilder.DropTable(
                name: "MasterHouseAssociacao");

            migrationBuilder.DropIndex(
                name: "IX_House_MasterHouseAssociacaoId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "MasterHouseAssociacaoId",
                table: "House");
        }
    }
}
