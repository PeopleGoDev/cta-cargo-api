using Azure.Storage.Files.Shares;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class UploadService : IUploadService
{
    private readonly ICiaAereaRepository _ciaaereaRepository;
    private readonly IAgenteDeCargaRepository _agenteDeCargaRepository;
    private readonly ICertificadoDigitalRepository _certificadoRepository;
    private readonly string _azureStorageConnectionString;
    private readonly string _azureStorageSharedFolder;
    private readonly IConfiguration _configuration;

    public UploadService(ICiaAereaRepository ciaaereaRepository,
        IAgenteDeCargaRepository agenteDeCargaRepository,
        ICertificadoDigitalRepository certificadoRepository, 
        IConfiguration configuration)
    {
        _ciaaereaRepository = ciaaereaRepository;
        _agenteDeCargaRepository = agenteDeCargaRepository;
        _certificadoRepository = certificadoRepository;
        _configuration = configuration;
        _azureStorageConnectionString = _configuration.GetConnectionString("AzureStorageConnectionString");
        _azureStorageSharedFolder = _configuration.GetConnectionString("AzureStorageSharedFolder");
    }

    public async Task<ApiResponse<UploadCertificadoResponseDto>> UploadArquivo(UploadFileRequest input, Stream fileStream)
    {

        // Instatiate a ShareServiceClient
        // Get a reference to a share named "sample-share"
        ShareClient share = new ShareClient(_azureStorageConnectionString, _azureStorageSharedFolder);

        try
        {
            string nomeArquivo;

            int certificadoId = -1;

            var x509Certificado2 = GetCertificate(fileStream, input.Senha);

            CertificadoDigital cert = await _certificadoRepository.GetCertificadoDigitalBySerialNumber(input.EmpresaId, x509Certificado2.SerialNumber);

            if (cert == null)
            {
                string primeirono = x509Certificado2.Subject.Split(",")[0];

                primeirono = primeirono.Replace("CN=", "").Replace("CD=", "");

                nomeArquivo = $"{ primeirono.Split(" ")[0].Trim().ToLower() }_D{DateTime.Now.ToString("yyyyMMdd")}_H{DateTime.Now.ToString("hhmmss")}.pfx";

                fileStream.Position = 0;

                ShareDirectoryClient directory = share.GetDirectoryClient("files");

                directory.CreateIfNotExists();

                ShareFileClient file = directory.GetFileClient(nomeArquivo);

                file.DeleteIfExists();

                await file.CreateAsync(fileStream.Length);

                await file.UploadAsync(fileStream);

                fileStream.Dispose();

                CertificadoDigital certificado = new CertificadoDigital
                {
                    Arquivo = nomeArquivo,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CriadoPeloId = input.UsuarioId,
                    DataVencimento = x509Certificado2.NotAfter,
                    EmpresaId = input.EmpresaId,
                    Senha = input.Senha,
                    NomeDono = primeirono,
                    SerialNumber = x509Certificado2.SerialNumber
                };

                _certificadoRepository.CreateCertificadoDigital(certificado);

                if (await _certificadoRepository.SaveChanges() == false)
                    throw new Exception("Erro desconhecido: Não foi possível gravar as informação na tabela Certificado Digital!");

                certificadoId = certificado.Id;

                // cia.CertificadoId = certificado.Id;
            }
            else
            {
                // cia.CertificadoId = cert.Id;
                certificadoId = cert.Id;

                nomeArquivo = cert.Arquivo;
            }

            switch (input.CertificadoDestino)
            {
                case FileDestinationMap.CiaAerea:

                    CiaAerea cia = await _ciaaereaRepository.GetCiaAereaById(input.Id);

                    if (cia == null)
                        throw new Exception("Companhia Aerea não encontrada!");

                    cia.CertificadoId = certificadoId;

                    _ciaaereaRepository.UpdateCiaAerea(cia);

                    if (await _ciaaereaRepository.SaveChanges() == false)
                        throw new Exception("Erro desconhecido: Não foi possível gravar as informação na tabela CiaAerea!");

                    break;

                case FileDestinationMap.AgenteDeCarga:

                    AgenteDeCarga agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(input.Id);

                    if (agenteDeCarga == null)
                        throw new Exception("Agente de Carga não encontrada!");

                    agenteDeCarga.CertificadoId = certificadoId;

                    _agenteDeCargaRepository.UpdateAgenteDeCarga(agenteDeCarga);

                    if(await _agenteDeCargaRepository.SaveChanges() == false)
                        throw new Exception("Erro desconhecido: Não foi possível gravar as informação na tabela AgenteDeCarga!");

                    break;
            }

            return new ApiResponse<UploadCertificadoResponseDto>()
            {
                Dados = new UploadCertificadoResponseDto()
                {
                    DataVencimento = x509Certificado2.NotAfter,
                    NomeArquivo = nomeArquivo
                },
                Sucesso = true,
                Notificacoes = null
            };

        }
        catch (Exception ex)
        {
            return new ApiResponse<UploadCertificadoResponseDto>()
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                    new Notificacao()
                    {
                        Codigo = "9999",
                        Mensagem = ex.Message
                    }
                }
            };
        }
    }
    private X509Certificate2 GetCertificate(Stream stream, string password)
    {
        //Create X509Certificate2 object from .cer file.
        byte[] rawData = ReadFile(stream);
        X509Certificate2 x509 = new X509Certificate2(rawData, password);
        return x509;
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
