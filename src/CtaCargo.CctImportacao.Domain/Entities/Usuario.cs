using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string EMail { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Senha { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Nome { get; set; }
        [Required]
        [Column(TypeName = "varchar(30)")]
        public string Sobrenome { get; set; }
        public int? CiaAereaId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string CiaAereaNome { get; set; }
        public bool AlteraCia { get; set; }
        [Required]
        public bool AcessaUsuarios { get; set; }
        [Required]
        public bool AcessaClientes { get; set; }
        [Required]
        public bool AcessaCiasAereas { get; set; }
        [Required]
        public bool Bloqueado { get; set; }
        public int CriadoPeloId { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTimeUtc { get; set; }
        public int? ModificadoPeloId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDateTimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataExclusao { get; set; }
        public bool UsuarioSistema { get; set; }
        public bool AlterarSenha { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataReset { get; set; }
        public int? CertificadoId { get; set; }
    }
}
