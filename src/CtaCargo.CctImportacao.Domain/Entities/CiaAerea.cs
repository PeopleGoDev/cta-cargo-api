using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class CiaAerea : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Nome { get; set; }
    [Required]
    [Column(TypeName = "varchar(14)")]
    public string CNPJ { get; set; }
    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Endereco { get; set; }
    [Column(TypeName = "varchar(60)")]
    public string Complemento { get; set; }
    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Cidade { get; set; }
    [Required]
    [Column(TypeName = "varchar(3)")]
    public string UF { get; set; }
    [Required]
    [Column(TypeName = "varchar(2)")]
    public string Pais { get; set; }
    [Required]
    [Column(TypeName = "varchar(10)")]
    public string Numero { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
    public int? CertificadoId { get; set; }
    [ForeignKey("CertificadoId")]
    public virtual CertificadoDigital CertificadoDigital { get; set; }
    public bool OnlyGhostFlight { get; set; }
}
