using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class CertificadoDigital : BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(120)")]
        public string Arquivo { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime DataVencimento { get; set; }
        [Column(TypeName = "varchar(120)")]
        public string NomeDono { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string NumeroDocumento { get; set; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Senha { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
        public virtual ICollection<CiaAerea> CiasAerea { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string SerialNumber { get; set; }
    }
}