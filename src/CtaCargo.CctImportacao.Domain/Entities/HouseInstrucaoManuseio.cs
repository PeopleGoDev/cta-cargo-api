using CtaCargo.CctImportacao.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class HouseInstrucaoManuseio
{
    [Key]
    [Required]
    public int Id { get; set; }

    public int HouseId { get; set; }

    [ForeignKey(nameof(HouseId))]
    public virtual House House { get; set; }

    public SpecialHanglingType Tipo { get; set; }

    [Required]
    [Column(TypeName = "varchar(3)")]
    public string Codigo { get; set; }

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Descricao { get; set; }
}