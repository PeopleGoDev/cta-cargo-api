using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class Master : BaseEntity
{
    public enum RFStatusEnvioType
    {
        NoSubmitted = 0,
        Received = 1,
        Processed = 2,
        Rejected = 3,
        ReceivedDeletion = 4,
        ProcessedDeletion = 5
    }

    [Key]
    [Required]
    public int Id { get; set; }
    public int? VooId { get; set; }
    public virtual Voo VooInfo { get; set; }
    [Required]
    [Column(TypeName = "varchar(15)")]
    public string Numero { get; set; }
    [Column(TypeName = "varchar(1)")]
    public string CodigoConteudo { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string ProdutoSituacaoAlfandega { get; set; }
    public double PesoTotalBruto { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string PesoTotalBrutoUN { get; set; }
    public int TotalPecas { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFOB { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFOBUN { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFretePP { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFretePPUN { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFreteFC { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFreteFCUN { get; set; }
    public bool IndicadorMadeiraMacica { get; set; }
    public bool IndicadorNaoDesunitizacao { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string DescricaoMercadoria { get; set; }
    [Column(TypeName = "varchar(7)")]
    public string CodigoRecintoAduaneiro { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string RUC { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ExpedidorNome { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ExpedidorEndereco { get; set; }
    [Column(TypeName = "varchar(35)")]
    public string ExpedidorPostal { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ExpedidorCidade { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string ExpedidorPaisCodigo { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ExpedidorSubdivisao { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ConsignatarioNome { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ConsignatarioEndereco { get; set; }
    [Column(TypeName = "varchar(35)")]
    public string ConsignatarioPostal { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ConsignatarioCidade { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string ConsignatarioPaisCodigo { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string ConsignatarioSubdivisao { get; set; }
    [Column(TypeName = "varchar(14)")]
    public string ConsignatarioCNPJ { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string EmissorNome { get; set; }
    [Column(TypeName = "varchar(35)")]
    public string EmissorPostal { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string EmissorEndereco { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string EmissorCidade { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string EmissorPaisCodigo { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string EmissorCargoAgenteLocalizacao { get; set; }
    public int? AeroportoOrigemId { get; set; }
    [ForeignKey("AeroportoOrigemId")]
    public virtual PortoIata AeroportoOrigemInfo { get; set; }
    public int? AeroportoDestinoId { get; set; }
    [ForeignKey("AeroportoDestinoId")]
    public virtual PortoIata AeroportoDestinoInfo { get; set; }
    public ICollection<MasterInstrucaoManuseio> InstrucoesManuseios { get; set; }
    [Column(TypeName = "varchar(250)")]
    public string  OutrasInstrucoesManuseio { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string CodigoManuseioProdutoAlgandega { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string MoedaAplicadaOrigem { get; set; }
    // Campos da importação do XML
    public double PesoTotalBrutoXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string PesoTotalBrutoUNXML { get; set; }
    public int TotalPecasXML { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFOBXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFOBUNXML { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFretePPXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFretePPUNXML { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFreteFCXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFreteFCUNXML { get; set; }
    // Campos de comunicação RFB
    [Column(TypeName = "varchar(30)")]
    public string NumeroDocumentoRFB { get; set; }
    public int StatusId { get; set; } // "Aguardando Confirmação Saida Voo", "Pronto para Envio Receita"
    public RFStatusEnvioType SituacaoRFBId { get; set; } // “Received” , “Rejected”, “Processed”
    [Column(TypeName = "varchar(50)")]
    public string ProtocoloRFB { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string CodigoErroRFB { get; set; }
    [Column(TypeName = "varchar(550)")]
    public string DescricaoErroRFB { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
    public bool MeiaEntradaXML { get; set; }
    public DateTime? DataEmissaoXML { get; set; }
    public string VooNumeroXML { get; set; }
    public int CiaAereaId { get; set; }
    [ForeignKey("CiaAereaId")]
    public virtual CiaAerea CiaAereaInfo { get; set; }
    public DateTime? DataProcessadoRFB { get; set; }
    public DateTime? DataProtocoloRFB { get; set; }
    public DateTime? DataChecagemRFB { get; set; }
    [Column(TypeName = "varchar(max)")]
    public string NCMLista { get; set; }
    public string[] GetNCMLista()
    {
        if (string.IsNullOrEmpty(NCMLista))
        {
            return new string[0];
        }
        else
        {
            return NCMLista.Split(",");
        }
    }
    [Column(TypeName = "datetime")]
    public DateTime? AutenticacaoSignatarioData { get; set; }
    [Column(TypeName = "varchar(60)")]
    public string AutenticacaoSignatariaNome { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string AutenticacaoSignatariaLocal { get; set; }
    public virtual List<UldMaster> ULDs { get; set; } = new List<UldMaster>();
    public virtual List<ErroMaster> ErrosMaster { get; set; } = new List<ErroMaster>();
    public string getULDDetail()
    {
        string uld = "";
        foreach (var item in ULDs)
        {
            if (uld != "")
                uld += "\n";
            uld += item.ULDCaracteristicaCodigo + item.ULDId + item.ULDIdPrimario;
        }
        return uld;
    }
    public int CountUldValidas()
    {
        if (ULDs != null)
        {
            int result = 0;
            foreach (var item in ULDs)
            {
                if (item.DataExclusao == null)
                    result++;
            }
            return result;
        }
        return 0;
    }
    [Column(TypeName = "varchar(3)")]
    public string AeroportoOrigemCodigo { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string AeroportoDestinoCodigo { get; set; }
    [Column(TypeName = "varchar(120)")]
    public string NaturezaCarga { get; set; }
    public string[] GetNaturezaCargaLista()
    {
        if (string.IsNullOrEmpty(NaturezaCarga))
            return new string[0];

            return NaturezaCarga.Split(",");
    }
    public int? NaturezaCargaId { get; set; }
    [ForeignKey("NaturezaCargaId")]
    public virtual NaturezaCarga NaturezaCargaInfo { get; set; }
    public bool Reenviar { get; set; }
    public double? Volume { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string VolumeUN { get; set; }
    public bool IndicadorAwbNaoIata { get; set; }
    [Column(TypeName = "varchar(1)")]
    public string TotalParcial { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string InputMode { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string Environment { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? XmlIssueDate { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string FlightAirportOfDestiny { get;set; }
    public DateTime? FlightEstimatedArrival { get; set; }
    [Column(TypeName = "varchar(15)")]
    public string StatusCodeRFB { get; set; }
}
