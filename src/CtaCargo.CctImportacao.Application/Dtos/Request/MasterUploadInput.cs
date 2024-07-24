using CtaCargo.CctImportacao.Domain.Enums;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class MasterUploadInput
{
    public int UsuarioId { get; set; }
    public int[] MasterId { get; set; }
    public IataXmlPurposeCode PurposeCode { get; set; }
}
