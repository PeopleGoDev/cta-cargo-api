using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Model;
using CtaCargo.CctImportacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class ReceitaHouseService : IReceitaHouseService
{
    private readonly ICertitificadoDigitalSupport _certificadoDigitalSupport;
    private readonly IHouseRepository _houseRepository;
    private readonly IAssociacaoRepository _masterHouseAssociacaoRepository;
    private readonly INaturezaCargaRepository _naturezaCargaRepository;
    private readonly IAutenticaReceitaFederal _autenticaReceitaFederal;
    private readonly IUploadReceitaFederal _uploadReceitaFederal;
    private readonly IMotorIataHouse _motorIataHouse;
    private readonly IMapper _mapper;

    #region Construtor
    public ReceitaHouseService(ICertitificadoDigitalSupport certificadoDigitalSupport,
        IAutenticaReceitaFederal autenticaReceitaFederal,
        IHouseRepository houseRepository,
        IAssociacaoRepository masterHouseAssociacaoRepository,
        IUploadReceitaFederal flightUploadReceitaFederal,
        IMapper mapper,
        IMotorIataHouse motorIataHouse,
        INaturezaCargaRepository naturezaCargaRepository)
    {
        _certificadoDigitalSupport = certificadoDigitalSupport;
        _autenticaReceitaFederal = autenticaReceitaFederal;
        _houseRepository = houseRepository;
        _uploadReceitaFederal = flightUploadReceitaFederal;
        _masterHouseAssociacaoRepository = masterHouseAssociacaoRepository;
        _mapper = mapper;
        _motorIataHouse = motorIataHouse;
        _naturezaCargaRepository = naturezaCargaRepository;
    }
    #endregion

    #region Métodos Publicos
    public async Task<ApiResponse<string>> SubmeterHousesAgentesDeCarga(
        UserSession userSession,
        SubmeterRFBHouseRequest input)
    {
        var processDate =
            new DateTime(input.DataProcessamento.Year,
            input.DataProcessamento.Month,
            input.DataProcessamento.Day, 0, 0, 0, 0, DateTimeKind.Unspecified);

        QueryJunction<House> param = new QueryJunction<House>();
        param.Add(x => x.DataProcessamento == processDate);
        param.Add(x => x.AgenteDeCargaId == input.AgenteDeCargaId);
        param.Add(x => x.DataExclusao == null);

        var naturezaCargas = await _naturezaCargaRepository.GetAllNaturezaCarga(userSession.CompanyId);
        var houses = _houseRepository.GetHouseForUploading(param);

        if (houses == null)
            throw new BusinessException("Não foi possivel selecionar Houses para o upload!");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, input.AgenteDeCargaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = await _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        var result = await SubmeterHousesAutomatico(houses, naturezaCargas, certificate.Certificate, token);

        if (result != null && result.Count > 0)
            return new ApiResponse<string>()
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = result
            };

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Enviado com sucesso !",
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<string>> SubmeterHousesAgentesDeCargaAndIds(
        UserSession userSession,
        SubmeterRFBHouseByIdsRequest input)
    {
        var processDate =
            new DateTime(input.DataProcessamento.Year,
            input.DataProcessamento.Month,
            input.DataProcessamento.Day, 0, 0, 0, 0, DateTimeKind.Unspecified);

        QueryJunction<House> param = new();
        param.Add(x => x.DataProcessamento == processDate);
        param.Add(x => x.AgenteDeCargaId == input.FreightFowarderId);
        param.Add(x => input.HouseIds.Contains(x.Id));
        param.Add(x => x.DataExclusao == null);

        var naturezaCargas = await _naturezaCargaRepository.GetAllNaturezaCarga(userSession.CompanyId);
        var houses = _houseRepository.GetHouseForUploading(param);

        if (houses == null)
            throw new BusinessException("Não foi possivel selecionar Houses para o upload!");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, input.FreightFowarderId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = await _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        var result = await SubmeterHousesAutomatico(houses, naturezaCargas, certificate.Certificate, token);

        if (result != null && result.Count > 0)
            return new ApiResponse<string>()
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = result
            };

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Enviado com sucesso !",
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(
        UserSession userSession,
        SubmeterRFBMasterHouseRequest request)
    {
        #region Prepara os houses para associação
        var houseIds = request.Masters.SelectMany(x =>
        {
            return x.HouseIds ?? Array.Empty<int>();
        });
        var masterNumbers = request.Masters.Select(x => x.MasterNumber).ToArray();

        QueryJunction<House> param = new();
        param.Add(x => x.AgenteDeCargaId == request.FreightFowarderId);
        if (houseIds?.Count() > 0)
            param.Add(x => houseIds.Contains(x.Id));
        else
            param.Add(x => masterNumbers.Contains(x.MasterNumeroXML));
        param.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(param) ??
            throw new BusinessException("Não há houses a serem enviados !");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");
        #endregion

        #region Prepara os masters para associação
        QueryJunction<MasterHouseAssociacao> paramAssocicao = new();
        paramAssocicao.Add(x => masterNumbers.Contains(x.MasterNumber));
        paramAssocicao.Add(x => x.DataExclusao == null);

        var associacoes = await _masterHouseAssociacaoRepository
            .SelectMasterHouseAssociacaoParam(paramAssocicao);
        #endregion

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, request.FreightFowarderId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        var token = await
            _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        await SubmeterAssociacaoHouseMasterList(
            userSession,
            associacoes,
            houses,
            request,
            certificate.Certificate,
            token);

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Dados submetidos com sucesso!",
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<string>> SubmitHouseMasterAssociationAsync(
        UserSession userSession,
        SubmitRFBMasterHouseRequest request)
    {
        var masterList =
            _masterHouseAssociacaoRepository.GetMasterNumbersByAssociationIds(userSession.CompanyId, request.FreightFowarderId, request.AssociationIds);

        var associations =
            _masterHouseAssociacaoRepository.GetMasterHouseAssociationByMasterList(userSession.CompanyId, masterList.ToArray())
            ?? Enumerable.Empty<MasterHouseAssociacao>();

        await SubmeterAssociacaoHouseMasterListAsync(masterList, associations, userSession, request.FreightFowarderId);

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Dados submetidos com sucesso!",
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<HouseResponseDto>> SubmeterHouseExclusion(
        UserSession userSession,
        int houseId)
    {
        var house = await _houseRepository.GetHouseByIdForExclusionUpload(userSession.CompanyId, houseId);

        if (house == null)
            throw new BusinessException("House não encontrado!");

        if (house.AgenteDeCargaId == null)
            throw new BusinessException("House não associado ao um agente de carga!");

        if (house.SituacaoDeletionRFBId == 2)
            throw new BusinessException("Exclusão do House no Portal Único já confirmado!");

        if (house.SituacaoRFBId != 2)
            throw new BusinessException("House não está com o status \"Submetido a Receita Federal\"!");

        var naturezaCargas = await _naturezaCargaRepository.GetAllNaturezaCarga(userSession.CompanyId);

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, house.AgenteDeCargaId.Value);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = await _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        if (house.SituacaoDeletionRFBId == 1)
        {
            var res = await _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloDeletionRFB, token);
            await ProcessaRetornoChecagemCancelarHouse(res, house);
        }
        else
        {
            await CancelarHouse(house, naturezaCargas, certificate.Certificate, token);
        }
        var dto = _mapper.Map<HouseResponseDto>(house);
        return new ApiResponse<HouseResponseDto>()
        {
            Sucesso = true,
            Dados = dto,
            Notificacoes = null
        };
    }
    #endregion

    #region Upload House
    private async Task<List<Notificacao>> SubmeterHousesAutomatico(
        IEnumerable<House> houses,
        List<NaturezaCarga> naturezaCargas,
        X509Certificate2 certificado,
        TokenResponse token)
    {
        List<Notificacao> notificacoes = new List<Notificacao>();

        foreach (House house in houses)
        {
            try
            {
                switch (house.SituacaoRFBId)
                {
                    case 1:
                        var res = await _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloRFB, token);

                        var listaErros = await ProcessaRetornoChecagemArquivoHouse(res, house);

                        if (listaErros != null)
                            notificacoes.AddRange(listaErros);
                        break;
                    case 0:
                    case 2:
                    case 3:
                        if (house.SituacaoRFBId == 2 && !house.Reenviar)
                            break;

                        string xml;
                        if (house.SituacaoRFBId == 2)
                            xml = _motorIataHouse.GenHouseManifest(house, naturezaCargas, IataXmlPurposeCode.Update);
                        else
                            xml = _motorIataHouse.GenHouseManifest(house, naturezaCargas, IataXmlPurposeCode.Creation);

                        var response = await _uploadReceitaFederal.SubmitHouse(house.AgenteDeCargaInfo.CNPJ, xml, token, certificado);

                        bool processa = await ProcessarRetornoEnvioArquivoHouse(response, house);
                        if (!processa)
                        {
                            if (response.StatusCode == "Rejected")
                                notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = response.Reason });

                            if (response.StatusCode == "Error")
                                throw new Exception(response.Reason);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                notificacoes.Add(new Notificacao { Codigo = "XML01A", Mensagem = ex.Message });
            }
        }

        return notificacoes;
    }

    #region Metodos Privados
    private async Task<bool> ProcessarRetornoEnvioArquivoHouse(ReceitaRetornoProtocol response, House house)
    {
        switch (response.StatusCode)
        {
            case ("Received"):
                house.SituacaoRFBId = 1;
                house.StatusId = 2;
                house.CodigoErroRFB = null;
                house.DescricaoErroRFB = null;
                house.ProtocoloRFB = response.Reason;
                house.DataProtocoloRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                await _houseRepository.SaveChanges();
                return false;
            case "Rejected":
                house.SituacaoRFBId = 3;
                house.DescricaoErroRFB = response.Reason;
                house.DataProtocoloRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                await _houseRepository.SaveChanges();
                return false;
            case "Processed":
                house.SituacaoRFBId = 2;
                house.CodigoErroRFB = null;
                house.DescricaoErroRFB = null;
                house.DataProtocoloRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                await _houseRepository.SaveChanges();
                return true;
            default:
                return false;
        }
    }
    private async Task<List<Notificacao>> ProcessaRetornoChecagemArquivoHouse(ProtocoloReceitaCheckFile response, House house)
    {
        List<Notificacao> notificacoes = new List<Notificacao>();
        switch (response.status)
        {
            case "Rejected":
                house.SituacaoRFBId = 3;
                if (response.errorList.Length > 0)
                {
                    house.CodigoErroRFB = response.errorList[0].code;
                    house.DescricaoErroRFB = string.Join("\n", response.errorList.Select(x => x.description));
                    house.DataChecagemRFB = response.dateTime;
                    house.Reenviar = false;
                    foreach (ErrorListCheckFileRFB item in response.errorList)
                    {
                        notificacoes.Add(new Notificacao { Codigo = item.code, Mensagem = item.description });
                    }
                }
                _houseRepository.UpdateHouse(house);
                await _houseRepository.SaveChanges();
                return notificacoes;
            case "Processed":
                house.SituacaoRFBId = 2;
                house.DataChecagemRFB = response.dateTime;
                house.Reenviar = false;
                house.DataChecagemDeletionRFB = null;
                house.CodigoErroDeletionRFB = null;
                house.DataProtocoloDeletionRFB = null;
                house.DescricaoErroDeletionRFB = null;
                house.ProtocoloDeletionRFB = null;
                house.SituacaoDeletionRFBId = 0;
                _houseRepository.UpdateHouse(house);
                await _houseRepository.SaveChanges();
                return null;
            case "Received":
                return null;
            default:
                return null;
        }
    }
    #endregion

    #region House Cancelar
    private async Task<ApiResponse<string>> CancelarHouse(
        House house,
        List<NaturezaCarga> naturezaCargas,
        X509Certificate2 certificado,
        TokenResponse token)
    {
        var xml = _motorIataHouse.GenHouseManifest(house, naturezaCargas, IataXmlPurposeCode.Deletion);
        var response = await _uploadReceitaFederal.SubmitHouse(house.AgenteDeCargaInfo.CNPJ, xml, token, certificado);
        await ProcessaRetornoEnvioCancelarHouse(response, house);

        if (response.StatusCode == "Rejected")
            throw new BusinessException(response.Reason);

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Enviado com sucesso !",
            Notificacoes = null
        };
    }

    private async Task ProcessaRetornoChecagemCancelarHouse(
        ProtocoloReceitaCheckFile response,
        House house)
    {
        switch (response.status)
        {
            case "Rejected":
                house.SituacaoDeletionRFBId = 3;
                if (response.errorList.Length > 0)
                {
                    house.CodigoErroDeletionRFB = response.errorList[0].code;
                    house.DescricaoErroDeletionRFB = string.Join("\n", response.errorList.Select(x => x.description));
                    house.DataChecagemDeletionRFB = response.dateTime;
                }
                _houseRepository.UpdateHouse(house);
                break;
            case "Processed":
                house.SituacaoDeletionRFBId = 2;
                house.DataChecagemDeletionRFB = response.dateTime;
                house.DescricaoErroDeletionRFB = null;
                house.SituacaoRFBId = 0;
                house.DataChecagemRFB = null;
                house.CodigoErroRFB = null;
                house.DataProcessadoRFB = null;
                house.ProtocoloRFB = null;
                _houseRepository.UpdateHouse(house);
                break;
            default:
                break;
        }

        await _houseRepository.SaveChanges();
    }

    private async Task ProcessaRetornoEnvioCancelarHouse(
        ReceitaRetornoProtocol response,
        House house)
    {
        switch (response.StatusCode)
        {
            case "Received":
                house.SituacaoDeletionRFBId = 1;
                house.CodigoErroDeletionRFB = null;
                house.DescricaoErroDeletionRFB = null;
                house.ProtocoloDeletionRFB = response.Reason;
                house.DataProtocoloDeletionRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                break;
            case "Rejected":
                house.SituacaoDeletionRFBId = 3;
                house.CodigoErroDeletionRFB = response.StatusCode;
                house.DescricaoErroDeletionRFB = response.Reason;
                house.DataProtocoloDeletionRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                break;
            case "Processed":
                house.SituacaoDeletionRFBId = 2;
                house.DataChecagemDeletionRFB = response.IssueDateTime;
                house.DescricaoErroDeletionRFB = null;
                house.SituacaoRFBId = 0;
                house.DataChecagemRFB = null;
                house.CodigoErroRFB = null;
                house.DataProcessadoRFB = null;
                house.ProtocoloRFB = null;
                _houseRepository.UpdateHouse(house);
                break;
        }

        await _houseRepository.SaveChanges();
    }
    #endregion

    #region Upload Associação House x Master
    private async Task SubmeterAssociacaoHouseMasterList(UserSession userSession,
        List<MasterHouseAssociacao> associacoes,
        List<House> houses,
        SubmeterRFBMasterHouseRequest request,
        X509Certificate2 certificado,
        TokenResponse token)
    {
        string freightFowarderCnpj = houses.FirstOrDefault().AgenteDeCargaInfo.CNPJ;

        var group = houses.GroupBy(house => house.MasterNumeroXML)
                        .Select(group => new
                        {
                            Master = group.Key,
                            TotalWeight = group.Sum(x => x.PesoTotalBruto),
                            TotalPackages = group.Sum(x => x.TotalVolumes),
                            Items = group.ToList()
                        });

        foreach (var item in group)
        {
            var carrieDeclarationDate = request.Masters.FirstOrDefault(x => x.MasterNumber == item.Master)?.CarrierDeclarationDate;

            var masterInfo = GetMasterInfo(item.Items, carrieDeclarationDate);

            var associacao = associacoes.FirstOrDefault(x => x.MasterNumber == item.Master);

            await SubmeterHouseMasterAssociacao(
                userSession,
                freightFowarderCnpj,
                masterInfo,
                item.Items,
                associacao,
                token,
                certificado);
        }
    }

    private static SubmeterRFBMasterHouseItemRequest GetMasterInfo(
        List<House> houses,
        DateTime? carrieDeclarationDate)
    {
        if (houses.Count == 0) return null;

        string destinationLocation = houses[0].AeroportoDestinoCodigo;
        string originLocation = houses[0].AeroportoOrigemCodigo;
        string weightUnit = "KGM";
        string masterNumber = houses[0].MasterNumeroXML;
        int totalPackages = 0;
        int totalPieces = 0;
        double totalWeight = 0;

        for (int i = 0; i < houses.Count; i++)
        {
            totalPackages += houses[i].TotalVolumes;
            totalPieces += houses[i].TotalVolumes;
            if (houses[i].PesoTotalBrutoUN == "KGM")
                totalWeight += houses[i].PesoTotalBruto;
            if (houses[i].PesoTotalBrutoUN == "LBS")
                totalWeight += (houses[i].PesoTotalBruto * 0.453592);
        }

        return new SubmeterRFBMasterHouseItemRequest
        {
            DestinationLocation = destinationLocation,
            OriginLocation = originLocation,
            PackageQuantity = totalPackages,
            TotalPiece = totalPieces,
            TotalWeight = totalWeight,
            TotalWeightUnit = weightUnit,
            HouseIds = houses.Select(x => x.Id).ToArray(),
            MasterNumber = masterNumber,
            CarrierDeclarationDate = carrieDeclarationDate,
        };
    }

    private async Task SubmeterHouseMasterAssociacao(UserSession userSession,
        string FreightFowarderTaxId,
        SubmeterRFBMasterHouseItemRequest masterInfo,
        List<House> houses,
        MasterHouseAssociacao associacao,
        TokenResponse token,
        X509Certificate2 certificado)
    {
        var operation = IataXmlPurposeCode.Creation;

        if (associacao == null)
        {
            associacao = new MasterHouseAssociacao
            {
                ConsigmentItemQuantity = houses.Count,
                CreatedDateTimeUtc = DateTime.UtcNow,
                FinalDestinationLocation = masterInfo.DestinationLocation,
                GrossWeight = masterInfo.TotalWeight,
                GrossWeightUnit = masterInfo.TotalWeightUnit,
                MasterNumber = masterInfo.MasterNumber,
                MessageHeaderDocumentId = masterInfo.MasterNumber,
                OriginLocation = masterInfo.OriginLocation,
                PackageQuantity = masterInfo.PackageQuantity,
                TotalPieceQuantity = masterInfo.TotalPiece,
                CriadoPeloId = userSession.UserId,
                EmpresaId = userSession.CompanyId,
                CarrierDeclarationDate = masterInfo.CarrierDeclarationDate,
            };
        }
        else
        {
            if (associacao.SituacaoAssociacaoRFBId == 1)
            {
                var res = await _uploadReceitaFederal.CheckFileProtocol(associacao.ProtocoloAssociacaoRFB, token);
                await ProcessaRetornoChecagemArquivoHouseMaster(res, associacao, houses);
                return;
            }

            if (associacao.SituacaoAssociacaoRFBId == 2)
                operation = IataXmlPurposeCode.Update;
        }

        string xmlAssociacao = _motorIataHouse
            .GenMasterHouseManifest(masterInfo, houses, operation, associacao.CreatedDateTimeUtc);

        var responseAssociacao = await _uploadReceitaFederal
            .SubmitHouseMaster(FreightFowarderTaxId, xmlAssociacao, token, certificado);

        await ProcessarRetornoEnvioArquivoHouseMaster(responseAssociacao, associacao, houses);

        return;
    }



    private async Task ProcessarRetornoEnvioArquivoHouseMaster(ReceitaRetornoProtocol response,
        MasterHouseAssociacao associacao,
        List<House> houses)
    {
        switch (response.StatusCode)
        {
            case "Received":
                associacao.SituacaoAssociacaoRFBId = 1;
                associacao.CodigoErroAssociacaoRFB = null;
                associacao.DescricaoErroAssociacaoRFB = null;
                associacao.ProtocoloAssociacaoRFB = response.Reason;
                associacao.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                associacao.ReenviarAssociacao = false;
                if (associacao.Id == 0)
                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);
                else
                    _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Rejected":
                associacao.SituacaoAssociacaoRFBId = 3;
                associacao.DescricaoErroAssociacaoRFB = response.Reason;
                associacao.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                if (associacao.Id == 0)
                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);
                else
                    _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Processed":
                associacao.SituacaoAssociacaoRFBId = 2;
                associacao.CodigoErroAssociacaoRFB = null;
                associacao.DescricaoErroAssociacaoRFB = null;
                associacao.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                associacao.ReenviarAssociacao = false;
                if (associacao.Id == 0)
                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);
                else
                    _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
        }

        houses.ForEach(house =>
        {
            switch (response.StatusCode)
            {
                case "Received":
                    house.SituacaoAssociacaoRFBId = 1;
                    house.CodigoErroAssociacaoRFB = null;
                    house.DescricaoErroAssociacaoRFB = null;
                    house.ProtocoloAssociacaoRFB = response.Reason;
                    house.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                    house.ReenviarAssociacao = false;
                    _houseRepository.UpdateHouse(house);
                    break;
                case "Rejected":
                    house.SituacaoAssociacaoRFBId = 3;
                    house.DescricaoErroAssociacaoRFB = response.Reason;
                    house.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                    _houseRepository.UpdateHouse(house);
                    break;
                case "Processed":
                    house.SituacaoAssociacaoRFBId = 2;
                    house.CodigoErroAssociacaoRFB = null;
                    house.DescricaoErroAssociacaoRFB = null;
                    house.DataProtocoloAssociacaoRFB = response.IssueDateTime;
                    house.ReenviarAssociacao = false;
                    _houseRepository.UpdateHouse(house);
                    break;
            }
        });
        await _houseRepository.SaveChanges();
    }

    private async Task ProcessaRetornoChecagemArquivoHouseMaster(ProtocoloReceitaCheckFile response,
        MasterHouseAssociacao associacao,
        List<House> houses)
    {
        switch (response.status)
        {
            case "Rejected":
                associacao.SituacaoAssociacaoRFBId = 3;
                if (response.errorList.Length > 0)
                {
                    associacao.CodigoErroAssociacaoRFB = response.errorList[0].code;
                    associacao.DescricaoErroAssociacaoRFB = string.Join("\n", response.errorList.Select(x => x.description));
                    associacao.DataChecagemAssociacaoRFB = response.dateTime;
                }
                if (associacao.Id == 0)
                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);
                else
                    _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Processed":
                associacao.SituacaoAssociacaoRFBId = 2;
                associacao.DataChecagemAssociacaoRFB = response.dateTime;
                if (associacao.Id == 0)
                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);
                else
                    _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            default:
                break;
        }
        houses.ForEach(house =>
        {
            switch (response.status)
            {
                case "Rejected":
                    house.SituacaoAssociacaoRFBId = 3;
                    if (response.errorList.Length > 0)
                    {
                        house.CodigoErroAssociacaoRFB = response.errorList[0].code;
                        house.DescricaoErroAssociacaoRFB = string.Join("\n", response.errorList.Select(x => x.description));
                        house.DataChecagemAssociacaoRFB = response.dateTime;
                    }
                    _houseRepository.UpdateHouse(house);
                    break;
                case "Processed":
                    house.SituacaoAssociacaoRFBId = 2;
                    house.DataChecagemAssociacaoRFB = response.dateTime;
                    _houseRepository.UpdateHouse(house);
                    break;
                default:
                    break;
            }
        });
        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task ProcessaRetornoChecagemAssociacaoHouseMaster(ProtocoloReceitaCheckFile response,
        MasterHouseAssociacao associacao)
    {
        switch (response.status)
        {
            case "Rejected":
                associacao.SituacaoDeletionAssociacaoRFBId = 3;
                if (response.errorList.Length > 0)
                {
                    associacao.CodigoErroDeletionAssociacaoRFB = response.errorList[0].code;
                    associacao.DescricaoErroDeletionAssociacaoRFB = string.Join("\n", response.errorList.Select(x => x.description));
                    associacao.DataChecagemDeletionAssociacaoRFB = response.dateTime;
                }
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Processed":
                associacao.SituacaoAssociacaoRFBId = 0;
                associacao.ProtocoloAssociacaoRFB = null;
                associacao.DataProtocoloAssociacaoRFB = null;
                associacao.DataChecagemAssociacaoRFB = null;
                associacao.DescricaoErroAssociacaoRFB = null;
                associacao.SituacaoDeletionAssociacaoRFBId = 2;
                associacao.DataChecagemDeletionAssociacaoRFB = response.dateTime;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            default:
                break;
        }
        associacao.MasterHouseAssociationChildren.ToList().ForEach(masterHouseAssociationChild =>
        {
            switch (response.status)
            {
                case "Processed":
                    masterHouseAssociationChild.House.SituacaoAssociacaoRFBId = 0;
                    masterHouseAssociationChild.House.DataChecagemAssociacaoRFB = null;
                    masterHouseAssociationChild.House.DescricaoErroAssociacaoRFB = null;
                    masterHouseAssociationChild.House.DataProtocoloAssociacaoRFB = null;
                    masterHouseAssociationChild.House.ProtocoloAssociacaoRFB = null;
                    masterHouseAssociationChild.House.ReenviarAssociacao = false;
                    _houseRepository.UpdateHouse(masterHouseAssociationChild.House);
                    break;
                default:
                    break;
            }
        });
        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }


    #endregion

    #endregion

    #region Upload Associação Novo
    private async Task SubmeterAssociacaoHouseMasterListAsync(
        IEnumerable<string> masterList,
        IEnumerable<MasterHouseAssociacao> associacoes,
        UserSession userSession,
        int freightFowarderId)
    {
        if (associacoes.Count() == 0)
            throw new BusinessException("Nenhuma associação disponível!");

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, freightFowarderId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        var token = await
            _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        foreach (var master in masterList)
        {
            var masterAssociations = associacoes.Where(x => x.MasterNumber == master);

            string freightFowarderCnpj = masterAssociations.FirstOrDefault().AgenteDeCargaInfo.CNPJ;

            await SubmitHouseMasterAssociationAsync(
                freightFowarderCnpj,
                masterAssociations,
                token,
                certificate.Certificate);
        }
    }

    private async Task SubmitHouseMasterAssociationAsync(
        string FreightFowarderTaxId,
        IEnumerable<MasterHouseAssociacao> associationList,
        TokenResponse token,
        X509Certificate2 certificado)
    {
        var operation = IataXmlPurposeCode.Creation;

        if (CheckPendingMasterHouseAssociationUpload(associationList))
        {
            var checkAssociations = associationList.Where(x => x.SituacaoAssociacaoRFBId == 1)
                .GroupBy(x => x.ProtocoloAssociacaoRFB);

            foreach (var checkAssociation in checkAssociations)
            {
                var res = await _uploadReceitaFederal.CheckFileProtocol(checkAssociation.Key, token);
                await CheckHouseMasterAssociationAsync(res, checkAssociation.Key, associationList);
            }

            return;
        }

        CheckDiferentDocumentMasterHouseAssociation(associationList);

        if (associationList.Any(x => x.SituacaoAssociacaoRFBId == 2))
            operation = IataXmlPurposeCode.Update;

        string xmlAssociacao = _motorIataHouse.GenMasterHouseManifest(associationList, operation);

        var responseAssociacao = await _uploadReceitaFederal
            .SubmitHouseMaster(FreightFowarderTaxId, xmlAssociacao, token, certificado);

        await ProcessMasterHouseRFBAsync(responseAssociacao, associationList);

        return;
    }

    private async Task ProcessMasterHouseRFBAsync(
        ReceitaRetornoProtocol response,
        IEnumerable<MasterHouseAssociacao> association)
    {
        switch (response.StatusCode)
        {
            case "Received":
                await ReceivedMasterHouseAssociationUpload(association, response);
                break;
            case "Rejected":
                await RejectMasterHouseAssociationUpload(association, response);
                break;
            case "Processed":
                await ProcessMasterHouseAssociationUpload(association, response);
                break;
        }
    }

    private async Task CheckHouseMasterAssociationAsync(
        ProtocoloReceitaCheckFile response,
        string protocol,
        IEnumerable<MasterHouseAssociacao> association)
    {
        switch (response.status)
        {
            case "Rejected":
                await RejectMasterHouseAssociationUploadCheck(association, protocol, response);
                break;
            case "Processed":
                await AcceptMasterHouseAssociationUploadCheck(association, protocol, response);
                break;
            default:
                break;
        }
    }

    private bool CheckPendingMasterHouseAssociationUpload(IEnumerable<MasterHouseAssociacao> associationList)
    {
        var pendingUpload = associationList.Where(x => x.SituacaoAssociacaoRFBId == 1)
            ?? Enumerable.Empty<MasterHouseAssociacao>();

        return pendingUpload.Count() > 0;
    }

    private void CheckDiferentDocumentMasterHouseAssociation(IEnumerable<MasterHouseAssociacao> associationList)
    {
        var documents = associationList.GroupBy(x => x.MessageHeaderDocumentId);

        if (documents.Count() > 1)
            throw new BusinessException($"Existe associações com números de documentos diferentes: {string.Join(",", documents.Select(x => x.Key))}");
    }

    #region Processamento Associação Receita Federal
    private async Task ReceivedMasterHouseAssociationUpload(IEnumerable<MasterHouseAssociacao> associationList, ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoAssociacaoRFBId = 1;
            association.CodigoErroAssociacaoRFB = null;
            association.DescricaoErroAssociacaoRFB = null;
            association.ProtocoloAssociacaoRFB = response.Reason;
            association.DataProtocoloAssociacaoRFB = response.IssueDateTime;
            association.ReenviarAssociacao = false;
            association.CodigoErroDeletionAssociacaoRFB = null;
            association.DescricaoErroDeletionAssociacaoRFB = null;
            association.DataProtocoloDeletionAssociacaoRFB = null;
            association.DataChecagemDeletionAssociacaoRFB = null;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task RejectMasterHouseAssociationUpload(IEnumerable<MasterHouseAssociacao> associationList, ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoAssociacaoRFBId = 3;
            association.DescricaoErroAssociacaoRFB = response.Reason;
            association.DataProtocoloAssociacaoRFB = response.IssueDateTime;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task ProcessMasterHouseAssociationUpload(IEnumerable<MasterHouseAssociacao> associationList, ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoAssociacaoRFBId = 2;
            association.CodigoErroAssociacaoRFB = null;
            association.DescricaoErroAssociacaoRFB = null;
            association.DataProtocoloAssociacaoRFB = response.IssueDateTime;
            association.ReenviarAssociacao = false;
            association.CodigoErroDeletionAssociacaoRFB = null;
            association.DescricaoErroDeletionAssociacaoRFB = null;
            association.DataProtocoloDeletionAssociacaoRFB = null;
            association.DataChecagemDeletionAssociacaoRFB = null;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }
    #endregion


    #region Associação Processamento Check Upload
    private async Task RejectMasterHouseAssociationUploadCheck(IEnumerable<MasterHouseAssociacao> associationList, string protocol, ProtocoloReceitaCheckFile response)
    {
        var associations = associationList.Where(x => x.ProtocoloAssociacaoRFB == protocol).ToList();

        foreach (var association in associations)
        {
            association.SituacaoAssociacaoRFBId = 3;
            if (response.errorList.Length > 0)
            {
                association.CodigoErroAssociacaoRFB = response.errorList[0].code;
                association.DescricaoErroAssociacaoRFB = string.Join("\n", response.errorList.Select(x => x.description));
                association.DataChecagemAssociacaoRFB = response.dateTime;
            }
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task AcceptMasterHouseAssociationUploadCheck(IEnumerable<MasterHouseAssociacao> associationList, string protocol, ProtocoloReceitaCheckFile response)
    {
        var associations = associationList.Where(x => x.ProtocoloAssociacaoRFB == protocol).ToList();

        foreach (var association in associations)
        {
            association.SituacaoAssociacaoRFBId = 2;
            association.CodigoErroAssociacaoRFB = null;
            association.DescricaoErroAssociacaoRFB = null;
            association.DataChecagemAssociacaoRFB = response.dateTime;
            association.SituacaoDeletionAssociacaoRFBId = 0;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }
    #endregion

    #endregion
}