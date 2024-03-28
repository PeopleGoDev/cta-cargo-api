using Refit;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Refit;

public interface IFlightRfb
{
    [Headers("Content-Type: application/xml")]
    [QueryUriFormat(UriFormat.Unescaped)]
    [Post("/ccta/api/ext/incoming/xffm")]
    Task<string> Submit([Query("cnpj")] string cnpj, [Body] string payload, [Header("Authorization")] string SetToken, [Header("X-CSRF-Token")] string XCSRFToken);
}
