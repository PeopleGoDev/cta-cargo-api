using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class MasterHouseAssociationChild : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    public int MasterHouseAssociationId { get; set; }
    [ForeignKey(nameof(MasterHouseAssociationId))]
    public MasterHouseAssociacao MasterHouseAssociacao { get; set; }
    public int HouseId { get; set; }
    [ForeignKey(nameof(HouseId))]
    public House House { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
}