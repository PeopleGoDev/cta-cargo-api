using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazaoSocial = table.Column<string>(type: "varchar(60)", nullable: false),
                    Contato = table.Column<string>(type: "varchar(50)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(25)", nullable: false),
                    EMail = table.Column<string>(type: "varchar(150)", nullable: false),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(60)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: false),
                    UF = table.Column<string>(type: "varchar(2)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(2)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "House",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    AgenteDeCargaId = table.Column<int>(nullable: true),
                    Numero = table.Column<string>(type: "varchar(15)", nullable: true),
                    NumeroAgenteDeCarga = table.Column<string>(type: "varchar(10)", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime", nullable: false),
                    PesoTotalBruto = table.Column<double>(nullable: false),
                    PesoTotalBrutoUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalVolumes = table.Column<int>(nullable: false),
                    ValorFretePP = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFretePPUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFreteFC = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFreteFCUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    IndicadorMadeiraMacica = table.Column<bool>(nullable: false),
                    DescricaoMercadoria = table.Column<string>(type: "varchar(100)", nullable: true),
                    CodigoRecintoAduaneiro = table.Column<int>(nullable: false),
                    RUC = table.Column<string>(type: "varchar(60)", nullable: true),
                    SignatarioNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    SignatarioAssinante = table.Column<string>(type: "varchar(35)", nullable: true),
                    SignatarioAssinaturaDataHora = table.Column<DateTime>(type: "datetime", nullable: true),
                    SignatarioAssinaturaLocal = table.Column<string>(type: "varchar(3)", nullable: true),
                    ExpedidorNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    ExpedidorCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    ExpedidorSubdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioNome = table.Column<string>(type: "varchar(60)", nullable: false),
                    ConsignatarioEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    ConsignatarioCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    ConsignatarioSubdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioCNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    AgenteDeCargaNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    AgenteDeCargaPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    AgenteDeCargaEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    AgenteDeCargaCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    AgenteDeCargaPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    AgenteDeCargaSubdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    AeroportoOrigemId = table.Column<int>(nullable: true),
                    AeroportoDestinoId = table.Column<int>(nullable: true),
                    OutrasInstrucoesManuseio = table.Column<string>(type: "varchar(150)", nullable: true),
                    MoedaAplicadaOrigem = table.Column<string>(type: "varchar(3)", nullable: true),
                    PesoTotalBrutoXML = table.Column<double>(nullable: false),
                    PesoTotalBrutoUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalVolumesXML = table.Column<int>(nullable: false),
                    ValorFretePPXML = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFretePPUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFreteFCXML = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFreteFCUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    SituacaoRFBId = table.Column<int>(nullable: false),
                    DataProcessadoRFB = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataEmissaoXML = table.Column<DateTime>(type: "datetime", nullable: true),
                    MasterNumeroXML = table.Column<string>(type: "varchar(15)", nullable: false),
                    DataProtocoloRFB = table.Column<DateTime>(nullable: true),
                    DataChecagemRFB = table.Column<DateTime>(nullable: true),
                    NCMLista = table.Column<string>(type: "varchar(max)", nullable: true),
                    ProtocoloRFB = table.Column<string>(type: "varchar(50)", nullable: true),
                    CodigoErroRFB = table.Column<string>(type: "varchar(40)", nullable: true),
                    DescricaoErroRFB = table.Column<string>(type: "varchar(550)", nullable: true),
                    AeroportoOrigemCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    AeroportoDestinoCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    Reenviar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_House", x => x.Id);
                    table.ForeignKey(
                        name: "FK_House_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgenteDeCarga",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(60)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(60)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: false),
                    UF = table.Column<string>(type: "varchar(3)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(2)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    CertificadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenteDeCarga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgenteDeCarga_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CiaAerea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(60)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(60)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: false),
                    UF = table.Column<string>(type: "varchar(3)", nullable: false),
                    Pais = table.Column<string>(type: "varchar(2)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    CertificadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiaAerea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CiaAerea_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    EMail = table.Column<string>(type: "varchar(150)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(20)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(30)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(30)", nullable: false),
                    CiaAereaId = table.Column<int>(nullable: true),
                    CiaAereaNome = table.Column<string>(type: "varchar(50)", nullable: true),
                    AlteraCia = table.Column<bool>(nullable: false),
                    AcessaUsuarios = table.Column<bool>(nullable: false),
                    AcessaClientes = table.Column<bool>(nullable: false),
                    AcessaCiasAereas = table.Column<bool>(nullable: false),
                    Bloqueado = table.Column<bool>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioSistema = table.Column<bool>(nullable: false),
                    AlterarSenha = table.Column<bool>(nullable: false),
                    DataReset = table.Column<DateTime>(type: "datetime", nullable: true),
                    CertificadoId = table.Column<int>(nullable: true),
                    CertificadoDigitalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CertificadoDigital",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Arquivo = table.Column<string>(type: "varchar(120)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    NomeDono = table.Column<string>(type: "varchar(120)", nullable: true),
                    NumeroDocumento = table.Column<string>(type: "varchar(60)", nullable: true),
                    Senha = table.Column<string>(type: "varchar(45)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    SerialNumber = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificadoDigital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CertificadoDigital_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoDigital_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CertificadoDigital_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CnpjCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CnpjCliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CnpjCliente_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CnpjCliente_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CnpjCliente_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConfiguracaoTipo = table.Column<string>(type: "varchar(20)", nullable: false),
                    ConfiguracaoNome = table.Column<string>(type: "varchar(20)", nullable: false),
                    ConfiguracaoValor = table.Column<string>(type: "varchar(255)", nullable: false),
                    CiaAereaId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Configura_CiaAerea_CiaAereaId",
                        column: x => x.CiaAereaId,
                        principalTable: "CiaAerea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configura_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configura_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configura_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Configura_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoFatura = table.Column<string>(type: "varchar(30)", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataExclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fatura_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fatura_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fatura_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NaturezaCarga",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(3)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturezaCarga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NaturezaCarga_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NaturezaCarga_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NaturezaCarga_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortoIATA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(3)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    SiglaPais = table.Column<string>(type: "varchar(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortoIATA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortoIATA_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortoIATA_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortoIATA_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Endereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    Postal = table.Column<string>(type: "varchar(15)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    PaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    Subdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    CnpjId = table.Column<int>(nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_CnpjCliente_CnpjId",
                        column: x => x.CnpjId,
                        principalTable: "CnpjCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cliente_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceitaFederalTransacao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoArquivo = table.Column<string>(type: "varchar(10)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(15)", nullable: false),
                    SituacaoRFBId = table.Column<int>(nullable: false),
                    DataProcessadoRFB = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProtocoloRFB = table.Column<string>(type: "varchar(50)", nullable: true),
                    CodigoErroRFB = table.Column<string>(type: "varchar(40)", nullable: true),
                    FaturaId = table.Column<string>(type: "varchar(550)", nullable: true),
                    NumeroFatura = table.Column<int>(nullable: true),
                    DescricaoErroRFB = table.Column<string>(nullable: true),
                    Xml = table.Column<string>(type: "TEXT", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceitaFederalTransacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceitaFederalTransacao_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaFederalTransacao_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaFederalTransacao_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceitaFederalTransacao_Fatura_NumeroFatura",
                        column: x => x.NumeroFatura,
                        principalTable: "Fatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Voo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    CiaAereaId = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(type: "varchar(15)", nullable: false),
                    PortoIataOrigemId = table.Column<int>(nullable: true),
                    PortoIataDestinoId = table.Column<int>(nullable: true),
                    TotalPesoBruto = table.Column<double>(nullable: true),
                    TotalPesoBrutoUnidade = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalVolumeBruto = table.Column<double>(nullable: true),
                    TotalVolumeBrutoUnidade = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalPacotes = table.Column<int>(nullable: true),
                    TotalPecas = table.Column<int>(nullable: true),
                    DataVoo = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataHoraSaidaEstimada = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraSaidaReal = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraChegadaEstimada = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataHoraChegadaReal = table.Column<DateTime>(type: "datetime", nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    SituacaoRFBId = table.Column<int>(nullable: false),
                    ProtocoloRFB = table.Column<string>(type: "varchar(50)", nullable: true),
                    CodigoErroRFB = table.Column<string>(type: "varchar(40)", nullable: true),
                    DescricaoErroRFB = table.Column<string>(type: "varchar(250)", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataEmissaoXML = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataProtocoloRFB = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataChecagemRFB = table.Column<DateTime>(type: "datetime", nullable: true),
                    AeroportoOrigemCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    AeroportoDestinoCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    Reenviar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voo_CiaAerea_CiaAereaId",
                        column: x => x.CiaAereaId,
                        principalTable: "CiaAerea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_PortoIATA_PortoIataDestinoId",
                        column: x => x.PortoIataDestinoId,
                        principalTable: "PortoIATA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Voo_PortoIATA_PortoIataOrigemId",
                        column: x => x.PortoIataOrigemId,
                        principalTable: "PortoIATA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    VooId = table.Column<int>(nullable: true),
                    Numero = table.Column<string>(type: "varchar(15)", nullable: false),
                    CodigoConteudo = table.Column<string>(type: "varchar(1)", nullable: true),
                    ProdutoSituacaoAlfandega = table.Column<string>(type: "varchar(2)", nullable: true),
                    PesoTotalBruto = table.Column<double>(nullable: false),
                    PesoTotalBrutoUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalPecas = table.Column<int>(nullable: false),
                    ValorFOB = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFOBUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFretePP = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFretePPUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFreteFC = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFreteFCUN = table.Column<string>(type: "varchar(3)", nullable: true),
                    IndicadorMadeiraMacica = table.Column<bool>(nullable: false),
                    IndicadorNaoDesunitizacao = table.Column<bool>(nullable: false),
                    DescricaoMercadoria = table.Column<string>(type: "varchar(100)", nullable: true),
                    CodigoRecintoAduaneiro = table.Column<int>(nullable: false),
                    RUC = table.Column<string>(type: "varchar(40)", nullable: true),
                    ExpedidorNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    ExpedidorCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    ExpedidorPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    ExpedidorSubdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    ConsignatarioCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    ConsignatarioSubdivisao = table.Column<string>(type: "varchar(60)", nullable: true),
                    ConsignatarioCNPJ = table.Column<string>(type: "varchar(14)", nullable: true),
                    EmissorNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    EmissorPostal = table.Column<string>(type: "varchar(15)", nullable: true),
                    EmissorEndereco = table.Column<string>(type: "varchar(60)", nullable: true),
                    EmissorCidade = table.Column<string>(type: "varchar(60)", nullable: true),
                    EmissorPaisCodigo = table.Column<string>(type: "varchar(2)", nullable: true),
                    EmissorCargoAgenteLocalizacao = table.Column<string>(type: "varchar(60)", nullable: true),
                    AeroportoOrigemId = table.Column<int>(nullable: true),
                    AeroportoDestinoId = table.Column<int>(nullable: true),
                    OutrasInstrucoesManuseio = table.Column<string>(type: "varchar(150)", nullable: true),
                    CodigoManuseioProdutoAlgandega = table.Column<string>(type: "varchar(2)", nullable: true),
                    MoedaAplicadaOrigem = table.Column<string>(type: "varchar(3)", nullable: true),
                    PesoTotalBrutoXML = table.Column<double>(nullable: false),
                    PesoTotalBrutoUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalPecasXML = table.Column<int>(nullable: false),
                    ValorFOBXML = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFOBUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFretePPXML = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFretePPUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    ValorFreteFCXML = table.Column<decimal>(type: "MONEY", nullable: false),
                    ValorFreteFCUNXML = table.Column<string>(type: "varchar(3)", nullable: true),
                    NumeroDocumentoRFB = table.Column<string>(type: "varchar(30)", nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    SituacaoRFBId = table.Column<int>(nullable: false),
                    ProtocoloRFB = table.Column<string>(type: "varchar(50)", nullable: true),
                    CodigoErroRFB = table.Column<string>(type: "varchar(40)", nullable: true),
                    DescricaoErroRFB = table.Column<string>(type: "varchar(550)", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    MeiaEntradaXML = table.Column<bool>(nullable: false),
                    DataEmissaoXML = table.Column<DateTime>(nullable: true),
                    VooNumeroXML = table.Column<string>(nullable: true),
                    CiaAereaId = table.Column<int>(nullable: false),
                    DataProcessadoRFB = table.Column<DateTime>(nullable: true),
                    DataProtocoloRFB = table.Column<DateTime>(nullable: true),
                    DataChecagemRFB = table.Column<DateTime>(nullable: true),
                    NCMLista = table.Column<string>(type: "varchar(max)", nullable: true),
                    AutenticacaoSignatarioData = table.Column<DateTime>(type: "datetime", nullable: true),
                    AutenticacaoSignatariaNome = table.Column<string>(type: "varchar(60)", nullable: true),
                    AutenticacaoSignatariaLocal = table.Column<string>(type: "varchar(3)", nullable: true),
                    TotalParcial = table.Column<string>(type: "varchar(1)", nullable: true),
                    AeroportoOrigemCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    AeroportoDestinoCodigo = table.Column<string>(type: "varchar(3)", nullable: true),
                    NaturezaCarga = table.Column<string>(type: "varchar(3)", nullable: true),
                    NaturezaCargaId = table.Column<int>(nullable: true),
                    Reenviar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Master_PortoIATA_AeroportoDestinoId",
                        column: x => x.AeroportoDestinoId,
                        principalTable: "PortoIATA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_PortoIATA_AeroportoOrigemId",
                        column: x => x.AeroportoOrigemId,
                        principalTable: "PortoIATA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_CiaAerea_CiaAereaId",
                        column: x => x.CiaAereaId,
                        principalTable: "CiaAerea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_NaturezaCarga_NaturezaCargaId",
                        column: x => x.NaturezaCargaId,
                        principalTable: "NaturezaCarga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Master_Voo_VooId",
                        column: x => x.VooId,
                        principalTable: "Voo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErroMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MasterId = table.Column<int>(nullable: false),
                    Erro = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErroMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErroMaster_Master_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterInstrucaoManuseio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(3)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: false),
                    MasterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterInstrucaoManuseio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MasterInstrucaoManuseio_Master_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ULDMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaId = table.Column<int>(nullable: false),
                    CriadoPeloId = table.Column<int>(nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModificadoPeloId = table.Column<int>(nullable: true),
                    ModifiedDateTimeUtc = table.Column<DateTime>(type: "datetime", nullable: true),
                    VooId = table.Column<int>(nullable: false),
                    MasterNumero = table.Column<string>(type: "varchar(15)", nullable: true),
                    MasterId = table.Column<int>(nullable: true),
                    ULDId = table.Column<string>(type: "varchar(20)", nullable: true),
                    ULDCaracteristicaCodigo = table.Column<string>(type: "varchar(10)", nullable: true),
                    ULDObs = table.Column<string>(type: "varchar(50)", nullable: true),
                    ULDIdPrimario = table.Column<string>(type: "varchar(10)", nullable: true),
                    DataExclusao = table.Column<DateTime>(type: "datetime", nullable: true),
                    QuantidadePecas = table.Column<int>(nullable: true),
                    Peso = table.Column<decimal>(type: "MONEY", nullable: true),
                    TotalParcial = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ULDMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ULDMaster_Usuario_CriadoPeloId",
                        column: x => x.CriadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ULDMaster_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ULDMaster_Master_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Master",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ULDMaster_Usuario_ModificadoPeloId",
                        column: x => x.ModificadoPeloId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ULDMaster_Voo_VooId",
                        column: x => x.VooId,
                        principalTable: "Voo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgenteDeCarga_CertificadoId",
                table: "AgenteDeCarga",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgenteDeCarga_CriadoPeloId",
                table: "AgenteDeCarga",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_AgenteDeCarga_ModificadoPeloId",
                table: "AgenteDeCarga",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_AgenteDeCarga_EmpresaId_Numero_DataExclusao",
                table: "AgenteDeCarga",
                columns: new[] { "EmpresaId", "Numero", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDigital_CriadoPeloId",
                table: "CertificadoDigital",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDigital_ModificadoPeloId",
                table: "CertificadoDigital",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CertificadoDigital_EmpresaId_SerialNumber_DataExclusao",
                table: "CertificadoDigital",
                columns: new[] { "EmpresaId", "SerialNumber", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CiaAerea_CertificadoId",
                table: "CiaAerea",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CiaAerea_CriadoPeloId",
                table: "CiaAerea",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CiaAerea_ModificadoPeloId",
                table: "CiaAerea",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CiaAerea_EmpresaId_Numero_DataExclusao",
                table: "CiaAerea",
                columns: new[] { "EmpresaId", "Numero", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CnpjId",
                table: "Cliente",
                column: "CnpjId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CriadoPeloId",
                table: "Cliente",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_ModificadoPeloId",
                table: "Cliente",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EmpresaId_Nome_Endereco_Cidade_PaisCodigo_Postal_Subdivisao",
                table: "Cliente",
                columns: new[] { "EmpresaId", "Nome", "Endereco", "Cidade", "PaisCodigo", "Postal", "Subdivisao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CnpjCliente_CriadoPeloId",
                table: "CnpjCliente",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CnpjCliente_ModificadoPeloId",
                table: "CnpjCliente",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_CnpjCliente_EmpresaId_Cnpj",
                table: "CnpjCliente",
                columns: new[] { "EmpresaId", "Cnpj" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Configura_CiaAereaId",
                table: "Configura",
                column: "CiaAereaId");

            migrationBuilder.CreateIndex(
                name: "IX_Configura_CriadoPeloId",
                table: "Configura",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Configura_EmpresaId",
                table: "Configura",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Configura_ModificadoPeloId",
                table: "Configura",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Configura_UsuarioId",
                table: "Configura",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_RazaoSocial",
                table: "Empresa",
                column: "RazaoSocial");

            migrationBuilder.CreateIndex(
                name: "IX_ErroMaster_MasterId_DataExclusao",
                table: "ErroMaster",
                columns: new[] { "MasterId", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_CriadoPeloId",
                table: "Fatura",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_ModificadoPeloId",
                table: "Fatura",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_EmpresaId_TipoFatura_DataExclusao",
                table: "Fatura",
                columns: new[] { "EmpresaId", "TipoFatura", "DataExclusao" });

            migrationBuilder.CreateIndex(
                name: "IX_House_AeroportoDestinoId",
                table: "House",
                column: "AeroportoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_House_AeroportoOrigemId",
                table: "House",
                column: "AeroportoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_House_AgenteDeCargaId",
                table: "House",
                column: "AgenteDeCargaId");

            migrationBuilder.CreateIndex(
                name: "IX_House_CriadoPeloId",
                table: "House",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_House_EmpresaId",
                table: "House",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_House_ModificadoPeloId",
                table: "House",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_House_MasterNumeroXML_Numero_DataExclusao",
                table: "House",
                columns: new[] { "MasterNumeroXML", "Numero", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Master_AeroportoDestinoId",
                table: "Master",
                column: "AeroportoDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_AeroportoOrigemId",
                table: "Master",
                column: "AeroportoOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_CiaAereaId",
                table: "Master",
                column: "CiaAereaId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_CriadoPeloId",
                table: "Master",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_EmpresaId",
                table: "Master",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_ModificadoPeloId",
                table: "Master",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_NaturezaCargaId",
                table: "Master",
                column: "NaturezaCargaId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_VooId_Numero_DataExclusao",
                table: "Master",
                columns: new[] { "VooId", "Numero", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_Codigo",
                table: "MasterInstrucaoManuseio",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterInstrucaoManuseio_MasterId",
                table: "MasterInstrucaoManuseio",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_NaturezaCarga_CriadoPeloId",
                table: "NaturezaCarga",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_NaturezaCarga_ModificadoPeloId",
                table: "NaturezaCarga",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_NaturezaCarga_EmpresaId_Codigo_DataExclusao",
                table: "NaturezaCarga",
                columns: new[] { "EmpresaId", "Codigo", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortoIATA_CriadoPeloId",
                table: "PortoIATA",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_PortoIATA_ModificadoPeloId",
                table: "PortoIATA",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_PortoIATA_EmpresaId_Codigo_DataExclusao",
                table: "PortoIATA",
                columns: new[] { "EmpresaId", "Codigo", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFederalTransacao_CriadoPeloId",
                table: "ReceitaFederalTransacao",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFederalTransacao_ModificadoPeloId",
                table: "ReceitaFederalTransacao",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFederalTransacao_NumeroFatura",
                table: "ReceitaFederalTransacao",
                column: "NumeroFatura");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaFederalTransacao_EmpresaId_FaturaId",
                table: "ReceitaFederalTransacao",
                columns: new[] { "EmpresaId", "FaturaId" });

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_CriadoPeloId",
                table: "ULDMaster",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_EmpresaId",
                table: "ULDMaster",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_ModificadoPeloId",
                table: "ULDMaster",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_VooId",
                table: "ULDMaster",
                column: "VooId");

            migrationBuilder.CreateIndex(
                name: "IX_ULDMaster_MasterId_ULDCaracteristicaCodigo_ULDId_ULDIdPrimario_DataExclusao",
                table: "ULDMaster",
                columns: new[] { "MasterId", "ULDCaracteristicaCodigo", "ULDId", "ULDIdPrimario", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CertificadoDigitalId",
                table: "Usuario",
                column: "CertificadoDigitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_EmpresaId_EMail_DataExclusao",
                table: "Usuario",
                columns: new[] { "EmpresaId", "EMail", "DataExclusao" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voo_CriadoPeloId",
                table: "Voo",
                column: "CriadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_EmpresaId",
                table: "Voo",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_ModificadoPeloId",
                table: "Voo",
                column: "ModificadoPeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_PortoIataDestinoId",
                table: "Voo",
                column: "PortoIataDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_PortoIataOrigemId",
                table: "Voo",
                column: "PortoIataOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Voo_CiaAereaId_DataVoo_Numero_DataExclusao",
                table: "Voo",
                columns: new[] { "CiaAereaId", "DataVoo", "Numero", "DataExclusao" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_House_Usuario_CriadoPeloId",
                table: "House",
                column: "CriadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_Usuario_ModificadoPeloId",
                table: "House",
                column: "ModificadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_PortoIATA_AeroportoDestinoId",
                table: "House",
                column: "AeroportoDestinoId",
                principalTable: "PortoIATA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_PortoIATA_AeroportoOrigemId",
                table: "House",
                column: "AeroportoOrigemId",
                principalTable: "PortoIATA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_House_AgenteDeCarga_AgenteDeCargaId",
                table: "House",
                column: "AgenteDeCargaId",
                principalTable: "AgenteDeCarga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgenteDeCarga_CertificadoDigital_CertificadoId",
                table: "AgenteDeCarga",
                column: "CertificadoId",
                principalTable: "CertificadoDigital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgenteDeCarga_Usuario_CriadoPeloId",
                table: "AgenteDeCarga",
                column: "CriadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgenteDeCarga_Usuario_ModificadoPeloId",
                table: "AgenteDeCarga",
                column: "ModificadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CiaAerea_CertificadoDigital_CertificadoId",
                table: "CiaAerea",
                column: "CertificadoId",
                principalTable: "CertificadoDigital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CiaAerea_Usuario_CriadoPeloId",
                table: "CiaAerea",
                column: "CriadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CiaAerea_Usuario_ModificadoPeloId",
                table: "CiaAerea",
                column: "ModificadoPeloId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_CertificadoDigital_CertificadoDigitalId",
                table: "Usuario",
                column: "CertificadoDigitalId",
                principalTable: "CertificadoDigital",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_CertificadoDigital_CertificadoDigitalId",
                table: "Usuario");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Configura");

            migrationBuilder.DropTable(
                name: "ErroMaster");

            migrationBuilder.DropTable(
                name: "House");

            migrationBuilder.DropTable(
                name: "MasterInstrucaoManuseio");

            migrationBuilder.DropTable(
                name: "ReceitaFederalTransacao");

            migrationBuilder.DropTable(
                name: "ULDMaster");

            migrationBuilder.DropTable(
                name: "CnpjCliente");

            migrationBuilder.DropTable(
                name: "AgenteDeCarga");

            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "NaturezaCarga");

            migrationBuilder.DropTable(
                name: "Voo");

            migrationBuilder.DropTable(
                name: "CiaAerea");

            migrationBuilder.DropTable(
                name: "PortoIATA");

            migrationBuilder.DropTable(
                name: "CertificadoDigital");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
