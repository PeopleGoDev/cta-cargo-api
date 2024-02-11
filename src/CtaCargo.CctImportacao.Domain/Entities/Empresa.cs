using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class Empresa
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(60)")]
    public string RazaoSocial { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Contato { get; set; }
    [Required]
    [Column(TypeName = "varchar(25)")]
    public string Telefone { get; set; }
    [Required]
    [Column(TypeName = "varchar(150)")]
    public string EMail { get; set; }
    [Required]
    [Column(TypeName = "varchar(10)")]
    public string CEP { get; set; }
    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Endereco { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Complemento { get; set; }
    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Cidade { get; set; }
    [Required]
    [Column(TypeName = "varchar(2)")]
    public string UF { get; set; }
    [Required]
    [Column(TypeName = "varchar(2)")]
    public string Pais { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
    [Column(TypeName = "text")]
    public string LogoUrl { get; set; }
    [Required]
    [Column(TypeName = "varchar(14)")]
    public string CNPJ { get; set; }
    //[Required]
    //public bool Blocked { get; set; } = false;
    //[Column(TypeName = "varchar(30)")]
    //public string BlockeReason { get; set; }
}
