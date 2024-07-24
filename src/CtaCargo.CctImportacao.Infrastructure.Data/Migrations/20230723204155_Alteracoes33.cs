using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileImport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    FirstLineTitle = table.Column<bool>(type: "bit", nullable: false),
                    ContinueOnError = table.Column<bool>(type: "bit", nullable: false),
                    Configuration1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    Configuration2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CriadoPeloId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileImport_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileImport_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileImport_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileImportDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileImportId = table.Column<int>(type: "int", nullable: false),
                    Sequency = table.Column<int>(type: "int", nullable: false),
                    ColumnName = table.Column<string>(type: "varchar(255)", nullable: false),
                    ColumnAssociate = table.Column<string>(type: "varchar(255)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CriadoPeloId = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(type: "int", nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileImportDetails_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileImportDetails_FileImport_FileImportId",
                        column: x => x.FileImportId,
                        principalTable: "FileImport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileImportDetails_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileImportDetails_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_CriadoPeloId",
                table: "FileImport",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_EmpresaId_Id_DataExclusao",
                table: "FileImport",
                columns: new[] { "EmpresaId", "Id", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_EmpresaId_Type_DataExclusao",
                table: "FileImport",
                columns: new[] { "EmpresaId", "Type", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_ModificadoPeloId",
                table: "FileImport",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImportDetails_CriadoPeloId",
                table: "FileImportDetails",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImportDetails_EmpresaId",
                table: "FileImportDetails",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImportDetails_FileImportId",
                table: "FileImportDetails",
                column: "FileImportId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImportDetails_ModificadoPeloId",
                table: "FileImportDetails",
                column: "ModificadoPeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileImportDetails");

            migrationBuilder.DropTable(
                name: "FileImport");
        }
    }
}
