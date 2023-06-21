using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class Cliente: BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Endereco { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Postal { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Cidade { get; set; }
        [Column(TypeName = "varchar(2)")]
        public string PaisCodigo { get; set; }
        [Column(TypeName = "varchar(60)")]
        public string Subdivisao { get; set; }
        public int CnpjId { get; set; }
        [ForeignKey("CnpjId")]
        public virtual CnpjCliente CnpjClienteInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
    }
}
