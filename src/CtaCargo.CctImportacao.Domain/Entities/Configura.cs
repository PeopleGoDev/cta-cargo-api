using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class Configura : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string ConfiguracaoTipo { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string ConfiguracaoNome { get; set; }
    [Required]
    [Column(TypeName = "varchar(255)")]
    public string ConfiguracaoValor { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string ConfiguracaoAdicional { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string ConfiguracaoAdicional2 { get; set; }
    public int CiaAereaId { get; set; }
    [ForeignKey("CiaAereaId")]
    public virtual CiaAerea CompanhiaAereaInfo { get; set; }
    public int UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual Usuario UsuarioInfo { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
}
