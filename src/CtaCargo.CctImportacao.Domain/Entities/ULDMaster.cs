using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class UldMaster: BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int VooId { get; set; }
        [ForeignKey("VooId")]
        public virtual Voo VooInfo { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string MasterNumero { get; set; }
        public int? MasterId { get; set; }
        [ForeignKey("MasterId")]
        public virtual Master MasterInfo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string ULDId { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string ULDCaracteristicaCodigo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ULDObs { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string ULDIdPrimario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
        public int? QuantidadePecas { get; set; }
        [Column(TypeName = "MONEY")]
        public decimal? Peso { get; set; }
        public string TotalParcial { get; set; }

        public int VooTrechoId { get; set; }
        [ForeignKey("VooTrechoId")]
        public virtual VooTrecho VooTrechoInfo { get; set; }
    }
}
