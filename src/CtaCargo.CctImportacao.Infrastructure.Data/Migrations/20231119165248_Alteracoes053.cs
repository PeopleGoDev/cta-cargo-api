using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes053 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProtocoloScheduleRFB",
                table: "Voo",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScheduleErrorCodeRFB",
                table: "Voo",
                type: "varchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScheduleErrorDescriptionRFB",
                table: "Voo",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleSituationRFB",
                table: "Voo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProtocoloScheduleRFB",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "ScheduleErrorCodeRFB",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "ScheduleErrorDescriptionRFB",
                table: "Voo");

            migrationBuilder.DropColumn(
                name: "ScheduleSituationRFB",
                table: "Voo");
        }
    }
}
