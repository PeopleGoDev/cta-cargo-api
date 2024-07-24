using CtaCargo.CctImportacao.Application.Refit;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace CtaCargo.CctImportacao.Application.Support;

public class FlightUploadReceitaFederal : IUploadReceitaFederal
{
    public readonly IConfiguration _configuration;
    public readonly string _baseUrl;
    public readonly string _requestFlightMethod;
    public readonly string _requestWaybillMethod;
    public readonly string _requestHouseMethod;
    public readonly string _requestHouseMasterMethod;
    public readonly string _requestFileMethod;

    private readonly IMasterRfb _masterRfb;

    public FlightUploadReceitaFederal(
        IConfiguration configuration,
        IMasterRfb masterRfb)
    {
        _configuration = configuration;
        _baseUrl = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalBaseUrl").Value;
        _requestFlightMethod = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalFlightSubmit").Value;
        _requestWaybillMethod = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalWaybillSubmit").Value;
        _requestHouseMethod = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalHouseSubmit").Value;
        _requestHouseMasterMethod = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalHouseMasterSubmit").Value;
        _requestFileMethod = _configuration.GetSection("EndPoints").GetSection("ReceitaFederalVerifyFiles").Value;
        _masterRfb = masterRfb;
    }

    public async Task<ReceitaRetornoProtocol> SubmitFlight(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado)
    {
        try
        {
            // Create the request
            string url = $"{_baseUrl}{_requestFlightMethod}?cnpj={cnpj}";
            string postData = xml;

            return await SubmitFile(url, xml, token);

        }
        catch (WebException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
        
    }
    public async Task<ReceitaRetornoProtocol> SubmitWaybill_old(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado)
    {
        var response = await _masterRfb.Submit(cnpj, xml, token.SetToken, token.XCSRFToken);

        return GetProtocolResponse(response);
    }
    public async Task<ReceitaRetornoProtocol> SubmitWaybill(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado)
    {
        try
        {
            // Create the request
            string url = $"{_baseUrl}{_requestWaybillMethod}?cnpj={cnpj}";
            string postData = xml;

            return await SubmitFile(url, xml, token);
        }
        catch (WebException ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: { ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: { ex.Message}"
            };
        }

    }
    public async Task<ReceitaRetornoProtocol> SubmitHouse(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado)
    {
        try
        {
            // Create the request
            string url = $"{_baseUrl}{_requestHouseMethod}?cnpj={cnpj}";
            string postData = xml;

            return await SubmitFile(url, xml, token);
        }
        catch (WebException ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: { ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: { ex.Message}"
            };
        }

    }
    public async Task<ReceitaRetornoProtocol> SubmitHouseMaster(string cnpj, string xml, TokenResponse token, X509Certificate2 certificado)
    {
        try
        {
            // Create the request
            string url = $"{_baseUrl}{_requestHouseMasterMethod}?cnpj={cnpj}";
            string postData = xml;

            return await SubmitFile(url, xml, token);
        }
        catch (WebException ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new ReceitaRetornoProtocol
            {
                StatusCode = "Error",
                Reason = $"Erro interno: {ex.Message}"
            };
        }

    }
    private static async Task<ReceitaRetornoProtocol> SubmitFile(string url, string xml, TokenResponse token)
    {
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        HttpWebRequest request = null;
        byte[] bytes = Encoding.UTF8.GetBytes(xml);
        request = (HttpWebRequest)WebRequest.Create(new Uri(url));

        request.ContentType = "application/xml";
        request.Method = "POST";
        request.ContentLength = bytes.Length;
        request.Headers.Add("Authorization", token.SetToken);
        request.Headers.Add("X-CSRF-Token", token.XCSRFToken);
        //request.ProtocolVersion = HttpVersion.Version11;
        request.ProtocolVersion = HttpVersion.Version10;

        using (Stream requeststream = await request.GetRequestStreamAsync())
        {
            requeststream.Write(bytes, 0, bytes.Length);
            requeststream.Close();
        }

        string statusCode = "";
        string reason = "";
        DateTime? issueDate = null;

        using (var webResponse = await request.GetResponseAsync())
        {
            using (StreamReader sr = new(webResponse.GetResponseStream()))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(sr);
                XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
                ns.AddNamespace("ns2", "iata:response:3");
                ns.AddNamespace("", "iata:datamodel:3");

                var node3 = doc.SelectSingleNode("ns2:Response/ns2:MessageHeaderDocument", ns);
                if (node3 != null && node3.HasChildNodes)
                    foreach (XmlNode item in node3.ChildNodes)
                        if (item.Name == "IssueDateTime")
                            issueDate = DateTime.Parse(item.InnerText);

                var node = doc.SelectSingleNode("ns2:Response/ns2:BusinessHeaderDocument", ns);
                if (node != null && node.HasChildNodes)
                    foreach (XmlNode item in node.ChildNodes)
                        if (item.Name == "StatusCode")
                            statusCode = item.InnerText;

                var node2 = doc.SelectSingleNode("ns2:Response/ns2:ResponseStatus", ns);
                if (node2 != null && node2.HasChildNodes)
                    foreach (XmlNode item in node2.ChildNodes)
                        if (item.Name == "Reason")
                            reason = item.InnerText;
            }
            webResponse.Close();
        }

        return new ReceitaRetornoProtocol
        {
            StatusCode = statusCode,
            Reason = reason,
            IssueDateTime = issueDate
        };

    }
    public async Task<ProtocoloReceitaCheckFile> CheckFileProtocol(string protocol, TokenResponse token)
    {
        ProtocoloReceitaCheckFile responseObject = null;
        try
        {
            string url = $"{_baseUrl}{_requestFileMethod}".Replace("{protocolNumber}",protocol);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "GET";
            request.Headers.Add("Authorization", token.SetToken);
            request.Headers.Add("X-CSRF-Token", token.XCSRFToken);
            request.ProtocolVersion = HttpVersion.Version11;

            using (var webResponse = await request.GetResponseAsync())
            {
                using (StreamReader sr = new(webResponse.GetResponseStream()))
                {
                    var response = sr.ReadToEnd().Trim();
                    sr.Close();
                    
                    responseObject = JsonSerializer.Deserialize<ProtocoloReceitaCheckFile>(response);
                }
                webResponse.Close();
            }
            return responseObject;
        }
        catch (WebException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private ReceitaRetornoProtocol GetProtocolResponse(string response)
    {
        string statusCode = "";
        string reason = "";
        DateTime? issueDate = null;

        XmlDocument doc = new();
        doc.LoadXml(response);
        XmlNamespaceManager ns = new(doc.NameTable);
        ns.AddNamespace("ns2", "iata:response:3");
        ns.AddNamespace("", "iata:datamodel:3");

        var node3 = doc.SelectSingleNode("ns2:Response/ns2:MessageHeaderDocument", ns);
        if (node3 != null && node3.HasChildNodes)
            foreach (XmlNode item in node3.ChildNodes)
                if (item.Name == "IssueDateTime")
                    issueDate = DateTime.Parse(item.InnerText);

        var node = doc.SelectSingleNode("ns2:Response/ns2:BusinessHeaderDocument", ns);
        if (node != null && node.HasChildNodes)
            foreach (XmlNode item in node.ChildNodes)
                if (item.Name == "StatusCode")
                    statusCode = item.InnerText;

        var node2 = doc.SelectSingleNode("ns2:Response/ns2:ResponseStatus", ns);
        if (node2 != null && node2.HasChildNodes)
            foreach (XmlNode item in node2.ChildNodes)
                if (item.Name == "Reason")
                    reason = item.InnerText;

        return new ReceitaRetornoProtocol
        {
            StatusCode = statusCode,
            Reason = reason,
            IssueDateTime = issueDate
        };
    }
}
