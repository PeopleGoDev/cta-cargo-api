using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class VooTrecho : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    public int VooId { get; set; }
    [ForeignKey("VooId")]
    public virtual Voo VooInfo { get; set; }

    [Column(TypeName = "varchar(3)")]
    public string AeroportoDestinoCodigo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataHoraSaidaEstimada { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataHoraChegadaEstimada { get; set; }

    public ICollection<UldMaster> ULDs { get; set; } = new List<UldMaster>();

    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }

    public int? PortoIataDestinoId { get; set; }
    [ForeignKey("PortoIataDestinoId")]
    public virtual PortoIata PortoIataDestinoInfo { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataHoraSaidaAtual { get; set; }
}