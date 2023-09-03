using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class Alteracoes036 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorSubdivisao",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorPostal",
                table: "Master",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorNome",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorEndereco",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorCidade",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorPostal",
                table: "Master",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorNome",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorEndereco",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorCidade",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorCargoAgenteLocalizacao",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioSubdivisao",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioPostal",
                table: "Master",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioNome",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioEndereco",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioCidade",
                table: "Master",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SignatarioNome",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorSubdivisao",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorPostal",
                table: "House",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorNome",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorEndereco",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorCidade",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioSubdivisao",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioPostal",
                table: "House",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioNome",
                table: "House",
                type: "varchar(70)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioEndereco",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioCidade",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaSubdivisao",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaPostal",
                table: "House",
                type: "varchar(35)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaNome",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaEndereco",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaCidade",
                table: "House",
                type: "varchar(70)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorSubdivisao",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorPostal",
                table: "Master",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorNome",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorEndereco",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorCidade",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorPostal",
                table: "Master",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorNome",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorEndereco",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorCidade",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmissorCargoAgenteLocalizacao",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioSubdivisao",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioPostal",
                table: "Master",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioNome",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioEndereco",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioCidade",
                table: "Master",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SignatarioNome",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorSubdivisao",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorPostal",
                table: "House",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorNome",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorEndereco",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpedidorCidade",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioSubdivisao",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioPostal",
                table: "House",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioNome",
                table: "House",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)");

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioEndereco",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsignatarioCidade",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaSubdivisao",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaPostal",
                table: "House",
                type: "varchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(35)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaNome",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaEndereco",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AgenteDeCargaCidade",
                table: "House",
                type: "varchar(60)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldNullable: true);
        }
    }
}
