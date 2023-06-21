using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public enum FaturaType
    {
        AirCargo = 1,
        FreightFoward =2
    }
    public class Fatura : BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string TipoFatura { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEmissao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataVencimento { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
