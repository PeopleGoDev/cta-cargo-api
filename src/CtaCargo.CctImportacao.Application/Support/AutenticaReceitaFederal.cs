using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CtaCargo.CctImportacao.Application.Support
{
    public class AutenticaReceitaFederal : IAutenticaReceitaFederal
    {
        public readonly IConfiguration _configuration;
        public readonly string _requestUri;
        public readonly string _baseUrl;

        public AutenticaReceitaFederal(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalBaseUrl").Value;
            _requestUri = _configuration.GetSection(@"EndPoints").GetSection("ReceitaFederalAuthentication").Value;
        }

        public TokenResponse GetTokenAuthetication(X509Certificate2 certificado, string perfil= "TRANSPORT")
        {
            try
            {
                string url = $"{_baseUrl}{_requestUri }";
                string postData = "";
                // Create the request
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Headers.Add("Role-Type", perfil);
                request.ProtocolVersion = HttpVersion.Version11;

                request.ClientCertificates.Add(certificado);
                // Write data to request

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

                TokenResponse token = new TokenResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    token.SetToken = response.Headers.Get("Set-Token");
                    token.XCSRFToken = response.Headers.Get("X-CSRF-Token");
                    return token;
                }

                // Return response
                throw new Exception($"Não possível autenticar-se ao servidor da Receita Federal através do enpoint { _requestUri }.");

            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }

        }
    }
}
