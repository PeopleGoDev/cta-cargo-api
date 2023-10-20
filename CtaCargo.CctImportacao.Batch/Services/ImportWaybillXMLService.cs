using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.WaybillManifest;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace CtaCargo.CctImportacao.Batch.Services;

public class ImportWaybillXMLService
{
    private int _empresaId;
    private int _usuarioId;
    private int _ciaaereaId;
    private readonly IVooRepository _vooRepository;
    private readonly IMasterRepository _masterRepository;
    private readonly IPortoIATARepository _portoIATARepository;
    private readonly IUldMasterRepository _uldMasterRepository;
    private readonly IValidadorMaster _validadorMaster;
    public ImportWaybillXMLService(IVooRepository vooRepository,
    IPortoIATARepository portoIATARepository,
    IMasterRepository masterRepository,
    IUldMasterRepository uldMasterRepository,
    IValidadorMaster validadorMaster)
    {
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
        _masterRepository = masterRepository;
        _uldMasterRepository = uldMasterRepository;
        _validadorMaster = validadorMaster;
    }

    public int EmpresaId { get { return _empresaId; } set { _empresaId = value; } }
    public int UsuarioId { get { return _usuarioId; } set { _usuarioId = value; } }
    public int CiaAereaId { get { return _ciaaereaId; } set { _ciaaereaId = value; } }

    public async Task<bool> ImportWaybillXml(WaybillType masterXML)
    {
        string numero = masterXML.BusinessHeaderDocument?.ID?.Value;
        Regex rgx = new Regex("[^0-9]");
        numero = rgx.Replace(numero, "");

        Master master = await _masterRepository.GetMasterByNumber(_empresaId, numero);

        decimal valorPP = 0;
        string valorPPUN = null;
        decimal valorFC = 0;
        string valorFCUN = null;
        if (masterXML.MasterConsignment?.ApplicableTotalRating != null)
        {
            foreach (TotalRatingType item in masterXML.MasterConsignment?.ApplicableTotalRating)
            {
                if (item.ApplicablePrepaidCollectMonetarySummation != null)
                    foreach (var frete in item.ApplicablePrepaidCollectMonetarySummation)
                    {
                        switch (frete.PrepaidIndicator)
                        {
                            case true:
                                // Frete PrePaid
                                valorPP = frete.GrandTotalAmount?.Value ?? 0;
                                valorPPUN = (bool)frete.GrandTotalAmount?.currencyIDSpecified ? frete.GrandTotalAmount?.currencyID.ToString() : null;
                                break;
                            case false:
                                // Frete Collect
                                valorFC = frete.GrandTotalAmount?.Value ?? 0;
                                valorFCUN = (bool)frete.GrandTotalAmount?.currencyIDSpecified ? frete.GrandTotalAmount?.currencyID.ToString() : null;
                                break;
                        }
                    }
            }
            if (valorPP > 0 & valorFC == 0)
                valorFCUN = valorPPUN;

            if (valorFC > 0 & valorPP == 0)
                valorPPUN = valorFCUN;
        }

        var tipoConteudo = () =>
        {
            if (masterXML.MessageHeaderDocument?.TypeCode?.Value == DocumentNameCodeContentType.Item740)
                return "D";
            else if (masterXML.MessageHeaderDocument?.TypeCode?.Value == DocumentNameCodeContentType.Item741)
                return "C";
            else
                return "C";
        };

        if (master == null)
        {
            master = new Master();
            master.Numero = numero;
            master.EmpresaId = _empresaId;
            master.CriadoPeloId = _usuarioId;
            master.CreatedDateTimeUtc = DateTime.UtcNow;
            master.CodigoConteudo = tipoConteudo();

            if(masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication != null)
            {
                var signatory = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication;

                master.DataEmissaoXML = signatory?.ActualDateTime;
                master.AutenticacaoSignatarioData = signatory?.ActualDateTime;
                master.AutenticacaoSignatariaLocal = signatory?.IssueAuthenticationLocation?.Name?.Value;
                master.AutenticacaoSignatariaNome = signatory?.Signatory?.Value;
            };

            master.ProdutoSituacaoAlfandega = masterXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
            master.PesoTotalBruto = decimal.ToDouble(masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
            bool temPesoUN = masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCodeSpecified ?? false;
            master.PesoTotalBrutoUN = temPesoUN ? masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
            master.TotalPecas = Convert.ToInt32(masterXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
            master.ValorFretePP = valorPP;
            master.ValorFretePPUN = valorPPUN;
            master.ValorFreteFC = valorFC;
            master.ValorFreteFCUN = valorFCUN;
            master.IndicadorMadeiraMacica = masterXML.MasterConsignment?.IncludedCustomsNote?[0].ContentCode?.Value == "DI";
            master.IndicadorNaoDesunitizacao = false;
            master.DescricaoMercadoria = masterXML.MasterConsignment?.ApplicableRating?[0].IncludedMasterConsignmentItem[0].NatureIdentificationTransportCargo?.Identification?.Value;
            master.CodigoRecintoAduaneiro = "0";
            master.RUC = null;
            master.ExpedidorNome = masterXML.MasterConsignment?.ConsignorParty?.Name?.Value;
            master.ExpedidorEndereco = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.StreetName?.Value;
            master.ExpedidorPostal = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.PostcodeCode?.Value;
            master.ExpedidorCidade = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CityName?.Value;
            master.ExpedidorPaisCodigo = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
            master.ExpedidorSubdivisao = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
            master.ConsignatarioNome = masterXML.MasterConsignment?.ConsigneeParty?.Name?.Value;
            master.ConsignatarioEndereco = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.StreetName?.Value;
            master.ConsignatarioPostal = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.PostcodeCode?.Value;
            master.ConsignatarioCidade = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CityName?.Value;
            master.ConsignatarioPaisCodigo = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
            master.ConsignatarioSubdivisao = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
            master.EmissorNome = masterXML.MasterConsignment?.FreightForwarderParty?.Name?.Value;
            master.EmissorEndereco = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.StreetName?.Value;
            master.EmissorPostal = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.PostcodeCode?.Value;
            master.EmissorCidade = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CityName?.Value;
            master.EmissorPaisCodigo = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CountryID?.Value.ToString();
            master.EmissorCargoAgenteLocalizacao = masterXML.MasterConsignment?.FreightForwarderParty?.CargoAgentID?.Value;

            string portoOrigemCode = masterXML.MasterConsignment?.OriginLocation?.ID?.Value;
            string portoOrigemNome = masterXML.MasterConsignment?.OriginLocation?.Name?.Value;
            string portoDestinoCode = masterXML.MasterConsignment?.FinalDestinationLocation?.ID?.Value;
            string portoDestinoNome = masterXML.MasterConsignment?.FinalDestinationLocation?.Name?.Value;

            master.AeroportoOrigemCodigo = portoOrigemCode;
            master.AeroportoDestinoCodigo = portoDestinoCode;
            master.AeroportoOrigemId = await GetPortoIataIdByCode(_empresaId, portoOrigemCode, portoOrigemNome);
            master.AeroportoDestinoId = await GetPortoIataIdByCode(_empresaId, portoDestinoCode, portoDestinoNome);
            if(masterXML.MasterConsignment?.HandlingSPHInstructions != null)
            {
                master.NaturezaCarga = string.Join(",",masterXML.MasterConsignment?.HandlingSPHInstructions.Select(x => x.DescriptionCode.Value));
            }
            master.OutrasInstrucoesManuseio = masterXML.MasterConsignment?.HandlingOSIInstructions?[0].Description?.Value;
            master.CodigoManuseioProdutoAlgandega = masterXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
            master.MoedaAplicadaOrigem = masterXML.MasterConsignment?.ApplicableOriginCurrencyExchange?.SourceCurrencyCode?.Value.ToString();
            master.PesoTotalBrutoXML = decimal.ToDouble(masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
            master.PesoTotalBrutoUNXML = temPesoUN ? masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
            master.TotalPecasXML = Convert.ToInt32(masterXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
            master.ValorFretePPXML = valorPP;
            master.ValorFretePPUNXML = valorPPUN;
            master.ValorFreteFCXML = valorFC;
            master.ValorFreteFCUNXML = valorFCUN;
            master.MeiaEntradaXML = true;
            master.CiaAereaId = _ciaaereaId;
            master.AutenticacaoSignatarioData = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.ActualDateTime;
            master.AutenticacaoSignatariaNome = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.Signatory?.Value;
            master.AutenticacaoSignatariaLocal = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.IssueAuthenticationLocation?.Name?.Value;

            master.VooNumeroXML = masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?[0].ID?.Value;

            if (masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?.Length > 0)
            {
                foreach (var logisticTransportMovement in masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement)
                {
                    var airportOfDestiny = logisticTransportMovement.ArrivalEvent?.OccurrenceArrivalLocation?.ID;
                    var estimateArrival = logisticTransportMovement.ArrivalEvent?.ScheduledOccurrenceDateTime;
                    var voo = await GetFlightId(airportOfDestiny.Value, estimateArrival, numero);
                    if (voo != null)
                    {
                        master.VooId = voo.Id;
                        master.VooNumeroXML = voo.Numero;
                        break;
                    }
                }
            }

            if (masterXML.MasterConsignment?.IncludedCustomsNote != null)
            {
                foreach (var customsNote in masterXML.MasterConsignment.IncludedCustomsNote)
                {
                    if (customsNote.SubjectCode?.Value == "CNE")
                    {
                        if (customsNote.Content.Value.StartsWith("CNPJ"))
                        {
                            master.ConsignatarioCNPJ = Regex.Replace(customsNote.Content.Value, "[^0-9]", "").Substring(0, 14);
                            break;
                        }
                        else if (customsNote.Content.Value.StartsWith("CPF"))
                        {
                            master.ConsignatarioCNPJ = Regex.Replace(customsNote.Content.Value, "[^0-9]", "").Substring(0, 14);
                            break;
                        }
                        else if (customsNote.Content.Value.StartsWith("PASSPORT"))
                        {
                            master.ConsignatarioCNPJ = $"CC{customsNote.Content.Value.Replace("PASSPORT", "").Substring(0, 12)}";
                            break;
                        }
                    }
                }
            }

            _validadorMaster.InserirErrosMaster(master);
            _masterRepository.CreateMaster(_empresaId, master);
            var result = await _masterRepository.SaveChanges();
            await UpdateUld(master);
            return result;
        }
        else
        {
            DateTime dataMin = DateTime.UtcNow.AddDays(-30);
            DateTime dataMax = DateTime.UtcNow.AddDays(1);
            if (master.CreatedDateTimeUtc >= dataMin && master.CreatedDateTimeUtc <= dataMax && master.SituacaoRFBId == Master.RFStatusEnvioType.NoSubmitted)
            {
                if (masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication != null)
                {
                    var signatory = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication;

                    master.DataEmissaoXML = signatory?.ActualDateTime;
                    master.AutenticacaoSignatarioData = signatory?.ActualDateTime;
                    master.AutenticacaoSignatariaLocal = signatory?.IssueAuthenticationLocation?.Name?.Value;
                    master.AutenticacaoSignatariaNome = signatory?.Signatory?.Value;
                };

                master.PesoTotalBrutoXML = decimal.ToDouble(masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
                bool temPesoUN = masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCodeSpecified ?? false;
                master.PesoTotalBrutoUNXML = temPesoUN ? masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
                master.TotalPecasXML = Convert.ToInt32(masterXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
                master.ValorFretePPXML = valorPP;
                master.ValorFretePPUNXML = valorPPUN;
                master.ValorFreteFCXML = valorFC;
                master.ValorFreteFCUNXML = valorFCUN;
                master.CodigoConteudo = tipoConteudo();
                master.ProdutoSituacaoAlfandega = masterXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
                master.PesoTotalBruto = decimal.ToDouble(masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
                master.PesoTotalBrutoUN = temPesoUN ? masterXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
                master.TotalPecas = Convert.ToInt32(masterXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
                master.ValorFretePP = valorPP;
                master.ValorFretePPUN = valorPPUN;
                master.ValorFreteFC = valorFC;
                master.ValorFreteFCUN = valorFCUN;
                master.IndicadorMadeiraMacica = masterXML.MasterConsignment?.IncludedCustomsNote?[0].ContentCode?.Value == "DI";
                master.IndicadorNaoDesunitizacao = false;
                master.DescricaoMercadoria = masterXML.MasterConsignment?.ApplicableRating?[0].IncludedMasterConsignmentItem[0].NatureIdentificationTransportCargo?.Identification?.Value;
                master.CodigoRecintoAduaneiro = "0";
                master.RUC = null;
                master.ExpedidorNome = masterXML.MasterConsignment?.ConsignorParty?.Name?.Value;
                master.ExpedidorEndereco = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.StreetName?.Value;
                master.ExpedidorPostal = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.PostcodeCode?.Value;
                master.ExpedidorCidade = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CityName?.Value;
                master.ExpedidorPaisCodigo = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
                master.ExpedidorSubdivisao = masterXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
                master.ConsignatarioNome = masterXML.MasterConsignment?.ConsigneeParty?.Name?.Value;
                master.ConsignatarioEndereco = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.StreetName?.Value;
                master.ConsignatarioPostal = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.PostcodeCode?.Value;
                master.ConsignatarioCidade = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CityName?.Value;
                master.ConsignatarioPaisCodigo = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
                master.ConsignatarioSubdivisao = masterXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
                master.EmissorNome = masterXML.MasterConsignment?.FreightForwarderParty?.Name?.Value;
                master.EmissorEndereco = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.StreetName?.Value;
                master.EmissorPostal = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.PostcodeCode?.Value;
                master.EmissorCidade = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CityName?.Value;
                master.EmissorPaisCodigo = masterXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CountryID?.Value.ToString();
                master.EmissorCargoAgenteLocalizacao = masterXML.MasterConsignment?.FreightForwarderParty?.CargoAgentID?.Value;
                string portoOrigemCode = masterXML.MasterConsignment?.OriginLocation?.ID?.Value;
                string portoOrigemNome = masterXML.MasterConsignment?.OriginLocation?.Name?.Value;
                string portoDestinoCode = masterXML.MasterConsignment?.FinalDestinationLocation?.ID?.Value;
                string portoDestinoNome = masterXML.MasterConsignment?.FinalDestinationLocation?.Name?.Value;
                master.AeroportoOrigemCodigo = portoOrigemCode;
                master.AeroportoDestinoCodigo = portoDestinoCode;
                master.AeroportoOrigemId = await GetPortoIataIdByCode(_empresaId, portoOrigemCode, portoOrigemNome);
                master.AeroportoDestinoId = await GetPortoIataIdByCode(_empresaId, portoDestinoCode, portoDestinoNome);
                if (masterXML.MasterConsignment?.HandlingSPHInstructions != null)
                {
                    master.NaturezaCarga = string.Join(",", masterXML.MasterConsignment?.HandlingSPHInstructions.Select(x => x.DescriptionCode.Value));
                }
                master.OutrasInstrucoesManuseio = masterXML.MasterConsignment?.HandlingOSIInstructions?[0].Description?.Value;
                master.CodigoManuseioProdutoAlgandega = masterXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
                master.MoedaAplicadaOrigem = masterXML.MasterConsignment?.ApplicableOriginCurrencyExchange?.SourceCurrencyCode?.Value.ToString();
                master.MeiaEntradaXML = false;
                master.AutenticacaoSignatarioData = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.ActualDateTime;
                master.AutenticacaoSignatariaNome = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.Signatory?.Value;
                master.AutenticacaoSignatariaLocal = masterXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.IssueAuthenticationLocation?.Name?.Value;

                master.VooNumeroXML = masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?[0].ID?.Value;

                if (master.VooId == null || master.VooId == 0)
                {
                    if (masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?.Length > 0)
                    {
                        foreach (var logisticTransportMovement in masterXML.MasterConsignment?.SpecifiedLogisticsTransportMovement)
                        {
                            var airportOfDestiny = logisticTransportMovement.ArrivalEvent?.OccurrenceArrivalLocation?.ID;
                            var estimateArrival = logisticTransportMovement.ArrivalEvent?.ScheduledOccurrenceDateTime;
                            var voo = await GetFlightId(airportOfDestiny.Value, estimateArrival, numero);
                            if (voo != null)
                            {
                                master.VooId = voo.Id;
                                master.VooNumeroXML = voo.Numero;
                                break;
                            }
                        }
                    }
                }

                if(masterXML.MasterConsignment?.IncludedCustomsNote != null)
                {
                    foreach(var customsNote in masterXML.MasterConsignment.IncludedCustomsNote)
                    {
                        if(customsNote.SubjectCode?.Value == "CNE")
                        {
                            if (customsNote.Content.Value.StartsWith("CNPJ"))
                            {
                                master.ConsignatarioCNPJ = Regex.Replace(customsNote.Content.Value, "[^0-9]", "").Substring(0,14);
                                break;
                            } else if (customsNote.Content.Value.StartsWith("CPF"))
                            {
                                master.ConsignatarioCNPJ = Regex.Replace(customsNote.Content.Value, "[^0-9]", "").Substring(0,14);
                                break;
                            } else if (customsNote.Content.Value.StartsWith("PASSPORT"))
                            {
                                master.ConsignatarioCNPJ = $"CC{customsNote.Content.Value.Replace("PASSPORT","").Substring(0,12)}";
                                break;
                            }
                        }
                    }
                }
                _validadorMaster.InserirErrosMaster(master);
                _masterRepository.UpdateMaster(master);
                var result = await _masterRepository.SaveChanges();
                await UpdateUld(master);
                return result;
            }
        }
        // sem efeito
        return true;
    }
    private bool ValidaMaster(Master master) => true;

    private async Task<int> GetPortoIataIdByCode(int empresaId, string code, string portoNome)
    {
        PortoIata porto = _portoIATARepository.GetPortoIATAByCode(empresaId, code);

        if (porto != null)
            return porto.Id;

        PortoIata novoPorto = new PortoIata()
        {
            CreatedDateTimeUtc = DateTime.UtcNow,
            CriadoPeloId = _usuarioId,
            EmpresaId = _empresaId,
            Codigo = code.ToUpper().Trim(),
            Nome = portoNome ?? code.ToUpper().Trim()
        };
        _portoIATARepository.CreatePortoIATA(novoPorto);
        await _portoIATARepository.SaveChanges();
        return novoPorto.Id;

    }

    private async Task<Voo> GetFlightId(string airportOfDestiny, DateTime? estimateArrival, string masterNumber)
    {
        var uld = await _uldMasterRepository.GetUldMasterByMasterNumber(_empresaId, masterNumber);
        if(uld != null)
            return uld.VooTrecho.VooInfo;
        
        DateTime sqlMinTime = new DateTime(1753, 1, 1, 12, 0, 0);
        if (airportOfDestiny != null && estimateArrival != null && estimateArrival > sqlMinTime)
        {
            var segment = _vooRepository.GetVooBySegment(_empresaId, airportOfDestiny, estimateArrival.Value);
            if (segment != null)
                return segment.VooInfo;
        }
        return null;
    }

    private async Task UpdateUld(Master master)
    {
        var uldList = await _uldMasterRepository.GetUldByMasterNumberForUpload(_empresaId, master.Numero);
        if(uldList != null && uldList.Count > 0)
        {
            foreach(var uld in uldList)
            {
                uld.MasterId = master.Id;
                _uldMasterRepository.UpdateUldMaster(uld);
            }
            _uldMasterRepository.SaveChanges();
        }
    }
}
