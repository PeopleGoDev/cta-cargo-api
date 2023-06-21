using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class Voo : BaseEntity
    {
        public Voo()
        {
            Masters = new HashSet<Master>();
            ULDs = new HashSet<UldMaster>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        public int CiaAereaId { get; set; }
        [ForeignKey("CiaAereaId")]
        public virtual CiaAerea CompanhiaAereaInfo { get; set; }
        [Required]
        [Column(TypeName = "varchar(15)")]
        public string Numero { get; set; }
        public int? PortoIataOrigemId { get; set; }
        [ForeignKey("PortoIataOrigemId")]
        public virtual PortoIata PortoIataOrigemInfo { get; set; }
        public int? PortoIataDestinoId { get; set; }
        [ForeignKey("PortoIataDestinoId")]
        public virtual PortoIata PortoIataDestinoInfo { get; set; }
        public double? TotalPesoBruto { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string TotalPesoBrutoUnidade { get; set; }
        public double? TotalVolumeBruto { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string TotalVolumeBrutoUnidade { get; set; }
        public int? TotalPacotes { get; set; }
        public int? TotalPecas { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataVoo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSaidaEstimada { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraSaidaReal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraChegadaEstimada{ get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraChegadaReal{ get; set; }
        public int StatusId { get; set; }
        public RFStatusEnvioType SituacaoRFBId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string ProtocoloRFB { get; set; }
        [Column(TypeName = "varchar(40)")]
        public string CodigoErroRFB { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string DescricaoErroRFB { get; set; }
        public virtual ICollection<Master> Masters { get; set; }
        public virtual ICollection<UldMaster> ULDs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataEmissaoXML { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataProtocoloRFB { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataChecagemRFB { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string AeroportoOrigemCodigo { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string AeroportoDestinoCodigo { get; set; }
        public bool Reenviar { get; set; }
    }
}
