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
            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_Codigo",
                table: "MasterInstrucaoManuseio");

            //migrationBuilder.DropIndex(
            //    name: "IX_MasterInstrucaoManuseio_MasterId",
            //    table: "MasterInstrucaoManuseio");

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "MasterInstrucaoManuseio",
                type: "datetime",
                nullable: true);

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

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HouseInstrucaoManuseio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(3)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseInstrucaoManuseio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseInstrucaoManuseio_House_HouseId",
                        column: x => x.HouseId,
                        principalTable: "House",
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
                name: "IX_MasterInstrucaoManuseio_MasterId_DataExclusao",
                table: "MasterInstrucaoManuseio",
                columns: new[] { "MasterId", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_ModificadoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_SubmetidoPeloId",
                table: "MasterInstrucaoManuseio",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseInstrucaoManuseio_HouseId",
                table: "HouseInstrucaoManuseio",
                column: "HouseId");

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
                name: "HouseInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_CriadoPeloId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_EmpresaId",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.DropIndex(
                name: "IX_MasterInstrucaoManuseio_MasterId_DataExclusao",
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
                name: "DataExclusao",
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

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "MasterInstrucaoManuseio");

            migrationBuilder.AlterColumn<int>(
                name: "MasterId",
                table: "MasterInstrucaoManuseio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_Codigo",
                table: "MasterInstrucaoManuseio",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_MasterId",
                table: "MasterInstrucaoManuseio",
                column: "MasterId");
        }
    }
}
