using Refit;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Refit;
public interface IHouseRfb
{
    [Headers("Content-Type: application/xml")]
    [QueryUriFormat(UriFormat.Unescaped)]
    [Post("/ccta/api/ext/incoming/xfzb")]
    Task<string> Submit([Query("cnpj")] string cnpj, [Body]string payload, [Header("Authorization")] string SetToken, [Header("X-CSRF-Token")] string XCSRFToken);
}
