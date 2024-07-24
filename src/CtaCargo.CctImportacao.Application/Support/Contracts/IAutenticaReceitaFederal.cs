using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public interface IAutenticaReceitaFederal
{
    Task<TokenResponse> GetTokenAuthetication(X509Certificate2 certificado, string perfil = "TRANSPORT");
}