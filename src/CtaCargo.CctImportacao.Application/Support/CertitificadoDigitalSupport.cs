using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support;

public class CertitificadoDigitalSupport : ICertitificadoDigitalSupport
{
    public readonly ICiaAereaRepository _ciaAereaRepository;
    public readonly IAgenteDeCargaRepository _agenteDeCargaRepository;
    public readonly IUsuarioRepository _usuarioRepository;
    public readonly ICertificadoDigitalRepository _certificadoDigitalRepository;
    public readonly IDownloadArquivoCertificado _downloadArquivoCertificado;

    public CertitificadoDigitalSupport(ICiaAereaRepository ciaAereaRepository,
        IUsuarioRepository usuarioRepository,
        ICertificadoDigitalRepository certificadoDigitalRepository,
        IAgenteDeCargaRepository agenteDeCargaRepository,
        IDownloadArquivoCertificado downloadArquivoCertificado)
    {
        _ciaAereaRepository = ciaAereaRepository;
        _usuarioRepository = usuarioRepository;
        _agenteDeCargaRepository = agenteDeCargaRepository;
        _certificadoDigitalRepository = certificadoDigitalRepository;
        _downloadArquivoCertificado = downloadArquivoCertificado;
    }

    public async Task<CctCertificate> GetCertificateForAirCompany(int usuarioId, int airCompanyId)
    {
        bool hasUserExpired = false;
        bool hasCompanyExpired = false;

        var certificate = await GetCertificateUsuarioAsync(usuarioId);
        if(certificate != null && certificate.NotAfter > DateTime.Now)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, certificate, false, null);

        if (certificate != null)
            hasUserExpired = true;

        certificate = await GetCertificateCiaAereaAsync(airCompanyId);
        if (certificate != null && certificate.NotAfter > DateTime.Now)
            return new CctCertificate(CctCertificate.CertificateOriginType.Company, certificate, false, null);

        if (certificate != null)
            hasCompanyExpired = true;

        if (hasUserExpired)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, null, true, "Certificado do usuário expirado!");

        if(hasCompanyExpired)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, null, true, "Certificado da companhia aérea expirado!");

        return new CctCertificate(CctCertificate.CertificateOriginType.Unknown, null, true, "Não há certificados disponivel para Usuario/Companhia Aérea");
    }

    public async Task<CctCertificate> GetCertificateForFreightFowarder(int usuarioId, int freightForwarderId)
    {
        bool hasUserExpired = false;
        bool hasCompanyExpired = false;

        var certificate = await GetCertificateUsuarioAsync(usuarioId);
        if (certificate != null && certificate.NotAfter > DateTime.Now)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, certificate, false, null);

        if (certificate != null)
            hasUserExpired = true;

        certificate = await GetCertificateAgenteDeCargaAsync(freightForwarderId);
        if (certificate != null && certificate.NotAfter > DateTime.Now)
            return new CctCertificate(CctCertificate.CertificateOriginType.Company, certificate, false, null);

        if (certificate != null)
            hasCompanyExpired = true;

        if (hasUserExpired)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, null, true, "Certificado do usuário expirado!");

        if (hasCompanyExpired)
            return new CctCertificate(CctCertificate.CertificateOriginType.User, null, true, "Certificado do agente de carga expirado!");

        return null;
    }

    public async Task<X509Certificate2> GetCertificateCiaAereaAsync(int ciaAereaId)
    {
        CiaAerea ciaAerea = await _ciaAereaRepository.GetCiaAereaById(ciaAereaId);

        if (ciaAerea?.CertificadoDigital is null)
            return null;

        string file = ciaAerea.CertificadoDigital.Arquivo;
        string password = ciaAerea.CertificadoDigital.Senha;

        byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);

        if (arquivoMemoria is null)
            return null;

        X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);

        return x509;
    }

    public async Task<X509Certificate2> GetCertificateAgenteDeCargaAsync(int agenteDeCargaId)
    {
        AgenteDeCarga agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(agenteDeCargaId);

        if (agenteDeCarga?.CertificadoDigital is null)
            return null;

        string file = agenteDeCarga.CertificadoDigital.Arquivo;
        string password = agenteDeCarga.CertificadoDigital.Senha;

        byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);

        if (arquivoMemoria == null)
            return null;

        X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);

        return x509;
    }

    public async Task<X509Certificate2> GetCertificateUsuarioAsync(int usuarioId, bool optionalError = true)
    {
        Usuario usuario = await _usuarioRepository.GetUsuarioById(usuarioId);

        if (usuario?.CertificadoId is null)
            return null;

        CertificadoDigital certificado = await _certificadoDigitalRepository.GetCertificadoDigitalById((int)usuario.CertificadoId);

        if (certificado == null)
            return null;

        string file = certificado.Arquivo;
        string password = certificado.Senha;

        byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);

        if (arquivoMemoria is null)
            return null;

        X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);
        return x509;

    }
}

public class CctCertificate
{
    public enum CertificateOriginType
    {
        Unknown = 0,
        User =1,
        Company = 2,
    }

    public CctCertificate(CertificateOriginType origin, X509Certificate2 certificate, bool hasError, string error)
    {
        Origin = origin;
        Certificate = certificate;
        HasError = hasError;
        Error = error;
    }

    public CertificateOriginType Origin { get; }
    public X509Certificate2 Certificate { get;}
    public bool HasError { get;}
    public string Error { get;}
}