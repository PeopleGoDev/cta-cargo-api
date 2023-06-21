using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class BaseEntity
    {
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public int CriadoPeloId { get; set; }
        [ForeignKey("CriadoPeloId")]
        public virtual Usuario UsuarioCriacaoInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTimeUtc { get; set; }
        public int? ModificadoPeloId { get; set; }
        [ForeignKey("ModificadoPeloId")]
        public virtual Usuario UsuarioModificacaoInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDateTimeUtc { get; set; }
    }
}
