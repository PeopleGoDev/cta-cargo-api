using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class NCM
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(12)")]
        public string Codigo { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(1200)")]
        public string Descricao { get; set; }
        [Required]
        [Column(TypeName = "TEXT")]
        public string DescricaoConcatenada { get; set; }
        public bool Seleciona { get; set; }
        [Column(TypeName = "VARCHAR(8)")]
        public string CodigoNumero { get; set; }
    }
}
