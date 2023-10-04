using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes045 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "VooTrecho",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryOrigin",
                table: "Voo",
                type: "varchar(2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "Voo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "ULDMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "ReceitaFederalTransacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "PortoIATA",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "NaturezaCarga",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "MasterHouseAssociacao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "Master",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "House",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "FileImportDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "FileImport",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "Fatura",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "Configura",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "CnpjCliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "CiaAerea",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "CertificadoDigital",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubmetidoPeloId",
                table: "AgenteDeCarga",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VooTrecho_SubmetidoPeloId",
                table: "VooTrecho",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_ParentFlightId",
                table: "Voo",
                column: "ParentFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_SubmetidoPeloId",
                table: "Voo",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_SubmetidoPeloId",
                table: "ULDMaster",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFederalTransacao_SubmetidoPeloId",
                table: "ReceitaFederalTransacao",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_PortoIATA_SubmetidoPeloId",
                table: "PortoIATA",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_NaturezaCarga_SubmetidoPeloId",
                table: "NaturezaCarga",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterHouseAssociacao_SubmetidoPeloId",
                table: "MasterHouseAssociacao",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_SubmetidoPeloId",
                table: "Master",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_House_SubmetidoPeloId",
                table: "House",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImportDetails_SubmetidoPeloId",
                table: "FileImportDetails",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_FileImport_SubmetidoPeloId",
                table: "FileImport",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_SubmetidoPeloId",
                table: "Fatura",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Configura_SubmetidoPeloId",
                table: "Configura",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CnpjCliente_SubmetidoPeloId",
                table: "CnpjCliente",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_SubmetidoPeloId",
                table: "Cliente",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CiaAerea_SubmetidoPeloId",
                table: "CiaAerea",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDigital_SubmetidoPeloId",
                table: "CertificadoDigital",
                column: "SubmetidoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_AgenteDeCarga_SubmetidoPeloId",
                table: "AgenteDeCarga",
                column: "SubmetidoPeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgenteDeCarga_Usuario_SubmetidoPeloId",
                table: "AgenteDeCarga",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CertificadoDigital_Usuario_SubmetidoPeloId",
                table: "CertificadoDigital",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CiaAerea_Usuario_SubmetidoPeloId",
                table: "CiaAerea",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Usuario_SubmetidoPeloId",
                table: "Cliente",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CnpjCliente_Usuario_SubmetidoPeloId",
                table: "CnpjCliente",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Configura_Usuario_SubmetidoPeloId",
                table: "Configura",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fatura_Usuario_SubmetidoPeloId",
                table: "Fatura",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileImport_Usuario_SubmetidoPeloId",
                table: "FileImport",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FileImportDetails_Usuario_SubmetidoPeloId",
                table: "FileImportDetails",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_Usuario_SubmetidoPeloId",
                table: "House",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Master_Usuario_SubmetidoPeloId",
                table: "Master",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterHouseAssociacao_Usuario_SubmetidoPeloId",
                table: "MasterHouseAssociacao",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NaturezaCarga_Usuario_SubmetidoPeloId",
                table: "NaturezaCarga",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PortoIATA_Usuario_SubmetidoPeloId",
                table: "PortoIATA",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaFederalTransacao_Usuario_SubmetidoPeloId",
                table: "ReceitaFederalTransacao",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ULDMaster_Usuario_SubmetidoPeloId",
                table: "ULDMaster",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voo_Usuario_SubmetidoPeloId",
                table: "Voo",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voo_Voo_ParentFlightId",
                table: "Voo",
                column: "ParentFlightId",
                principalTable: "Voo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VooTrecho_Usuario_SubmetidoPeloId",
                table: "VooTrecho",
                column: "SubmetidoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgenteDeCarga_Usuario_SubmetidoPeloId",
                table: "AgenteDeCarga");

            migrationBuilder.DropForeignKey(
                name: "FK_CertificadoDigital_Usuario_SubmetidoPeloId",
                table: "CertificadoDigital");

            migrationBuilder.DropForeignKey(
                name: "FK_CiaAerea_Usuario_SubmetidoPeloId",
                table: "CiaAerea");

            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Usuario_SubmetidoPeloId",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_CnpjCliente_Usuario_SubmetidoPeloId",
                table: "CnpjCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Configura_Usuario_SubmetidoPeloId",
                table: "Configura");

            migrationBuilder.DropForeignKey(
                name: "FK_Fatura_Usuario_SubmetidoPeloId",
                table: "Fatura");

            migrationBuilder.DropForeignKey(
                name: "FK_FileImport_Usuario_SubmetidoPeloId",
                table: "FileImport");

            migrationBuilder.DropForeignKey(
                name: "FK_FileImportDetails_Usuario_SubmetidoPeloId",
                table: "FileImportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_House_Usuario_SubmetidoPeloId",
                table: "House");

            migrationBuilder.DropForeignKey(
                name: "FK_Master_Usuario_SubmetidoPeloId",
                table: "Master");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterHouseAssociacao_Usuario_SubmetidoPeloId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropForeignKey(
                name: "FK_NaturezaCarga_Usuario_SubmetidoPeloId",
                table: "NaturezaCarga");

            migrationBuilder.DropForeignKey(
                name: "FK_PortoIATA_Usuario_SubmetidoPeloId",
                table: "PortoIATA");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaFederalTransacao_Usuario_SubmetidoPeloId",
                table: "ReceitaFederalTransacao");

            migrationBuilder.DropForeignKey(
                name: "FK_ULDMaster_Usuario_SubmetidoPeloId",
                table: "ULDMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Voo_Usuario_SubmetidoPeloId",
                table: "Voo");

            migrationBuilder.DropForeignKey(
                name: "FK_Voo_Voo_ParentFlightId",
                table: "Voo");

            migrationBuilder.DropForeignKey(
                name: "FK_VooTrecho_Usuario_SubmetidoPeloId",
                table: "VooTrecho");

            migrationBuilder.DropIndex(
                name: "IX_VooTrecho_SubmetidoPeloId",
                table: "VooTrecho");

            migrationBuilder.DropIndex(
                name: "IX_Voo_ParentFlightId",
                table: "Voo");

            migrationBuilder.DropIndex(
                name: "IX_Voo_SubmetidoPeloId",
                table: "Voo");

            migrationBuilder.DropIndex(
                name: "IX_ULDMaster_SubmetidoPeloId",
                table: "ULDMaster");

            migrationBuilder.DropIndex(
                name: "IX_ReceitaFederalTransacao_SubmetidoPeloId",
                table: "ReceitaFederalTransacao");

            migrationBuilder.DropIndex(
                name: "IX_PortoIATA_SubmetidoPeloId",
                table: "PortoIATA");

            migrationBuilder.DropIndex(
                name: "IX_NaturezaCarga_SubmetidoPeloId",
                table: "NaturezaCarga");

            migrationBuilder.DropIndex(
                name: "IX_MasterHouseAssociacao_SubmetidoPeloId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropIndex(
                name: "IX_Master_SubmetidoPeloId",
                table: "Master");

            migrationBuilder.DropIndex(
                name: "IX_House_SubmetidoPeloId",
                table: "House");

            migrationBuilder.DropIndex(
                name: "IX_FileImportDetails_SubmetidoPeloId",
                table: "FileImportDetails");

            migrationBuilder.DropIndex(
                name: "IX_FileImport_SubmetidoPeloId",
                table: "FileImport");

            migrationBuilder.DropIndex(
                name: "IX_Fatura_SubmetidoPeloId",
                table: "Fatura");

            migrationBuilder.DropIndex(
                name: "IX_Configura_SubmetidoPeloId",
                table: "Configura");

            migrationBuilder.DropIndex(
                name: "IX_CnpjCliente_SubmetidoPeloId",
                table: "CnpjCliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_SubmetidoPeloId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_CiaAerea_SubmetidoPeloId",
                table: "CiaAerea");

            migrationBuilder.DropIndex(
                name: "IX_CertificadoDigital_SubmetidoPeloId",
                table: "CertificadoDigital");

            migrationBuilder.DropIndex(
                name: "IX_AgenteDeCarga_SubmetidoPeloId",
                table: "AgenteDeCarga");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "VooTrecho");

            migrationBuilder.DropColumn(
                name: "CountryOrigin",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "ULDMaster");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "ReceitaFederalTransacao");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "PortoIATA");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "NaturezaCarga");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "MasterHouseAssociacao");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "Master");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "House");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "FileImportDetails");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "FileImport");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "Fatura");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "Configura");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "CnpjCliente");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "CiaAerea");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "CertificadoDigital");

            migrationBuilder.DropColumn(
                name: "SubmetidoPeloId",
                table: "AgenteDeCarga");
        }
    }
}
