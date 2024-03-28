using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes061 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "Usuario",
                type: "varchar(150)",
                nullable: true);

            UpdateAccountColumn(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "Usuario");
            
        }

        private static void UpdateAccountColumn(MigrationBuilder migrationBuilder)
        {
            var sql = $@"UPDATE [dbo].[Usuario] SET Account = EMail";

            migrationBuilder.Sql($"EXEC('{sql}')");
        }
    }
}
