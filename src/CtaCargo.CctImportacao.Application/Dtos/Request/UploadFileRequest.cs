namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public enum FileDestinationMap
{
    CiaAerea = 0,
    AgenteDeCarga = 1,
    User = 2,
}
public class UploadFileRequest
{
    public int? Id { get; set; }
    public string Senha { get; set; }
    public FileDestinationMap CertificadoDestino { get; set; }
}
