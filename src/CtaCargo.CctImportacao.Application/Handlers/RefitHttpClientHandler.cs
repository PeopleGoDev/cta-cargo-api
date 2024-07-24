using System.Net.Http;

namespace CtaCargo.CctImportacao.Application.Handlers;
public class RefitHttpClientHandler : HttpClientHandler
{
    public RefitHttpClientHandler() {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }
}
