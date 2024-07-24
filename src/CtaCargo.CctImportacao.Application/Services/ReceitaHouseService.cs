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
    private readonly IMasterHouseAssociacaoRepository _masterHouseAssociacaoRepository;
    private readonly INaturezaCargaRepository _naturezaCargaRepository;
    private readonly IAutenticaReceitaFederal _autenticaReceitaFederal;
    private readonly IUploadReceitaFederal _uploadReceitaFederal;
    private readonly IMotorIataHouse _motorIataHouse;
    private readonly IMapper _mapper;

    #region Construtor
    public ReceitaHouseService(ICertitificadoDigitalSupport certificadoDigitalSupport,
        IAutenticaReceitaFederal autenticaReceitaFederal,
        IHouseRepository houseRepository,
        IMasterHouseAssociacaoRepository masterHouseAssociacaoRepository,
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
        SubmeterRFBMasterHouseRequest input)
    {
        #region Prepara os houses para associação

        var houseIds = input.Masters.SelectMany(x => x.HouseIds).ToArray();

        QueryJunction<House> param = new();
        param.Add(x => x.AgenteDeCargaId == input.FreightFowarderId);
        param.Add(x => houseIds.Contains(x.Id));
        param.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(param) ??
            throw new BusinessException("Não há houses a serem enviados !");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");

        #endregion

        #region Prepara os masters para associação

        var masterNumbers = input.Masters.Select(x => x.MasterNumber).ToArray();
        QueryJunction<MasterHouseAssociacao> paramAssocicao = new();
        paramAssocicao.Add(x => masterNumbers.Contains(x.MasterNumber));
        paramAssocicao.Add(x => x.DataExclusao == null);

        var associacao = await _masterHouseAssociacaoRepository
            .SelectMasterHouseAssociacaoParam(paramAssocicao);

        #endregion

        var certificate = await 
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, input.FreightFowarderId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        var token = await 
            _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        await SubmeterAssociacaoHouseMasterList(
            userSession, 
            input.Masters, 
            houses, 
            certificate.Certificate, 
            token);

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Dados submetidos com sucesso!",
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<string>> SubmeterAssociation(
        UserSession userSession,
        int associationId)
    {
        var association = await _masterHouseAssociacaoRepository.SelectMasterHouseAssociacaoById(userSession.CompanyId, associationId);

        if (association == null)
            throw new BusinessException("Associação não encontrada !");

        QueryJunction<House> param = new QueryJunction<House>();
        param.Add(x => x.MasterNumeroXML == association.MasterNumber);
        param.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(param);

        if (houses == null || houses.Count() == 0)
            throw new BusinessException("Não há houses associados a este Master !");

        var agenteId = houses.FirstOrDefault().AgenteDeCargaId;

        var masterInfo = new SubmeterRFBMasterHouseItemRequest
        {
            DestinationLocation = association.FinalDestinationLocation,
            MasterNumber = association.MasterNumber,
            OriginLocation = association.OriginLocation,
            PackageQuantity = association.PackageQuantity,
            TotalPiece = association.TotalPieceQuantity,
            TotalWeight = association.GrossWeight,
            TotalWeightUnit = association.GrossWeightUnit
        };

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, agenteId.Value);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = await _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        if (association.SituacaoDeletionAssociacaoRFBId == 1)
        {
            var res = await _uploadReceitaFederal.CheckFileProtocol(association.ProtocoloDeletionAssociacaoRFB, token);
            await ProcessaRetornoChecagemAssociacaoHouseMaster(res, association, houses);
        }
        else
        {
            await CancelarAssociacaoHouseMasterList(association, userSession, masterInfo, houses, certificate.Certificate, token);
        }
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
                notificacoes.Add(new Notificacao { Codigo = "XML01A", Mensagem= ex.Message });
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
        List<SubmeterRFBMasterHouseItemRequest> Masters,
        List<House> houses,
        X509Certificate2 certificado,
        TokenResponse token)
    {
        string curMaster = houses[0].MasterNumeroXML;
        var masterInfo = Masters.FirstOrDefault(x => x.MasterNumber == curMaster);
        string freightFowarderCnpj = houses.FirstOrDefault().AgenteDeCargaInfo.CNPJ;

        List<House> houseList = new();
        foreach (House house in houses.OrderBy(x => x.MasterNumeroXML))
        {
            if (house.MasterNumeroXML == curMaster)
            {
                houseList.Add(house);
                continue;
            }

            if (house.MasterNumeroXML != curMaster)
            {
                await SubmeterHouseMasterAssociacao(userSession, freightFowarderCnpj, masterInfo, houseList, token, certificado);
                houseList = new List<House>();
                houseList.Add(house);
                curMaster = house.MasterNumeroXML;
                masterInfo = Masters.FirstOrDefault(x => x.MasterNumber == curMaster);
            }
        }
        await SubmeterHouseMasterAssociacao(userSession, freightFowarderCnpj, masterInfo, houseList, token, certificado);
    }

    private async Task CancelarAssociacaoHouseMasterList(MasterHouseAssociacao associacao,
        UserSession userSession,
        SubmeterRFBMasterHouseItemRequest masterInfo,
        List<House> houses,
        X509Certificate2 certificado,
        TokenResponse token)
    {

        string freightFowarderCnpj = houses.FirstOrDefault().AgenteDeCargaInfo.CNPJ;

        List<House> houseList = new List<House>();
        foreach (var house in houses)
        {
            houseList.Add(house);
        }
        await CancelarHouseMasterAssociacao(associacao, userSession, freightFowarderCnpj, masterInfo, houseList, token, certificado);
    }

    private async Task SubmeterHouseMasterAssociacao(UserSession userSession,
        string FreightFowarderTaxId,
        SubmeterRFBMasterHouseItemRequest masterInfo,
        List<House> houses,
        TokenResponse token,
        X509Certificate2 certificado)
    {
        var associacao = await _masterHouseAssociacaoRepository
            .SelectMasterHouseAssociacaoByMaster(masterInfo.MasterNumber);

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
                EmpresaId = userSession.CompanyId
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

            if (!CheckUploadAvailability(houses))
                return;

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

    private async Task CancelarHouseMasterAssociacao(MasterHouseAssociacao associacao, UserSession userSession,
        string FreightFowarderTaxId,
        SubmeterRFBMasterHouseItemRequest masterInfo,
        List<House> houses,
        TokenResponse token,
        X509Certificate2 certificado)
    {

        var operation = IataXmlPurposeCode.Deletion;

        string xmlAssociacao = _motorIataHouse
            .GenMasterHouseManifest(masterInfo, houses, operation, associacao.CreatedDateTimeUtc);

        var responseAssociacao = await _uploadReceitaFederal
            .SubmitHouseMaster(FreightFowarderTaxId, xmlAssociacao, token, certificado);

        await ProcessarRetornoExclusaoAssociacao(responseAssociacao, associacao, houses);

        return;
    }

    private bool CheckUploadAvailability(List<House> houses)
    {
        return houses
            .Where(x => x.SituacaoAssociacaoRFBId != 2 || (x.SituacaoAssociacaoRFBId == 2 && x.ReenviarAssociacao))
            .Count() > 0;
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
        await _masterHouseAssociacaoRepository.SaveChanges();
    }

    private async Task ProcessaRetornoChecagemAssociacaoHouseMaster(ProtocoloReceitaCheckFile response,
        MasterHouseAssociacao associacao,
        List<House> houses)
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
                associacao.SituacaoDeletionAssociacaoRFBId = 4;
                associacao.DataChecagemDeletionAssociacaoRFB = response.dateTime;
                associacao.DataExclusao = DateTime.UtcNow;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            default:
                break;
        }
        houses.ForEach(house =>
        {
            switch (response.status)
            {
                case "Processed":
                    house.SituacaoAssociacaoRFBId = 0;
                    house.DataChecagemAssociacaoRFB = null;
                    house.DescricaoErroAssociacaoRFB = null;
                    house.DataProtocoloAssociacaoRFB = null;
                    house.ProtocoloAssociacaoRFB = null;
                    house.ReenviarAssociacao = false;
                    _houseRepository.UpdateHouse(house);
                    break;
                default:
                    break;
            }
        });
        await _masterHouseAssociacaoRepository.SaveChanges();
    }

    private async Task ProcessarRetornoExclusaoAssociacao(ReceitaRetornoProtocol response,
        MasterHouseAssociacao associacao,
        List<House> houses)
    {
        switch (response.StatusCode)
        {
            case "Received":
                associacao.SituacaoDeletionAssociacaoRFBId = 1;
                associacao.CodigoErroDeletionAssociacaoRFB = null;
                associacao.DescricaoErroDeletionAssociacaoRFB = null;
                associacao.ProtocoloDeletionAssociacaoRFB = response.Reason;
                associacao.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Rejected":
                associacao.SituacaoDeletionAssociacaoRFBId = 3;
                associacao.CodigoErroDeletionAssociacaoRFB = response.StatusCode;
                associacao.DescricaoErroDeletionAssociacaoRFB = response.Reason;
                associacao.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
            case "Processed":
                associacao.SituacaoDeletionAssociacaoRFBId = 2;
                associacao.CodigoErroDeletionAssociacaoRFB = null;
                associacao.DescricaoErroDeletionAssociacaoRFB = null;
                associacao.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);
                break;
        }

        if (response.StatusCode == "Processed")
        {
            houses.ForEach(house =>
            {
                house.SituacaoAssociacaoRFBId = 0;
                house.CodigoErroAssociacaoRFB = null;
                house.DescricaoErroAssociacaoRFB = null;
                house.DataProtocoloAssociacaoRFB = null;
                house.ReenviarAssociacao = false;
                _houseRepository.UpdateHouse(house);
            });
        }
        await _houseRepository.SaveChanges();
    }
    #endregion

    #endregion
}