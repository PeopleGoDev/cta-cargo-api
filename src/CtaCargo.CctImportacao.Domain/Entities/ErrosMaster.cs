using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class ErroMaster
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int MasterId { get; set; }
        [ForeignKey("MasterId")]
        public virtual Master MasterInfo { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Erro { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
    }
}
