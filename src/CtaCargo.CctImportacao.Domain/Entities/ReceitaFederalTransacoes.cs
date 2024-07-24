using CtaCargo.CctImportacao.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class ReceitaFederalTransacao : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(10)")]
    public string TipoArquivo { get; set; }
    [Required]
    [Column(TypeName = "varchar(15)")]
    public string Numero { get; set; }
    public RFStatusEnvioType SituacaoRFBId { get; set; } // “Received” , “Rejected”, “Processed”
    [Column(TypeName = "datetime")]
    public DateTime? DataProcessadoRFB { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string ProtocoloRFB { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string CodigoErroRFB { get; set; }
    [Column(TypeName = "varchar(550)")]
    public int? FaturaId { get; set; }
    [ForeignKey("NumeroFatura")]
    public virtual Fatura FaturaInfo { get; set; }
    public string DescricaoErroRFB { get; set; }
    [Column(TypeName = "TEXT")]
    public string Xml { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
}
