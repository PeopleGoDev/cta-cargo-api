using System.Security.Cryptography.X509Certificates;

namespace CtaCargo.CctImportacao.Application.Support
{
    public interface IUploadReceitaFederal
    {
        ReceitaRetornoProtocol SubmitFlight(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
        ReceitaRetornoProtocol SubmitWaybill(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
        ReceitaRetornoProtocol SubmitHouse(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
        ReceitaRetornoProtocol SubmitHouseMaster(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
        ProtocoloReceitaCheckFile CheckFileProtocol(string protocol, TokenResponse token);
    }
}