using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public interface IUploadReceitaFederal
{
    Task<ReceitaRetornoProtocol> SubmitFlight(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
    Task<ReceitaRetornoProtocol> SubmitWaybill(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
    Task<ReceitaRetornoProtocol> SubmitHouse(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
    Task<ReceitaRetornoProtocol> SubmitHouseMaster(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado);
    Task<ProtocoloReceitaCheckFile> CheckFileProtocol(string protocol, TokenResponse token);
}