using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alteracoes070 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "MasterInstrucaoManuseio",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CriadoPeloId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModificadoPeloId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTimeUtc",
                table: "MasterInstrucaoManuseio",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HouseTratamentoEspecial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
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
                    table.PrimaryKey("PK_HouseTratamentoEspecial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseTratamentoEspecial_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseTratamentoEspecial_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseTratamentoEspecial_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseTratamentoEspecial_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HouseTratamentoEspecial_Usuario_SubmetidoPeloId",
                        column: x => x.SubmetidoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterTratamentoEspecial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<byte>(type: "tinyint", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
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
                    table.PrimaryKey("PK_MasterTratamentoEspecial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterTratamentoEspecial_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTratamentoEspecial_Master_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTratamentoEspecial_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTratamentoEspecial_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTratamentoEspecial_Usuario_SubmetidoPeloId",
                        column: x => x.SubmetidoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_CriadoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_EmpresaId",
                table: "MasterInstrucaoManuseio",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_ModificadoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_SubmetidoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTratamentoEspecial_CriadoPeloId",
                table: "HouseTratamentoEspecial",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTratamentoEspecial_EmpresaId",
                table: "HouseTratamentoEspecial",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTratamentoEspecial_HouseId",
                table: "HouseTratamentoEspecial",
                column: "HouseId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTratamentoEspecial_ModificadoPeloId",
                table: "HouseTratamentoEspecial",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseTratamentoEspecial_SubmetidoPeloId",
                table: "HouseTratamentoEspecial",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTratamentoEspecial_CriadoPeloId",
                table: "MasterTratamentoEspecial",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTratamentoEspecial_EmpresaId",
                table: "MasterTratamentoEspecial",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTratamentoEspecial_MasterId_DataExclusao",
                table: "MasterTratamentoEspecial",
                columns: new[] { "MasterId", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_MasterTratamentoEspecial_ModificadoPeloId",
                table: "MasterTratamentoEspecial",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTratamentoEspecial_SubmetidoPeloId",
                table: "MasterTratamentoEspecial",
                column: "SubmetidoPeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterInstrucaoManuseio_Empresa_EmpresaId",
                table: "MasterInstrucaoManuseio",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_CriadoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "CriadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_ModificadoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "ModificadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_SubmetidoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterInstrucaoManuseio_Empresa_EmpresaId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_CriadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_ModificadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterInstrucaoManuseio_Usuario_SubmetidoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropTable(
                name: "HouseTratamentoEspecial");

            migrationBuilder.DropTable(
                name: "MasterTratamentoEspecial");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_CriadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_EmpresaId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_ModificadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_SubmetidoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "CriadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "ModificadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTimeUtc",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "MasterInstrucaoManuseio");
        }
    }
}
