using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class FileImportDetail : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [ForeignKey("FileImportId")]
    public int FileImportId { get; set; }
    public FileImport FileImport { get; set; }

    [Required]
    public int Sequency { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string ColumnName { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string ColumnAssociate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
}