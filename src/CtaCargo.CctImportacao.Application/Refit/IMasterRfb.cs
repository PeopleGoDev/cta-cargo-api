using Refit;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Refit;

public interface IMasterRfb
{
    [Headers("Content-Type: application/xml")]
    [QueryUriFormat(UriFormat.Unescaped)]
    [Post("/ccta/api/ext/incoming/xfwb")]
    Task<string> Submit([Query("cnpj")] string cnpj, [Body] string payload, [Header("Authorization")] string setToken, [Header("X-CSRF-Token")] string xCSRFToken);
}
