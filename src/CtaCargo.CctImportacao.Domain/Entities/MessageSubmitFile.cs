using CtaCargo.CctImportacao.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class MessageSubmitFile : BaseEntity
{
    [Key]
    [Required]
    public int Id { get; set; }
    public FileType Type { get; set; }
    public IataXmlPurposeCode FilePurpose { get; set; }
    public RFStatusSubmitFile Status { get; set; }
    public int SourceId { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string ProtocolNumber { get; set; }
    [Column(TypeName = "varchar(40)")]
    public string ErrorCode { get; set; }
    [Column(TypeName = "varchar(250)")]
    public string ErrorDescription { get; set; }
    [Column(TypeName = "varchar(max)")]
    public string Content { get; set; }
}