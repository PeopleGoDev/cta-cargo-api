﻿using CtaCargo.CctImportacao.Batch.Services.Interface;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.FlightManifest;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace CtaCargo.CctImportacao.Batch.Services;

public class FunctionTimerService
{
    private readonly IConfiguraRepository _configuraRepository;
    private readonly ImportFlightXMLService _importFlightXMLService;
    private readonly ILogger _logger;
    public FunctionTimerService(IConfiguraRepository configuraRepository, ImportFlightXMLService importFlightXMLService,
        ILoggerFactory loggerFactory)
    {
        _configuraRepository = configuraRepository;
        _importFlightXMLService = importFlightXMLService;
        _logger = loggerFactory.CreateLogger<ImportFunction>();
    }

    public async Task CheckFiles()
    {
        List<Configura> diretorios = _configuraRepository.GetAllAvailableConfiguration();

        foreach( var dir in diretorios )
        {
            if(dir.ConfiguracaoTipo=="ASF")
            {
                var impservice = new AzureSharedFolderService(dir.ConfiguracaoValor, dir.ConfiguracaoAdicional, dir.ConfiguracaoAdicional2);

                _importFlightXMLService.CiaAereaId = dir.CiaAereaId;
                _importFlightXMLService.EmpresaId = dir.EmpresaId;
                _importFlightXMLService.UsuarioId = dir.UsuarioId;

                var files = impservice.ListFiles();

                List<DirectoryItemFile> xffmFiles = files.Where(x => x.Name.StartsWith("XFFM")).ToList();
                List<DirectoryItemFile> xfwbFiles = files.Where(x => x.Name.StartsWith("XFWB")).ToList();

                foreach (var file in xffmFiles)
                {
                    try
                    {
                        var fileStream = impservice.ReadFile(file.Name);

                        FlightManifestType? arquivoVooXML = DeserializeFromStream<FlightManifestType>(fileStream);

                        if (arquivoVooXML != null)
                        {
                            var result = await _importFlightXMLService.ImportFlightXML(arquivoVooXML);
                            impservice.MoveToImportFolder(file.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        impservice.MoveToErrorFolder(file.Name);
                    }
                }
            }
        }
        Console.WriteLine(diretorios.Count);
    }

    private T? DeserializeFromStream<T>(Stream tr)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        try
        {
            var reportReq = (T?)serializer.Deserialize(tr);
            return reportReq;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return default(T?);
        }
    }
}
