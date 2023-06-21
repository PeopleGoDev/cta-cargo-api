using System.Security.Cryptography.X509Certificates;

namespace CtaCargo.CctImportacao.Application.Support.Contracts
{
    public interface IAutenticaReceitaFederal
    {
        TokenResponse GetTokenAuthetication(X509Certificate2 certificado, string perfil = "TRANSPORT");
    }
}