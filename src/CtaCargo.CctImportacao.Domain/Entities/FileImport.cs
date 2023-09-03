using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class FileImport : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Type { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Description { get; set; }
    public bool FirstLineTitle { get; set; }
    public bool ContinueOnError { get; set; }
    [Column(TypeName = "varchar(255)")]
    public string Configuration1 { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Configuration2 { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime? DataExclusao { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string Destination { get; set; }
    public ICollection<FileImportDetail> Details { get; set; } = new List<FileImportDetail>();
}
