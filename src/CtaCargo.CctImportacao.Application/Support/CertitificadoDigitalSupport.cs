using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support
{
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

        public async Task<X509Certificate2> GetCertificateCiaAereaAsync(int ciaAereaId)
        {
            CiaAerea ciaAerea = await _ciaAereaRepository.GetCiaAereaById(ciaAereaId);
            if (ciaAerea != null)
            {
                if (ciaAerea.CertificadoDigital != null)
                {
                    string file = ciaAerea.CertificadoDigital.Arquivo;
                    string password = ciaAerea.CertificadoDigital.Senha;

                    try
                    {
                        byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);
                        if (arquivoMemoria != null)
                        {
                            X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);
                            return x509;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Erro certificado digital: { ex.Message }.");
                    }
                }
                throw new Exception("Companhia Aerea não possui certificado digital.");
            }
            throw new Exception("Companhia Aerea não selecionada pelo voo.");
        }

        public async Task<X509Certificate2> GetCertificateAgenteDeCargaAsync(int agenteDeCargaId)
        {
            AgenteDeCarga agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(agenteDeCargaId);

            if (agenteDeCarga == null)
                throw new Exception($"Agente de carga não encontrado: {agenteDeCargaId.ToString()}");

            if (agenteDeCarga.CertificadoDigital == null)
                throw new Exception("Agente de carga não possui certificado digital.");

            string file = agenteDeCarga.CertificadoDigital.Arquivo;
            string password = agenteDeCarga.CertificadoDigital.Senha;

            try
            {
                byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);

                if (arquivoMemoria == null)
                    throw new Exception($"Não foi possível baixar o arquivo do certificado digitial: {file}.");

                X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);
                if (DateTime.Now > x509.NotAfter)
                    throw new Exception($"Certificado digital expirado em: {x509.NotAfter.ToString()}.");
                return x509;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro certificado digital: {ex.Message}.");
            }
        }

        public async Task<X509Certificate2> GetCertificateUsuarioAsync(int usuarioId)
        {
            Usuario usuario = await _usuarioRepository.GetUsuarioById(usuarioId);

            if (usuario == null)
                throw new Exception("Usuario não existente ou desativado.");

            if (usuario.CertificadoId == null)
                throw new Exception("Usuario não possui certificado digital.");

            CertificadoDigital certificado = await _certificadoDigitalRepository.GetCertificadoDigitalById((int)usuario.CertificadoId);
            if (certificado == null)
                throw new Exception("Certificado Digital não encontrado.");

            string file = certificado.Arquivo;
            string password = certificado.Senha;

            try
            {
                byte[] arquivoMemoria = _downloadArquivoCertificado.GetCertificateStream(file);
                if (arquivoMemoria != null)
                {
                    X509Certificate2 x509 = new X509Certificate2(arquivoMemoria, password);
                    return x509;
                }
                throw new Exception("Certificado Digital Invalido!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro certificado digital: { ex.Message }.");
            }
        }
    }
}
