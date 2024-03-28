using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data.Cache;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support;

public class AutenticaReceitaFederal : IAutenticaReceitaFederal
{
    const string RFB_CCT_CACHE_KEY = "RfbCctToken";
    public readonly IConfiguration _configuration;
    public readonly ICacheService _cacheService;
    public readonly string _requestUri;
    public readonly string _baseUrl;

    public AutenticaReceitaFederal(IConfiguration configuration, ICacheService cacheService)
    {
        _configuration = configuration;
        _baseUrl = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalBaseUrl").Value;
        _requestUri = _configuration.GetSection(@"EndPoints").GetSection("ReceitaFederalAuthentication").Value;
        _cacheService = cacheService;
    }

    public async Task<TokenResponse> GetTokenAuthetication(X509Certificate2 certificado, string profile = "TRANSPORT")
    {
        string key = $"{RFB_CCT_CACHE_KEY}-{certificado.SerialNumber}-{profile}";

        var authResponse = await _cacheService.GetDataAsync<TokenResponse>(key);
        if (authResponse is not null && authResponse.ExpirationTokenTime > DateTime.UtcNow)
            return authResponse;

        try
        {
            string url = $"{_baseUrl}/{_requestUri }";
            string postData = "";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Role-Type", profile);
            request.ProtocolVersion = HttpVersion.Version11;

            request.ClientCertificates.Add(certificado);

            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
            try
            {
                requestWriter.Write(postData);
            }
            finally
            {
                requestWriter.Close();
            }

            // Send to the remote server and wait for the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                content = reader.ReadToEnd();
            }

            authResponse = new TokenResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                double expiration = Double.Parse(response.Headers.Get("X-CSRF-Expiration"));

                DateTimeOffset DateUTCKin1 = DateTimeOffset.FromUnixTimeMilliseconds((long)expiration);

                authResponse.SetToken = response.Headers.Get("Set-Token");
                authResponse.XCSRFToken = response.Headers.Get("X-CSRF-Token");
                authResponse.ExpirationTokenTime = DateUTCKin1.DateTime;
                await _cacheService.SetDataAsync(key, authResponse, expiration);
                return authResponse;
            }

            // Return response
            throw new Exception($"Não foi possível autenticar-se ao servidor da Receita Federal através do enpoint { _requestUri }.");
        }
        catch (Exception ex)
        {
            throw new BusinessException(ex.Message);
        }

    }
}
