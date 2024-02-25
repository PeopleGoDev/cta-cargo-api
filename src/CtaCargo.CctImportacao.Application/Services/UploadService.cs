using Azure.Storage.Files.Shares;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class UploadService : IUploadService
{
    private readonly ICiaAereaRepository _ciaaereaRepository;
    private readonly IAgenteDeCargaRepository _agenteDeCargaRepository;
    private readonly ICertificadoDigitalRepository _certificadoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly string _azureStorageConnectionString;
    private readonly string _azureStorageSharedFolder;
    private readonly IConfiguration _configuration;

    public UploadService(ICiaAereaRepository ciaaereaRepository,
        IAgenteDeCargaRepository agenteDeCargaRepository,
        ICertificadoDigitalRepository certificadoRepository,
        IConfiguration configuration,
        IUsuarioRepository usuarioRepository)
    {
        _ciaaereaRepository = ciaaereaRepository;
        _agenteDeCargaRepository = agenteDeCargaRepository;
        _certificadoRepository = certificadoRepository;
        _configuration = configuration;
        _azureStorageConnectionString = _configuration.GetConnectionString("AzureStorageConnectionString");
        _azureStorageSharedFolder = _configuration.GetConnectionString("AzureStorageSharedFolder");
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UploadCertificadoResponseDto> UploadArquivo(
        UserSession userSession,
        UploadFileRequest input,
        Stream fileStream)
    {
        string nomeArquivo;
        int certificadoId = -1;
        var x509Certificado2 = GetCertificate(fileStream, input.Senha);
        CertificadoDigital cert = await _certificadoRepository
            .GetCertificadoDigitalBySerialNumber(userSession.CompanyId, x509Certificado2.SerialNumber);

        if (cert == null)
        {
            // Instatiate a ShareServiceClient
            // Get a reference to a share named "sample-share"
            ShareClient share = new ShareClient(_azureStorageConnectionString, _azureStorageSharedFolder);

            string firstNode = x509Certificado2.Subject.Split(",")[0];
            firstNode = firstNode.Replace("CN=", "").Replace("CD=", "");
            string owner = firstNode.Split(":")[0];
            string ownerId = firstNode.Split(":")[1];
            nomeArquivo = $"{firstNode.Split(" ")[0].Trim().ToLower()}_D{DateTime.Now.ToString("yyyyMMdd")}_H{DateTime.Now.ToString("hhmmss")}.pfx";
            fileStream.Position = 0;
            ShareDirectoryClient directory = share.GetDirectoryClient("files");
            directory.CreateIfNotExists();
            ShareFileClient file = directory.GetFileClient(nomeArquivo);
            file.DeleteIfExists();
            await file.CreateAsync(fileStream.Length);
            await file.UploadAsync(fileStream);
            fileStream.Dispose();

            cert = new CertificadoDigital
            {
                Arquivo = nomeArquivo,
                CreatedDateTimeUtc = DateTime.UtcNow,
                CriadoPeloId = userSession.UserId,
                DataVencimento = x509Certificado2.NotAfter,
                EmpresaId = userSession.CompanyId,
                Senha = input.Senha,
                NomeDono = firstNode,
                SerialNumber = x509Certificado2.SerialNumber,
                Owner = owner,
                OwnerId = ownerId
            };

            _certificadoRepository.CreateCertificadoDigital(cert);

            if (await _certificadoRepository.SaveChanges() == false)
                throw new Exception("Erro desconhecido: Não foi possível gravar as informação na tabela Certificado Digital!");
        }

        certificadoId = cert.Id;
        nomeArquivo = cert.Arquivo;

        switch (input.CertificadoDestino)
        {
            case FileDestinationMap.CiaAerea:
                var cia = await _ciaaereaRepository.GetCiaAereaById(input.Id.Value);
                if (cia == null)
                    throw new BusinessException("Companhia Aerea não encontrada!");

                cia.CertificadoId = certificadoId;
                _ciaaereaRepository.UpdateCiaAerea(cia);
                if (await _ciaaereaRepository.SaveChanges() == false)
                    throw new BusinessException("Erro desconhecido: Não foi possível gravar as informação na tabela CiaAerea!");
                break;
            case FileDestinationMap.AgenteDeCarga:
                var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(input.Id.Value);
                if (agenteDeCarga == null)
                    throw new BusinessException("Agente de Carga não encontrada!");
                agenteDeCarga.CertificadoId = certificadoId;
                _agenteDeCargaRepository.UpdateAgenteDeCarga(agenteDeCarga);
                if (await _agenteDeCargaRepository.SaveChanges() == false)
                    throw new BusinessException("Erro desconhecido: Não foi possível gravar as informação na tabela AgenteDeCarga!");
                break;
            case FileDestinationMap.User:
                var user = await _usuarioRepository.GetUserCertificateById(userSession.UserId);
                if (user == null)
                    throw new BusinessException("Usuário não encontrado!");
                user.CertificadoId = certificadoId;
                _usuarioRepository.UpdateUsuario(user);
                if (await _usuarioRepository.SaveChanges() == false)
                    throw new BusinessException("Erro desconhecido: Não foi possível gravar as informação na tabela Usuário!");
                break;
        }

        return new UploadCertificadoResponseDto()
        {
            DataVencimento = x509Certificado2.NotAfter,
            NomeArquivo = nomeArquivo
        };
    }

    private X509Certificate2 GetCertificate(Stream stream, string password)
    {
        try
        {
            byte[] rawData = ReadFile(stream);
            X509Certificate2 x509 = new X509Certificate2(rawData, password);
            return x509;
        }
        catch (Exception ex)
        {
            if (ex.Message == "A senha de rede especificada não está correta.")
                throw new BusinessException("A senha do certificado está incorreta!",ex);
            throw new BusinessException(ex.Message, ex);
        }
    }
    private async Task<string> GetFileNameAirCompany(int companhiaId)
    {
        CiaAerea cia = await _ciaaereaRepository.GetCiaAereaById(companhiaId);
        if (cia != null)
        {
            return
                $"{ cia.Nome.Split(" ")[0].Trim().ToLower() }_D{DateTime.Now.ToString("yyyyMMdd")}_H{DateTime.Now.ToString("hhmmss")}.pfx";
        }
        else
        {
            return
                $"unknow_D{DateTime.Now.ToString("yyyyMMdd")}_H{DateTime.Now.ToString("hhmmss")}.pfx";
        }

    }
    private byte[] ReadFile(Stream stream)
    {
        int size = (int)stream.Length;
        byte[] data = new byte[size];
        stream.Read(data, 0, size);
        return data;
    }

}
