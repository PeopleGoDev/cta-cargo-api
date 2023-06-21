using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class MasterInstrucaoManuseio
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(3)")]
        public string Codigo { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string Descricao { get; set; }
    }
}
