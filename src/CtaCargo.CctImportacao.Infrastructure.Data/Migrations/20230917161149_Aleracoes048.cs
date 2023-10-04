using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Aleracoes048 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AutenticacaoSignatariaLocal",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AutenticacaoSignatariaLocal",
                table: "Master",
                type: "varchar(3)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);
        }
    }
}
