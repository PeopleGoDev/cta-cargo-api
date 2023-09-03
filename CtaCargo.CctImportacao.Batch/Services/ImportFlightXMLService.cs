using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.FlightManifest;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace CtaCargo.CctImportacao.Batch.Services;

public class ImportFlightXMLService
{
    private int _empresaId;
    private int _usuarioId;
    private int _ciaaereaId;
    private readonly IVooRepository _vooRepository;
    private readonly IPortoIATARepository _portoIATARepository;
    private readonly ILogger _logger;
    public ImportFlightXMLService(IVooRepository vooRepository,
        IPortoIATARepository portoIATARepository, ILoggerFactory loggerFactory)
    {
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
        _logger = loggerFactory.CreateLogger<ImportFunction>();
    }

    public int EmpresaId { get { return _empresaId; } set { _empresaId = value; } }
    public int UsuarioId { get { return _usuarioId; } set { _usuarioId = value; } }
    public int CiaAereaId { get { return _ciaaereaId; } set { _ciaaereaId = value; } }
    public async Task<bool> ImportFlightXML(FlightManifestType arquivoVooXML)
    {
        DateTime? dataSaidaEstimada = null;
        DateTime? dataSaidaReal = null;
        DateTime? dataChegadaEstimada = null;
        DateTime? dataChegadaReal = null;
        DateTime? dataVoo = null;

        DateTime? dataPartida = arquivoVooXML.LogisticsTransportMovement?.DepartureEvent?.DepartureOccurrenceDateTime;
        DateTime? dataEmissao = arquivoVooXML.MessageHeaderDocument?.IssueDateTime;
        string? tipoPartida = arquivoVooXML.LogisticsTransportMovement?.DepartureEvent?.DepartureDateTimeTypeCode?.Value;
        switch (tipoPartida)
        {
            case "S":
                dataSaidaEstimada = dataPartida;
                break;
            case "A":
                dataSaidaReal = dataPartida;
                break;
        }
        string? numeroVoo = arquivoVooXML.LogisticsTransportMovement?.ID?.Value;

        if (numeroVoo == null)
            throw new Exception(@"Não foi possível importar o arquivo. Número do voo não identificada em FlightManifest\LogisticsTransportMovement\ID.");

        if (dataSaidaEstimada != null)
            dataVoo = new DateTime(dataSaidaEstimada.Value.Year, dataSaidaEstimada.Value.Month, dataSaidaEstimada.Value.Day, 0, 0, 0, 0);
        else
            if (dataSaidaReal != null)
            dataVoo = new DateTime(dataSaidaReal.Value.Year, dataSaidaReal.Value.Month, dataSaidaReal.Value.Day, 0, 0, 0, 0);

        if (dataVoo == null)
            throw new Exception(@"Não foi possível importar o arquivo. Data estimada do voo não identificada em FlightManifest\LogisticsTransportMovement\DepartureEvent\DepartureOccurrenceDateTime, DepartureDateTimeTypeCode = S.");

        Voo voo = _vooRepository.GetVooIdByDataVooNumero(_empresaId, dataVoo.Value, numeroVoo);
        if (voo != null && voo.ModificadoPeloId != null)
            throw new Exception($"Não é possível alterar o voo {numeroVoo}. Voo já foi alterado por outro usuário!");

        decimal? pesoTotal = arquivoVooXML.LogisticsTransportMovement?.TotalGrossWeightMeasure?.Value;
        decimal? volumeTotal = arquivoVooXML.LogisticsTransportMovement?.TotalGrossVolumeMeasure?.Value;
        decimal? pecasTotal = arquivoVooXML.LogisticsTransportMovement?.TotalPieceQuantity?.Value;
        decimal? pacotesTotal = arquivoVooXML.LogisticsTransportMovement?.TotalPackageQuantity?.Value;
        string? portoOrigemCode = arquivoVooXML.LogisticsTransportMovement?.DepartureEvent?.OccurrenceDepartureLocation?.ID?.Value;
        string? portoOrigemNome = arquivoVooXML.LogisticsTransportMovement?.DepartureEvent?.OccurrenceDepartureLocation?.Name?.Value;
        string? portoOrigemTipo = arquivoVooXML.LogisticsTransportMovement?.DepartureEvent?.OccurrenceDepartureLocation?.TypeCode?.Value;
        string? portoDestinoCode = null;
        string? portoDestinoNome = null;
        string? portoDestinoTipo = null;

        if (portoOrigemCode == null)
            throw new Exception(@"Não foi possível importar o arquivo. Porto de origem não identificado em FlightManifest\LogisticsTransportMovement\DepartureEvent\OccurrenceDepartureLocation\ID.");

        if (arquivoVooXML.ArrivalEvent != null)
        {
            var item = arquivoVooXML.ArrivalEvent[0];
            DateTime? dataChegada = item.ArrivalOccurrenceDateTime;
            string? tipoChegada = item.ArrivalDateTimeTypeCode?.Value;
            switch (tipoChegada)
            {
                case "S":
                    dataChegadaEstimada = dataChegada;
                    break;
                case "A":
                    dataChegadaReal = dataChegada;
                    break;
            }
            portoDestinoCode = item.OccurrenceArrivalLocation?.ID?.Value;
            portoDestinoNome = item.OccurrenceArrivalLocation?.Name?.Value;
            portoDestinoTipo = item.OccurrenceArrivalLocation?.TypeCode?.Value;
        }

        if (portoDestinoCode == null)
            throw new Exception(@"Não foi possível importar o arquivo. Porto de destino não identificado em FlightManifest\LogisticsTransportMovement\DepartureEvent\OccurrenceDepartureLocation\ID.");

        if (voo == null)
        {
            voo = new Voo();
            voo.Trechos = new List<VooTrecho>();
        }

        voo.DataVoo = dataVoo.Value;
        voo.DataEmissaoXML = dataEmissao;
        voo.EmpresaId = _empresaId;
        voo.CriadoPeloId = _usuarioId;
        voo.CiaAereaId = _ciaaereaId;
        voo.CreatedDateTimeUtc = DateTime.UtcNow;
        voo.Numero = numeroVoo;
        voo.DataHoraSaidaEstimada = dataSaidaEstimada;
        voo.DataHoraSaidaReal = dataSaidaReal;
        voo.DataHoraChegadaEstimada = dataChegadaEstimada;
        voo.DataHoraChegadaReal = dataChegadaReal;
        voo.AeroportoOrigemCodigo = portoOrigemCode;
        voo.AeroportoDestinoCodigo = portoDestinoCode;
        voo.PortoIataOrigemId = await GetPortoIataIdByCode(portoOrigemCode, portoOrigemNome);
        voo.PortoIataDestinoId = await GetPortoIataIdByCode(portoDestinoCode, portoDestinoNome);
        
        voo.TotalPesoBruto = pesoTotal == null ? null : Decimal.ToDouble(pesoTotal.Value);

        bool hasWeightUnit = arquivoVooXML.LogisticsTransportMovement?.TotalGrossWeightMeasure?.unitCodeSpecified ?? false;

        voo.TotalPesoBrutoUnidade = hasWeightUnit ?
            arquivoVooXML.LogisticsTransportMovement?.TotalGrossWeightMeasure?.unitCode.ToString(): null;

        voo.TotalVolumeBruto = volumeTotal == null ? null : Decimal.ToDouble(volumeTotal.Value);

        bool temVolumeUN = arquivoVooXML.LogisticsTransportMovement?.TotalGrossVolumeMeasure?.unitCodeSpecified ?? false;

        voo.TotalVolumeBrutoUnidade = hasWeightUnit ?
            arquivoVooXML.LogisticsTransportMovement?.TotalGrossVolumeMeasure?.unitCode.ToString() : null;

        voo.TotalPecas = pecasTotal == null ? null : Convert.ToInt32(pecasTotal.Value);
        
        voo.TotalPacotes = pacotesTotal == null ? null : Convert.ToInt32(pacotesTotal.Value);

        if (arquivoVooXML.ArrivalEvent != null)
        {
            foreach (var arrivalEvent in arquivoVooXML.ArrivalEvent.OrderBy(x => x.ArrivalOccurrenceDateTime))
            {
                var port = arrivalEvent.OccurrenceArrivalLocation.ID.Value;
                var trecho = voo.Trechos.FirstOrDefault(x => x.AeroportoDestinoCodigo == port);
                if(trecho == null)
                {
                    trecho = new VooTrecho();
                    trecho.ULDs = new List<UldMaster>();
                }

                trecho.AeroportoDestinoCodigo = port;
                trecho.CreatedDateTimeUtc = DateTime.UtcNow;
                if (arrivalEvent.ArrivalDateTimeTypeCode != null)
                {
                    trecho.DataHoraChegadaEstimada = arrivalEvent.ArrivalDateTimeTypeCode?.Value == "S" ? arrivalEvent.ArrivalOccurrenceDateTime : null;
                };
                if (arrivalEvent.DepartureDateTimeTypeCode != null)
                {
                    trecho.DataHoraSaidaEstimada = arrivalEvent.DepartureDateTimeTypeCode?.Value == "S" ? arrivalEvent.DepartureOccurrenceDateTime : null;
                }
                trecho.CriadoPeloId = _usuarioId;
                trecho.EmpresaId = _empresaId;
                trecho.PortoIataDestinoId = await GetPortoIataIdByCode(arrivalEvent.OccurrenceArrivalLocation.ID.Value,
                                                                arrivalEvent.OccurrenceArrivalLocation.Name?.Value);

                if(arrivalEvent.AssociatedTransportCargo != null)
                {
                    foreach(var associated in arrivalEvent.AssociatedTransportCargo)
                    {
                        string? chacteristicCode = null;
                        string? idPrimary = null;
                        string? uldId = null;
                        if(associated.TypeCode.Value == "ULD")
                        {
                            chacteristicCode = associated.UtilizedUnitLoadTransportEquipment.CharacteristicCode.Value;
                            idPrimary = associated.UtilizedUnitLoadTransportEquipment.OperatingParty.PrimaryID.Value;
                            uldId = associated.UtilizedUnitLoadTransportEquipment.ID.Value;
                        }
                        if(associated.TypeCode.Value == "BLK")
                        {
                            chacteristicCode = "BLK";
                        }

                        if (associated.IncludedMasterConsignment != null)
                        {
                            foreach (var master in associated.IncludedMasterConsignment)
                            {
                                var masterNumber = master.TransportContractDocument.ID.Value.Replace("-", "");

                                var uld = trecho.ULDs.FirstOrDefault(x => x.MasterNumero == masterNumber &&
                                            x.ULDCaracteristicaCodigo == chacteristicCode &&
                                            x.ULDId == uldId &&
                                            x.ULDIdPrimario == idPrimary &&
                                            x.DataExclusao == null);

                                if (uld == null)
                                {
                                    uld = new UldMaster();
                                    uld.EmpresaId = _empresaId;
                                    uld.CriadoPeloId = _usuarioId;
                                    uld.CreatedDateTimeUtc = DateTime.UtcNow;
                                    uld.MasterNumero = masterNumber;
                                    uld.ULDId = uldId;
                                    uld.ULDCaracteristicaCodigo = chacteristicCode;
                                    uld.ULDIdPrimario = idPrimary;
                                }
                                uld.Peso = master.GrossWeightMeasure.Value;
                                uld.QuantidadePecas = Convert.ToInt32(master.TotalPieceQuantity.Value);
                                uld.TotalParcial = master.TransportSplitDescription.Value;
                                uld.PesoUN = master.GrossWeightMeasure.unitCode.ToString();
                                uld.VooId = voo.Id;

                                if(uld.Id == 0)
                                    trecho.ULDs.Add(uld);
                            }
                        }
                    }
                }

                if (trecho.Id == 0)
                    voo.Trechos.Add(trecho);
                else
                    _vooRepository.UpdateTrecho(trecho);
            }
        }

        if (voo.Id == 0)
            _vooRepository.CreateVoo(voo);
        else
            _vooRepository.UpdateVoo(voo);

        if (!await _vooRepository.SaveChanges())
            throw new Exception("Erro desconhecido. Não foi possível importar o arquivo de voo.");

        return true;
    }
    private bool ValidaVoo(Voo voo)
    {
        return true;
    }
    private async Task<int?> GetPortoIataIdByCode(string code, string? portoNome = null)
    {
        PortoIata porto = _portoIATARepository.GetPortoIATAByCode(_empresaId, code);

        if (porto == null && code != null)
        {
            PortoIata novoPorto = new PortoIata()
            {
                CreatedDateTimeUtc = DateTime.UtcNow,
                CriadoPeloId = _usuarioId,
                EmpresaId = _empresaId,
                Codigo = code.ToUpper().Trim(),
                Nome = portoNome ?? code
            };
            _portoIATARepository.CreatePortoIATA(novoPorto);
            await _portoIATARepository.SaveChanges();
            return (int)novoPorto.Id;
        }
        return (int)porto.Id;
    }
}
