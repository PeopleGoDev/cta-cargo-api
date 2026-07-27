using CtaCargo.CctImportacao.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class HouseTratamentoEspecial : BaseEntity
{
    [Key]
    public int Id { get; set; }

    // FK para House
    public int HouseId { get; set; }

    [ForeignKey("HouseId")]
    public virtual House House { get; set; }

    [Required]
    [Column(TypeName = "tinyint")]
    public CargoHangling Tipo { get; set; }

    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Codigo { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Descricao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; } 
}