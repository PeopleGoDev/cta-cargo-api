using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class MasterHouseAssociacao : BaseEntity
    {
        public MasterHouseAssociacao()
        {
            Houses = new HashSet<House>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string MasterNumber { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string OriginLocation { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string FinalDestinationLocation { get; set; }
        public int ConsigmentItemQuantity { get; set; }
        public int PackageQuantity { get; set; }
        public int TotalPieceQuantity { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string MessageHeaderDocumentId { get; set; }
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
        public virtual ICollection<House> Houses { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
        public double GrossWeight { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string GrossWeightUnit { get; set; }
    }
}
