using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes058 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Empresa",
                type: "varchar(14)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(14)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "CiaAerea",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contato",
                table: "CiaAerea",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "CiaAerea",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "CiaAerea",
                type: "varchar(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "CiaAerea");

            migrationBuilder.DropColumn(
                name: "Contato",
                table: "CiaAerea");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "CiaAerea");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "CiaAerea");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Empresa",
                type: "varchar(14)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(14)");
        }
    }
}
