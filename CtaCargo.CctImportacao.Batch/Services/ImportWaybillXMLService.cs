using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.WaybillManifest;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Batch.Services;

public class ImportWaybillXMLService
{
    private int _empresaId;
    private int _usuarioId;
    private int _ciaaereaId;
    private readonly IVooRepository _vooRepository;
    private readonly IMasterRepository _masterRepository;
    private readonly IPortoIATARepository _portoIATARepository;

    public ImportWaybillXMLService(IVooRepository vooRepository,
    IPortoIATARepository portoIATARepository,
    IMasterRepository masterRepository)
    {
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
        _masterRepository = masterRepository;
    }

    public int EmpresaId { get { return _empresaId; } set { _empresaId = value; } }
    public int UsuarioId { get { return _usuarioId; } set { _usuarioId = value; } }
    public int CiaAereaId { get { return _ciaaereaId; } set { _ciaaereaId = value; } }

    public bool WaybillXmlParaEntity(WaybillType arquivoVooXML)
    {
        //string numero = arquivoVooXML.BusinessHeaderDocument?.ID?.Value;
        //Regex rgx = new Regex("[^0-9]");
        //numero = rgx.Replace(numero, "");

        //Master master = _masterRepository.GetMasterByNumeroMaisAtual(_empresaId, _ciaaereaId, numero);

        //decimal valorPP = 0;
        //string valorPPUN = null;
        //decimal valorFC = 0;
        //string valorFCUN = null;
        //if (arquivoVooXML.MasterConsignment?.ApplicableTotalRating != null)
        //{
        //    foreach (TotalRatingType item in arquivoVooXML.MasterConsignment?.ApplicableTotalRating)
        //    {
        //        if (item.ApplicablePrepaidCollectMonetarySummation != null)
        //            foreach (var frete in item.ApplicablePrepaidCollectMonetarySummation)
        //            {
        //                switch (frete.PrepaidIndicator)
        //                {
        //                    case true:
        //                        // Frete PrePaid
        //                        valorPP = frete.GrandTotalAmount?.Value ?? 0;
        //                        valorPPUN = (bool)frete.GrandTotalAmount?.currencyIDSpecified ? frete.GrandTotalAmount?.currencyID.ToString() : null;
        //                        break;
        //                    case false:
        //                        // Frete Collect
        //                        valorFC = frete.GrandTotalAmount?.Value ?? 0;
        //                        valorFCUN = (bool)frete.GrandTotalAmount?.currencyIDSpecified ? frete.GrandTotalAmount?.currencyID.ToString() : null;
        //                        break;
        //                }
        //            }
        //    }
        //}

        //if (master == null)
        //{
        //    master = new Master();
        //    master.Numero = numero;
        //    master.DataEmissaoXML = arquivoVooXML.MessageHeaderDocument?.IssueDateTime;
        //    master.EmpresaId = _empresaId;
        //    master.CriadoPeloId = _usuarioId;
        //    master.CreatedDateTimeUtc = DateTime.UtcNow;
        //    master.CodigoConteudo = arquivoVooXML.BusinessHeaderDocument.IncludedHeaderNote?[0].ContentCode?.Value;
        //    master.ProdutoSituacaoAlfandega = arquivoVooXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
        //    master.PesoTotalBruto = decimal.ToDouble(arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
        //    bool temPesoUN = arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCodeSpecified ?? false;
        //    master.PesoTotalBrutoUN = temPesoUN ? arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
        //    master.TotalPecas = Convert.ToInt32(arquivoVooXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
        //    master.ValorFretePP = valorPP;
        //    master.ValorFretePPUN = valorPPUN;
        //    master.ValorFreteFC = valorFC;
        //    master.ValorFreteFCUN = valorFCUN;
        //    master.IndicadorMadeiraMacica = arquivoVooXML.MasterConsignment?.IncludedCustomsNote?[0].ContentCode?.Value == "DI";
        //    master.IndicadorNaoDesunitizacao = false;
        //    master.DescricaoMercadoria = arquivoVooXML.MasterConsignment?.ApplicableRating?[0].IncludedMasterConsignmentItem[0].NatureIdentificationTransportCargo?.Identification?.Value;
        //    master.CodigoRecintoAduaneiro = 123456;
        //    master.RUC = null;
        //    master.ExpedidorNome = arquivoVooXML.MasterConsignment?.ConsignorParty?.Name?.Value;
        //    master.ExpedidorEndereco = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.StreetName?.Value;
        //    master.ExpedidorPostal = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.PostcodeCode?.Value;
        //    master.ExpedidorCidade = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CityName?.Value;
        //    master.ExpedidorPaisCodigo = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
        //    master.ExpedidorSubdivisao = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
        //    master.ConsignatarioNome = arquivoVooXML.MasterConsignment?.ConsigneeParty?.Name?.Value;
        //    master.ConsignatarioEndereco = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.StreetName?.Value;
        //    master.ConsignatarioPostal = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.PostcodeCode?.Value;
        //    master.ConsignatarioCidade = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CityName?.Value;
        //    master.ConsignatarioPaisCodigo = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
        //    master.ConsignatarioSubdivisao = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
        //    master.EmissorNome = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.Name?.Value;
        //    master.EmissorEndereco = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.StreetName?.Value;
        //    master.EmissorPostal = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.PostcodeCode?.Value;
        //    master.EmissorCidade = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CityName?.Value;
        //    master.EmissorPaisCodigo = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CountryID?.Value.ToString();
        //    master.EmissorCargoAgenteLocalizacao = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.CargoAgentID?.Value;

        //    string portoOrigemCode = arquivoVooXML.MasterConsignment?.OriginLocation?.ID?.Value;
        //    string portoOrigemNome = arquivoVooXML.MasterConsignment?.OriginLocation?.Name?.Value;
        //    string portoDestinoCode = arquivoVooXML.MasterConsignment?.FinalDestinationLocation?.ID?.Value;
        //    string portoDestinoNome = arquivoVooXML.MasterConsignment?.FinalDestinationLocation?.Name?.Value;

        //    master.AeroportoOrigemId = GetPortoIataIdByCode(_empresaId, portoOrigemCode, portoOrigemNome);
        //    master.AeroportoDestinoId = GetPortoIataIdByCode(_empresaId, portoDestinoCode, portoDestinoNome);
        //    master.OutrasInstrucoesManuseio = arquivoVooXML.MasterConsignment?.HandlingOSIInstructions?[0].Description?.Value;
        //    master.CodigoManuseioProdutoAlgandega = arquivoVooXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
        //    master.MoedaAplicadaOrigem = arquivoVooXML.MasterConsignment?.ApplicableOriginCurrencyExchange?.SourceCurrencyCode?.Value.ToString();
        //    master.PesoTotalBrutoXML = decimal.ToDouble(arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
        //    master.PesoTotalBrutoUNXML = temPesoUN ? arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
        //    master.TotalPecasXML = Convert.ToInt32(arquivoVooXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
        //    master.ValorFretePPXML = valorPP;
        //    master.ValorFretePPUNXML = valorPPUN;
        //    master.ValorFreteFCXML = valorFC;
        //    master.ValorFreteFCUNXML = valorFCUN;
        //    master.MeiaEntradaXML = true;
        //    master.CiaAereaId = _ciaaereaId;
        //    master.AutenticacaoSignatarioData = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.ActualDateTime;
        //    master.AutenticacaoSignatariaNome = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.Signatory?.Value;
        //    master.AutenticacaoSignatariaLocal = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.IssueAuthenticationLocation?.Name?.Value;

        //    master.VooNumeroXML = arquivoVooXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?[0].UsedLogisticsTransportMeans?.Name?.Value;
        //    if (ValidaMaster(master))
        //    {
        //        _masterRepository.CreateMaster(master);
        //        _masterRepository.SaveChanges();
        //        return true;
        //    }
        //}
        //else
        //{
        //    DateTime dataMin = DateTime.UtcNow.AddDays(-1);
        //    DateTime dataMax = DateTime.UtcNow.AddDays(1);
        //    if (master.CreatedDateTimeUtc >= dataMin && master.CreatedDateTimeUtc <= dataMax && master.SituacaoRFBId != Master.RFStatusEnvioType.Processed)
        //    {
        //        master.PesoTotalBrutoXML = decimal.ToDouble(arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
        //        bool temPesoUN = arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCodeSpecified ?? false;
        //        master.PesoTotalBrutoUNXML = temPesoUN ? arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
        //        master.TotalPecasXML = Convert.ToInt32(arquivoVooXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
        //        master.ValorFretePPXML = valorPP;
        //        master.ValorFretePPUNXML = valorPPUN;
        //        master.ValorFreteFCXML = valorFC;
        //        master.ValorFreteFCUNXML = valorFCUN;
        //        master.DataEmissaoXML = arquivoVooXML.MessageHeaderDocument?.IssueDateTime;
        //        if (master.MeiaEntradaXML)
        //        {
        //            master.CodigoConteudo = arquivoVooXML.BusinessHeaderDocument.IncludedHeaderNote?[0].ContentCode?.Value;
        //            master.ProdutoSituacaoAlfandega = arquivoVooXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
        //            master.PesoTotalBruto = decimal.ToDouble(arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.Value ?? 0);
        //            master.PesoTotalBrutoUN = temPesoUN ? arquivoVooXML.MasterConsignment?.IncludedTareGrossWeightMeasure?.unitCode.ToString() : null;
        //            master.TotalPecas = Convert.ToInt32(arquivoVooXML.MasterConsignment?.TotalPieceQuantity?.Value ?? 0);
        //            master.ValorFretePP = valorPP;
        //            master.ValorFretePPUN = valorPPUN;
        //            master.ValorFreteFC = valorFC;
        //            master.ValorFreteFCUN = valorFCUN;
        //            master.IndicadorMadeiraMacica = arquivoVooXML.MasterConsignment?.IncludedCustomsNote?[0].ContentCode?.Value == "DI";
        //            master.IndicadorNaoDesunitizacao = false;
        //            master.DescricaoMercadoria = arquivoVooXML.MasterConsignment?.ApplicableRating?[0].IncludedMasterConsignmentItem[0].NatureIdentificationTransportCargo?.Identification?.Value;
        //            master.CodigoRecintoAduaneiro = 123456;
        //            master.RUC = null;
        //            master.ExpedidorNome = arquivoVooXML.MasterConsignment?.ConsignorParty?.Name?.Value;
        //            master.ExpedidorEndereco = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.StreetName?.Value;
        //            master.ExpedidorPostal = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.PostcodeCode?.Value;
        //            master.ExpedidorCidade = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CityName?.Value;
        //            master.ExpedidorPaisCodigo = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
        //            master.ExpedidorSubdivisao = arquivoVooXML.MasterConsignment?.ConsignorParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
        //            master.ConsignatarioNome = arquivoVooXML.MasterConsignment?.ConsigneeParty?.Name?.Value;
        //            master.ConsignatarioEndereco = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.StreetName?.Value;
        //            master.ConsignatarioPostal = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.PostcodeCode?.Value;
        //            master.ConsignatarioCidade = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CityName?.Value;
        //            master.ConsignatarioPaisCodigo = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountryID?.Value.ToString();
        //            master.ConsignatarioSubdivisao = arquivoVooXML.MasterConsignment?.ConsigneeParty?.PostalStructuredAddress?.CountrySubDivisionName?.Value;
        //            master.EmissorNome = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.Name?.Value;
        //            master.EmissorEndereco = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.StreetName?.Value;
        //            master.EmissorPostal = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.PostcodeCode?.Value;
        //            master.EmissorCidade = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CityName?.Value;
        //            master.EmissorPaisCodigo = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.FreightForwarderAddress?.CountryID?.Value.ToString();
        //            master.EmissorCargoAgenteLocalizacao = arquivoVooXML.MasterConsignment?.FreightForwarderParty?.CargoAgentID?.Value;

        //            string portoOrigemCode = arquivoVooXML.MasterConsignment?.OriginLocation?.ID?.Value;
        //            string portoOrigemNome = arquivoVooXML.MasterConsignment?.OriginLocation?.Name?.Value;
        //            string portoDestinoCode = arquivoVooXML.MasterConsignment?.FinalDestinationLocation?.ID?.Value;
        //            string portoDestinoNome = arquivoVooXML.MasterConsignment?.FinalDestinationLocation?.Name?.Value;

        //            master.AeroportoOrigemId = GetPortoIataIdByCode(_empresaId, portoOrigemCode, portoOrigemNome);
        //            master.AeroportoDestinoId = GetPortoIataIdByCode(_empresaId, portoDestinoCode, portoDestinoNome);
        //            master.OutrasInstrucoesManuseio = arquivoVooXML.MasterConsignment?.HandlingOSIInstructions?[0].Description?.Value;
        //            master.CodigoManuseioProdutoAlgandega = arquivoVooXML.MasterConsignment?.AssociatedConsignmentCustomsProcedure?.GoodsStatusCode?.Value;
        //            master.MoedaAplicadaOrigem = arquivoVooXML.MasterConsignment?.ApplicableOriginCurrencyExchange?.SourceCurrencyCode?.Value.ToString();
        //            master.MeiaEntradaXML = false;
        //            master.VooNumeroXML = arquivoVooXML.MasterConsignment?.SpecifiedLogisticsTransportMovement?[0].UsedLogisticsTransportMeans?.Name?.Value;
        //            master.AutenticacaoSignatarioData = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.ActualDateTime;
        //            master.AutenticacaoSignatariaNome = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.Signatory?.Value;
        //            master.AutenticacaoSignatariaLocal = arquivoVooXML.BusinessHeaderDocument?.SignatoryCarrierAuthentication?.IssueAuthenticationLocation?.Name?.Value;
        //        }
        //        _masterRepository.SaveChanges();
        //        return true;
        //    }
        //}
        // sem efeito
        return true;
    }
    private bool ValidaMaster(Master master)
    {
        return true;
    }
    private int GetPortoIataIdByCode(int empresaId, string code, string portoNome)
    {
        PortoIata porto = _portoIATARepository.GetPortoIATAByCode(empresaId, code);

        if (porto == null)
        {
            PortoIata novoPorto = new PortoIata()
            {
                CreatedDateTimeUtc = DateTime.UtcNow,
                CriadoPeloId = _usuarioId,
                EmpresaId = _empresaId,
                Codigo = code.ToUpper().Trim(),
                Nome = portoNome.ToUpper().Trim()
            };
            _portoIATARepository.CreatePortoIATA(novoPorto);
            _portoIATARepository.SaveChanges();
            return (int)novoPorto.Id;

        }
        return (int)porto.Id;
    }
}
