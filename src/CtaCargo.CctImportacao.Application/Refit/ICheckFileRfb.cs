using CtaCargo.CctImportacao.Application.Support;
using Refit;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Refit;

public interface ICheckFileRfb
{
    [Headers("Content-Type: application/json")]
    [QueryUriFormat(UriFormat.Unescaped)]
    [Get("/ccta/api/ext/check/received-files/{protocolNumber}")]
    Task<ProtocoloReceitaCheckFile> Submit(string protocolNumber, [Header("Authorization")] string SetToken, [Header("X-CSRF-Token")] string XCSRFToken);
}
