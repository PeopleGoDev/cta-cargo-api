using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class House : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    public int? AgenteDeCargaId { get; set; }
    [ForeignKey("AgenteDeCargaId")]
    public virtual AgenteDeCarga AgenteDeCargaInfo { get; set; }
    [Column(TypeName = "varchar(15)")]
    public string Numero { get; set; }
    [Required]
    [Column(TypeName = "varchar(15)")]
    public string NumeroAgenteDeCarga { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime DataProcessamento { get; set; } 
    public double PesoTotalBruto { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string PesoTotalBrutoUN { get; set; }
    [Required]
    public int TotalVolumes { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFretePP { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFretePPUN { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFreteFC { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFreteFCUN { get; set; }
    public bool IndicadorMadeiraMacica { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string DescricaoMercadoria { get; set; }
    [Column(TypeName = "varchar(7)")]
    public string CodigoRecintoAduaneiro { get; set; }
    [Column(TypeName = "varchar(60)")]
    public string RUC { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string SignatarioNome { get; set; }
    [Column(TypeName = "varchar(35)")]
    public string SignatarioAssinante { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? SignatarioAssinaturaDataHora { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string SignatarioAssinaturaLocal { get; set; }
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
    [Required]
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
    public string AgenteDeCargaNome { get; set; }
    [Column(TypeName = "varchar(35)")]
    public string AgenteDeCargaPostal { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string AgenteDeCargaEndereco { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string AgenteDeCargaCidade { get; set; }
    [Column(TypeName = "varchar(2)")]
    public string AgenteDeCargaPaisCodigo { get; set; }
    [Column(TypeName = "varchar(70)")]
    public string AgenteDeCargaSubdivisao { get; set; }
    public int? AeroportoOrigemId { get; set; }
    [ForeignKey("AeroportoOrigemId")]
    public virtual PortoIata AeroportoOrigemInfo { get; set; }
    public int? AeroportoDestinoId { get; set; }
    [ForeignKey("AeroportoDestinoId")]
    public virtual PortoIata AeroportoDestinoInfo { get; set; }
    [Column(TypeName = "varchar(150)")]
    public string OutrasInstrucoesManuseio { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string MoedaAplicadaOrigem { get; set; }
    // Campos arquivo XML
    public double PesoTotalBrutoXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string PesoTotalBrutoUNXML { get; set; }
    public int TotalVolumesXML { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFretePPXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFretePPUNXML { get; set; }
    [Column(TypeName = "MONEY")]
    public decimal ValorFreteFCXML { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string ValorFreteFCUNXML { get; set; }
    public int StatusId { get; set; }
    public int SituacaoRFBId { get; set; } // “Received” , “Rejected”, “Processed”
    [Column(TypeName = "datetime")]
    public DateTime? DataProcessadoRFB { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataEmissaoXML { get; set; }
    [Required]
    [Column(TypeName = "varchar(15)")]
    public string MasterNumeroXML { get; set; }
    public DateTime? DataProtocoloRFB { get; set; }
    public DateTime? DataChecagemRFB { get; set; }
    [Column(TypeName = "varchar(max)")]
    public string NCMLista { get; set; }
    public string[] NcmArray()
    {
        if (string.IsNullOrEmpty(NCMLista))
            return new string[0];

        return NCMLista.Split(",");
    }
    [Column(TypeName = "varchar(50)")]
    public string ProtocoloRFB { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string CodigoErroRFB { get; set; }
    [Column(TypeName = "text")]
    public string DescricaoErroRFB { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string AeroportoOrigemCodigo { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string AeroportoDestinoCodigo { get; set; }
    public bool Reenviar { get; set; }


    [Column(TypeName = "varchar(50)")]
    public string ProtocoloAssociacaoRFB { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string CodigoErroAssociacaoRFB { get; set; }
    [Column(TypeName = "text")]
    public string DescricaoErroAssociacaoRFB { get; set; }
    public int SituacaoAssociacaoRFBId { get; set; } // “Received” , “Rejected”, “Processed”
    public DateTime? DataProtocoloAssociacaoRFB { get; set; }
    public DateTime? DataChecagemAssociacaoRFB { get; set; }
    public bool ReenviarAssociacao { get; set; }
    public double? Volume { get; set; }
    [Column(TypeName = "varchar(3)")]
    public string VolumeUN { get; set; }
    public int? MasterHouseAssociacaoId { get; set; }
    [ForeignKey("MasterHouseAssociacaoId")]
    public virtual MasterHouseAssociacao MasterHouseAssociacaoIdInfo { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string InputMode { get; set; }
    [Column(TypeName = "varchar(30)")]
    public string Environment { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? XmlIssueDate { get; set; }




    [Column(TypeName = "varchar(50)")]
    public string ProtocoloDeletionRFB { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string CodigoErroDeletionRFB { get; set; }
    [Column(TypeName = "text")]
    public string DescricaoErroDeletionRFB { get; set; }
    public int SituacaoDeletionRFBId { get; set; } // “Received” , “Rejected”, “Processed”
    public DateTime? DataProtocoloDeletionRFB { get; set; }
    public DateTime? DataChecagemDeletionRFB { get; set; }
    [Column(TypeName = "varchar(120)")]
    public string NaturezaCargaLista { get; set; }
    public string[] NaturezaCargaArray()
    {
        if (string.IsNullOrEmpty(NaturezaCargaLista))
            return new string[0];

        return NaturezaCargaLista.Split(",");
    }
}
