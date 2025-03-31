using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes064 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CarrierDeclarationDate",
                table: "MasterHouseAssociacao",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MessageSubmitFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FilePurpose = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    ProtocolNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    ErrorCode = table.Column<string>(type: "varchar(40)", nullable: true),
                    ErrorDescription = table.Column<string>(type: "varchar(250)", nullable: true),
                    Content = table.Column<string>(type: "varchar(max)", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CriadoPeloId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    SubmetidoPeloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSubmitFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSubmitFile_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSubmitFile_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSubmitFile_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageSubmitFile_Usuario_SubmetidoPeloId",
                        column: x => x.SubmetidoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_CriadoPeloId",
                table: "MessageSubmitFile",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_EmpresaId",
                table: "MessageSubmitFile",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_FilePurpose_SourceId",
                table: "MessageSubmitFile",
                columns: new[] { "FilePurpose", "SourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_ModificadoPeloId",
                table: "MessageSubmitFile",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_ProtocolNumber",
                table: "MessageSubmitFile",
                column: "ProtocolNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageSubmitFile_SubmetidoPeloId",
                table: "MessageSubmitFile",
                column: "SubmetidoPeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageSubmitFile");

            migrationBuilder.DropColumn(
                name: "CarrierDeclarationDate",
                table: "MasterHouseAssociacao");
        }
    }
}
