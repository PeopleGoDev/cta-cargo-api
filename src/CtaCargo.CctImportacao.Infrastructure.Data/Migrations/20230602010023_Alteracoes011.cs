using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodigoRecintoAduaneiro",
                table: "Master",
                type: "varchar(7)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CodigoRecintoAduaneiro",
                table: "House",
                type: "varchar(7)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CodigoRecintoAduaneiro",
                table: "Master",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CodigoRecintoAduaneiro",
                table: "House",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)");
        }
    }
}
