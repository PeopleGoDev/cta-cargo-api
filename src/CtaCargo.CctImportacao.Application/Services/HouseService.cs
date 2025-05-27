using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Model;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Domain.Validator;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class HouseService : IHouseService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;
    private readonly IHouseRepository _houseRepository;
    private readonly IAgenteDeCargaRepository _agenteDeCargaRepository;
    private readonly IPortoIATARepository _portoIATARepository;
    private readonly IMapper _mapper;
    private readonly IAssociacaoRepository _masterHouseAssociacaoRepository;

    public HouseService(IMapper mapper,
        IHouseRepository houseRepository,
        IPortoIATARepository portoIATARepository,
        IAgenteDeCargaRepository agenteDeCargaRepository,
        IAssociacaoRepository masterHouseAssociacaoRepository)
    {
        _houseRepository = houseRepository;
        _portoIATARepository = portoIATARepository;
        _mapper = mapper;
        _agenteDeCargaRepository = agenteDeCargaRepository;
        _masterHouseAssociacaoRepository = masterHouseAssociacaoRepository;
    }

    #region House
    public async Task<ApiResponse<HouseResponseDto>> HousePorId(UserSession userSession, int houseId)
    {
        var lista = await _houseRepository.GetHouseById(userSession.CompanyId, houseId);

        if (lista == null)
            throw new BusinessException("House não encontrado !");

        var dto = _mapper.Map<HouseResponseDto>(lista);

        return new ApiResponse<HouseResponseDto>
        {
            Dados = dto,
            Sucesso = true,
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(UserSession userSession, HouseListarRequest input)
    {
        var param = GeneratePredicateParam(userSession, input);

        var lista = await _houseRepository.GetAllHouses(param.ToPredicate()); ;
        var dto = _mapper.Map<IEnumerable<HouseResponseDto>>(lista);
        return
                new ApiResponse<IEnumerable<HouseResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHousesPorDataCriacao(UserSession userSession, MasterHousePorDataCriacaoRequest input)
    {
        var lista = await _houseRepository.GetAllHousesByDataCriacao(userSession.CompanyId, input.DataCriacao);
        var dto = _mapper.Map<IEnumerable<HouseResponseDto>>(lista);
        return
                new ApiResponse<IEnumerable<HouseResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUploadAsync(UserSession userSession, HouseListarRequest input)
    {
        var param = GeneratePredicateParam(userSession, input);
        var masters = _houseRepository.GetMastersByParam(param);

        if (masters is null) return default;
        if (masters.Length == 0) return default;

        var houses = _houseRepository
            .GetHouseByMasterList(masters);

        QueryJunction<MasterHouseAssociacao> paramAssociacao = new();
        paramAssociacao.Add(x => x.EmpresaId == userSession.CompanyId);
        paramAssociacao.Add(x => x.DataExclusao == null);
        paramAssociacao.Add(x => masters.Contains(x.MasterNumber));

        var association = await _masterHouseAssociacaoRepository
            .SelectMasterHouseAssociacaoParam(paramAssociacao);

        var response = new List<MasterHouseAssociationUploadResponse>();
        foreach (var master in masters)
        {
            var findSummary = association.Find(x => x.MasterNumber == master);
            var findHouses = houses.Where(x => x.MasterNumeroXML == master);

            var resp = new MasterHouseAssociationUploadResponse
            {
                Number = master,
                DocumentId = findSummary?.MessageHeaderDocumentId,
                Houses = (from house in findHouses
                          select new MasterHouseAssociationHouseItemResponse
                          {
                              Id = house.Id,
                              Number = house.Numero,
                              AssociationCheckDate = house.DataChecagemAssociacaoRFB,
                              AssociationDate = house.DataProtocoloAssociacaoRFB,
                              AssociationErrorCode = house.CodigoErroAssociacaoRFB,
                              AssociationErrorDescription = house.DescricaoErroAssociacaoRFB,
                              AssociationProtocol = house.ProtocoloAssociacaoRFB,
                              AssociationStatusId = house.SituacaoAssociacaoRFBId,
                              DestinationLocation = house.AeroportoDestinoCodigo,
                              OriginLocation = house.AeroportoOrigemCodigo,
                              PackageQuantity = house.TotalVolumes,
                              ResendAssociation = house.ReenviarAssociacao,
                              TotalPieceQuantity = house.TotalVolumes,
                              TotalWeight = house.PesoTotalBruto,
                              TotalWeightUnit = house.PesoTotalBrutoUN,
                              ProcessDate = house.DataProcessamento,
                              RFBStatus = house.SituacaoRFBId,
                              Resend = house.Reenviar
                          }).ToList()
            };

            if (findSummary != null)
            {
                resp.Summary = new MasterHouseAssociationSummaryUploadResponse
                {
                    Id = findSummary.Id,
                    ConsignmentItemQuantity = findSummary.ConsigmentItemQuantity,
                    DestinationLocation = findSummary.FinalDestinationLocation,
                    IssueDate = findSummary.CarrierDeclarationDate,
                    OriginLocation = findSummary.OriginLocation,
                    PackageQuantity = findSummary.PackageQuantity,
                    TotalPieceQuantity = findSummary.TotalPieceQuantity,
                    TotalWeight = findSummary.GrossWeight,
                    TotalWeightUnit = findSummary.GrossWeightUnit,
                    RFBCreationStatus = findSummary.SituacaoAssociacaoRFBId,
                    RFBCreationProtocol = findSummary.ProtocoloAssociacaoRFB,
                    RFBCancelationStatus = findSummary.SituacaoDeletionAssociacaoRFBId,
                    RFBCancelationProtocol = findSummary.ProtocoloDeletionAssociacaoRFB
                };
            }

            response.Add(resp);
        }

        return response;
    }

    /// <summary>
    /// Selecionar Associações e houses disponiveis para Upload
    /// </summary>
    /// <param name="userSession"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<MasterHouseAssoationForUploadResponse> SelectHouseAssociationAsync(UserSession userSession, HouseListarRequest input)
    {
        var response = new MasterHouseAssoationForUploadResponse();
        response.MasterAssociationItems = new();
        response.OpenMasters = new();

        QueryJunction<MasterHouseAssociacao> paramAssociacao = new();
        paramAssociacao.Add(x => x.EmpresaId == userSession.CompanyId);
        paramAssociacao.Add(x => x.DataExclusao == null);
        paramAssociacao.Add(x => x.AgenteDeCargaId == input.AgenteDeCargaId);
        paramAssociacao.Add(x => x.ProcessDate == input.DataProcessamento.Value.Date);

        var associations = await _masterHouseAssociacaoRepository.SelectMasterHouseAssociacaoParam(paramAssociacao);

        var param = GeneratePredicateParam(userSession, input);

        //param.Add(x => x.MasterHouseAssociacaoId == null);

        var houses = _houseRepository.GetHouseForUploading(param);

        foreach (var association in associations)
        {
            response.MasterAssociationItems.Add(new MasterHouseAssociationUploadResponse
            {
                Number = association.MasterNumber,
                DocumentId = association.MessageHeaderDocumentId,
                CreateRFBErrorCode = association.CodigoErroAssociacaoRFB,
                CreateRFBErrorDescription = association.DescricaoErroAssociacaoRFB,
                CreateRFBProtocol = association.ProtocoloAssociacaoRFB,
                CreateRFBSubmitDateTime = association.DataProtocoloAssociacaoRFB,
                CreateRFPStatus = (RFStatusEnvioType)association.SituacaoAssociacaoRFBId,
                ProcessDate = association.ProcessDate,
                CancelationRFBErrorCode = association.CodigoErroDeletionAssociacaoRFB,
                CancelationRFBErrorDescription = association.DescricaoErroDeletionAssociacaoRFB,
                CancelationRFBProtocol = association.ProtocoloDeletionAssociacaoRFB,
                CancelationRFBSubmitDateTime = association.DataProtocoloDeletionAssociacaoRFB,
                CancelationRFPStatus = (RFStatusEnvioType)association.SituacaoDeletionAssociacaoRFBId,
                Houses = (from masterHouseAssociationChild in association.MasterHouseAssociationChildren
                          select new MasterHouseAssociationHouseItemResponse
                          {
                              Id = masterHouseAssociationChild.House.Id,
                              Number = masterHouseAssociationChild.House.Numero,
                              AssociationCheckDate = masterHouseAssociationChild.House.DataChecagemAssociacaoRFB,
                              AssociationDate = masterHouseAssociationChild.House.DataProtocoloAssociacaoRFB,
                              AssociationErrorCode = masterHouseAssociationChild.House.CodigoErroAssociacaoRFB,
                              AssociationErrorDescription = masterHouseAssociationChild.House.DescricaoErroAssociacaoRFB,
                              AssociationProtocol = masterHouseAssociationChild.House.ProtocoloAssociacaoRFB,
                              AssociationStatusId = masterHouseAssociationChild.House.SituacaoAssociacaoRFBId,
                              DestinationLocation = masterHouseAssociationChild.House.AeroportoDestinoCodigo,
                              OriginLocation = masterHouseAssociationChild.House.AeroportoOrigemCodigo,
                              PackageQuantity = masterHouseAssociationChild.House.TotalVolumes,
                              ResendAssociation = masterHouseAssociationChild.House.ReenviarAssociacao,
                              TotalPieceQuantity = masterHouseAssociationChild.House.TotalVolumes,
                              TotalWeight = masterHouseAssociationChild.House.PesoTotalBruto,
                              TotalWeightUnit = masterHouseAssociationChild.House.PesoTotalBrutoUN,
                              ProcessDate = masterHouseAssociationChild.House.DataProcessamento,
                              RFBStatus = masterHouseAssociationChild.House.SituacaoRFBId,
                              Resend = masterHouseAssociationChild.House.Reenviar
                          }).ToList(),
                Summary = new MasterHouseAssociationSummaryUploadResponse
                {
                    Id = association.Id,
                    ConsignmentItemQuantity = association.ConsigmentItemQuantity,
                    DestinationLocation = association.FinalDestinationLocation,
                    IssueDate = association.CarrierDeclarationDate,
                    OriginLocation = association.OriginLocation,
                    PackageQuantity = association.PackageQuantity,
                    TotalPieceQuantity = association.TotalPieceQuantity,
                    TotalWeight = association.GrossWeight,
                    TotalWeightUnit = association.GrossWeightUnit,
                    RFBCreationStatus = association.SituacaoAssociacaoRFBId,
                    RFBCreationProtocol = association.ProtocoloAssociacaoRFB,
                    RFBCancelationStatus = association.SituacaoDeletionAssociacaoRFBId,
                    RFBCancelationProtocol = association.ProtocoloDeletionAssociacaoRFB
                }
            });
        }

        var houseAssociation = associations.SelectMany(x => x.MasterHouseAssociationChildren);

        var selectHouses = houses.Where(x => !houseAssociation.Any(y => y.House.Numero == x.Numero));

        var masterGroups = selectHouses.GroupBy(x => x.MasterNumeroXML)
            .Select(s => new MasterHouseAssociationOpenMasterItem
            {
                MasterNumber = s.Key,
                Houses = s.Select(house => new MasterHouseAssociationOpenHouseItem
                {
                    Id = house.Id,
                    DestinationLocation = house.AeroportoDestinoCodigo,
                    MasterNumber = house.MasterNumeroXML,
                    Number = house.Numero,
                    OriginLocation = house.AeroportoOrigemCodigo,
                    PackageQuantity = house.TotalVolumes,
                    ProcessDate = house.DataProcessamento,
                    TotalPieceQuantity = house.TotalVolumes,
                    TotalWeight = house.PesoTotalBruto,
                    TotalWeightUnit = house.PesoTotalBrutoUN
                }).ToList()
            }).ToList();

        response.OpenMasters = masterGroups;

        return response;
    }

    public async Task<ApiResponse<HouseResponseDto>> InserirHouse(UserSession userSession, HouseInsertRequestDto houseRequest, string inputMode = "Manual")
    {
        houseRequest.Numero = houseRequest.Numero.Trim();
        var limiteDate = DateTime.Now.AddYears(-1);

        var houseId = await _houseRepository.GetHouseIdByNumberValidate(userSession.CompanyId, houseRequest.Numero, limiteDate);

        if (houseId > 0)
            throw new BusinessException($"Já existe um House número {houseRequest.Numero} dentro dos últimos 365 dias!");

        houseRequest.DataProcessamento = new DateTime(
                houseRequest.DataProcessamento.Year,
                houseRequest.DataProcessamento.Month,
                houseRequest.DataProcessamento.Day,
                0, 0, 0, DateTimeKind.Unspecified);

        var house = _mapper.Map<House>(houseRequest);

        house.CreatedDateTimeUtc = DateTime.UtcNow;

        HouseEntityValidator validator = new HouseEntityValidator();

        var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaByIataCode(userSession.CompanyId, houseRequest.AgenteDeCargaNumero);
        if (agenteDeCarga == null)
            throw new BusinessException("Agente de carga não cadastrado!");

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(houseRequest.AeroportoOrigem);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(houseRequest.AeroportoDestino);

        house.AeroportoOrigemId = null;
        house.AeroportoDestinoId = null;

        if (codigoOrigemId > 0)
            house.AeroportoOrigemId = codigoOrigemId;

        if (codigoDestinoId > 0)
            house.AeroportoDestinoId = codigoDestinoId;

        house.AgenteDeCargaId = agenteDeCarga.Id;
        house.CriadoPeloId = userSession.UserId;
        house.EmpresaId = userSession.CompanyId;
        house.Environment = userSession.Environment;
        house.InputMode = inputMode;

        var result = validator.Validate(house);

        house.StatusId = result.IsValid ? 1 : 0;

        _houseRepository.CreateHouse(house);

        if (await _houseRepository.SaveChanges())
        {
            var HouseResponseDto = _mapper.Map<HouseResponseDto>(house);
            return
                new ApiResponse<HouseResponseDto>
                {
                    Dados = HouseResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }

        throw new BusinessException("Não Foi possível adicionar o House: Erro Desconhecido!");
    }

    public async Task<ApiResponse<HouseResponseDto>> AtualizarHouse(UserSession userSession, HouseUpdateRequestDto input)
    {

        var house = await _houseRepository.GetHouseById(userSession.CompanyId, input.HouseId);

        if (house == null)
            throw new BusinessException("Não foi possível atualizar o House: House não encontrado !");

        _mapper.Map(input, house);

        var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaByIataCode(house.EmpresaId, input.AgenteDeCargaNumero);
        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigem);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestino);

        house.AeroportoOrigemId = null;
        house.AeroportoDestinoId = null;
        house.AgenteDeCargaId = null;

        if (codigoOrigemId > 0)
            house.AeroportoOrigemId = codigoOrigemId;

        if (codigoDestinoId > 0)
            house.AeroportoDestinoId = codigoDestinoId;

        if (agenteDeCarga == null)
            throw new Exception("Agente de carga não cadastrado!");

        house.AgenteDeCargaId = agenteDeCarga.Id;
        house.ModifiedDateTimeUtc = DateTime.UtcNow;
        house.ModificadoPeloId = userSession.UserId;

        HouseEntityValidator validator = new HouseEntityValidator();

        var result = validator.Validate(house);
        if (result.IsValid)
            house.StatusId = 1;
        else
            house.StatusId = 0;
        _houseRepository.UpdateHouse(house);

        if (await _houseRepository.SaveChanges())
        {
            var HouseResponseDto = _mapper.Map<HouseResponseDto>(house);
            return
                new ApiResponse<HouseResponseDto>
                {
                    Dados = HouseResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        };

        throw new BusinessException("Não foi possível atualiza o House: Erro Desconhecido!");
    }

    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarHouse(UserSession userSession, int houseId)
    {
        var house = await _houseRepository.GetHouseById(userSession.CompanyId, houseId);

        if (house == null)
            throw new BusinessException("House não encontrado !");

        house.Reenviar = true;

        HouseEntityValidator validator = new HouseEntityValidator();

        var result = validator.Validate(house);

        house.StatusId = result.IsValid ? 1 : 0;

        _houseRepository.UpdateHouse(house);

        if (await _houseRepository.SaveChanges())
        {
            var HouseResponseDto = _mapper.Map<HouseResponseDto>(house);
            return
                new ApiResponse<HouseResponseDto>
                {
                    Dados = HouseResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        };

        throw new BusinessException("Não foi possível atualiza o House: Erro Desconhecido!");

    }

    public async Task<ApiResponse<HouseResponseDto>> ExcluirHouse(UserSession userSession, int houseId)
    {
        try
        {
            var houseRepo = await _houseRepository.GetHouseById(userSession.CompanyId, houseId);
            if (houseRepo == null)
            {
                return
                    new ApiResponse<HouseResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Não foi possível excluir House: House não encontrado !"
                            }
                        }
                    };
            }

            _houseRepository.DeleteHouse(houseRepo);

            if (await _houseRepository.SaveChanges())
            {
                return
                    new ApiResponse<HouseResponseDto>
                    {
                        Dados = null,
                        Sucesso = true,
                        Notificacoes = null
                    };
            }
            else
            {
                return
                    new ApiResponse<HouseResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Não foi possível excluir House: Erro Desconhecido!"
                            }
                        }
                    };
            }

        }
        catch (DbUpdateException e)
        {
            return ErrorHandling(e);
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<HouseResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Não foi possível excluir House: {ex.Message} !"
                            }
                        }
                    };
        }

    }
    #endregion

    #region Associação Master House
    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarAssociacaoHouse(UserSession userSession, int houseId)
    {
        var house = await _houseRepository.GetHouseById(userSession.CompanyId, houseId);

        if (house == null)
            throw new BusinessException("House não encontrado !");

        house.ReenviarAssociacao = true;

        _houseRepository.UpdateHouse(house);

        if (await _houseRepository.SaveChanges())
        {
            var HouseResponseDto = _mapper.Map<HouseResponseDto>(house);
            return
                new ApiResponse<HouseResponseDto>
                {
                    Dados = HouseResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
            throw new BusinessException("Não foi possível atualiza o House: Erro Desconhecido!");
    }
    
    public async Task<List<MasterHouseAssociationResponse>> IncluirAssociacaoMasterHouse(UserSession userSession, AddMasterHouseAssociationRequest request)
    {
        var houseIds = request.Masters.SelectMany(x => x.HouseIds);

        QueryJunction<House> param = new();
        param.Add(x => x.EmpresaId == userSession.CompanyId);
        param.Add(x => x.AgenteDeCargaId == request.FreightFowarderId);
        param.Add(x => houseIds.Contains(x.Id));
        param.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(param) ??
            throw new BusinessException("Não há houses a serem enviados !");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");

        return await PrepareAndSaveMasterHouseAssociation(
            userSession,
            houses,
            request);
    }

    public async Task<List<MasterHouseAssociationResponse>> AtualizarAssociacaoMasterHouse(UserSession userSession, UpdateMasterHouseAssociationRequest request)
    {
        var houseIds = request.Associations.SelectMany(x => x.HouseIds);
        var associationsId = request.Associations.Select(x => x.MessageHeaderDocumentoId).ToArray();

        QueryJunction<MasterHouseAssociacao> param = new();
        param.Add(x => x.EmpresaId == userSession.CompanyId);
        param.Add(x => associationsId.Contains(x.MessageHeaderDocumentId));
        param.Add(x => x.DataExclusao == null);

        var associations = (await _masterHouseAssociacaoRepository.SelectMasterHouseAssociacaoParam(param)) ??
            throw new BusinessException("Associação não Encontrada !");

        if (associations.Count == 0)
            throw new BusinessException("Associação(ões) não encontrada(s)!");

        if(associations.Any(x => x.SituacaoAssociacaoRFBId == 1) || associations.Any(x => x.SituacaoAssociacaoRFBId == 2))
            throw new BusinessException("Não é possivel alterar a Associação, uma vez que este está em processo de envio a RFB !");

        QueryJunction<House> paramHouses = new();
        paramHouses.Add(x => x.EmpresaId == userSession.CompanyId);
        paramHouses.Add(x => x.AgenteDeCargaId == request.FreightFowarderId);
        paramHouses.Add(x => houseIds.Contains(x.Id));
        paramHouses.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(paramHouses) ??
            throw new BusinessException("Não há houses a serem enviados !");

        if (houses.Count == 0)
            throw new BusinessException("Nenhum house selecionado !");

        return await PrepareUpdateAndSaveMasterHouseAssociation(
            userSession,
            associations,
            houses,
            request);
    }

    public async Task<List<MasterHouseAssociationResponse>> DesfazerAssociacaoMasterHouse(UserSession userSession, RemoveMasterHouseAssociationRequest request)
    {
        var associationsId = request.Associations.Select(x => x.MessageHeaderDocumentoId).ToArray();

        QueryJunction<MasterHouseAssociacao> param = new();
        param.Add(x => x.EmpresaId == userSession.CompanyId);
        param.Add(x => associationsId.Contains(x.MessageHeaderDocumentId));
        param.Add(x => x.DataExclusao == null);

        var associations = (await _masterHouseAssociacaoRepository.SelectMasterHouseAssociacaoParam(param)) ??
            throw new BusinessException("Associação não Encontrada/Excluida !");

        if (associations.Count == 0)
            throw new BusinessException("Associação(ões) não encontrada(s)!");

        if (associations.Any(x => x.SituacaoAssociacaoRFBId == 1) || associations.Any(x => x.SituacaoAssociacaoRFBId == 2))
            throw new BusinessException("Não é possivel remover a Associação, pois existe(m) associação(ões) processo de envio a RFB !");

        return await PrepareRemoveAndSaveMasterHouseAssociation(
            userSession,
            associations);
    }
    #endregion

    #region Private
    private static QueryJunction<House> GeneratePredicateParam(UserSession userSession, HouseListarRequest input)
    {
        QueryJunction<House> param = new QueryJunction<House>();
        param.Add(x => x.EmpresaId == userSession.CompanyId);
        param.Add(x => x.DataExclusao == null);

        if (input.AgenteDeCargaId != null)
            param.Add(x => x.AgenteDeCargaId == input.AgenteDeCargaId);

        if (input.DataProcessamento != null)
        {
            DateTime dataProcessamento = new DateTime(
                input.DataProcessamento.Value.Year,
                input.DataProcessamento.Value.Month,
                input.DataProcessamento.Value.Day,
                0, 0, 0, 0);
            param.Add(x => x.DataProcessamento == dataProcessamento);
        }

        if (!string.IsNullOrEmpty(input.NomeLike))
            param.Add(x => x.ConsignatarioNome.StartsWith(input.NomeLike));

        if (!string.IsNullOrEmpty(input.Numero))
            param.Add(x => x.Numero == input.Numero);

        if (input.StatusReceita != null)
            param.Add(x => x.SituacaoRFBId == input.StatusReceita);

        if (input.DataEnvioReceita != null)
        {
            DateTime dteInicial = new DateTime(
                input.DataEnvioReceita.Value.Year,
                input.DataEnvioReceita.Value.Month,
                input.DataEnvioReceita.Value.Day,
                0, 0, 0, 0);
            DateTime dteFinal = new DateTime(
                input.DataEnvioReceita.Value.Year,
                input.DataEnvioReceita.Value.Month,
                input.DataEnvioReceita.Value.Day,
                23, 59, 59, 997);
            param.Add(x => x.DataProcessadoRFB >= dteInicial && x.DataProcessadoRFB <= dteFinal);
        }

        if (input.DataCriacaoInicialUnica != null && input.DataCriacaoFinal != null)
        {
            DateTime dteInicial = new DateTime(
                input.DataCriacaoInicialUnica.Value.Year,
                input.DataCriacaoInicialUnica.Value.Month,
                input.DataCriacaoInicialUnica.Value.Day,
                0, 0, 0, 0);
            DateTime dteFinal = new DateTime(
                input.DataCriacaoFinal.Value.Year,
                input.DataCriacaoFinal.Value.Month,
                input.DataCriacaoFinal.Value.Day,
                23, 59, 59, 997);
            param.Add(x => x.CreatedDateTimeUtc >= dteInicial && x.CreatedDateTimeUtc <= dteFinal);
        }
        else
        {
            if (input.DataCriacaoInicialUnica != null)
            {
                DateTime dteInicial = new DateTime(
                input.DataCriacaoInicialUnica.Value.Year,
                input.DataCriacaoInicialUnica.Value.Month,
                input.DataCriacaoInicialUnica.Value.Day,
                0, 0, 0, 0);
                DateTime dteFinal = new DateTime(
                    input.DataCriacaoInicialUnica.Value.Year,
                    input.DataCriacaoInicialUnica.Value.Month,
                    input.DataCriacaoInicialUnica.Value.Day,
                    23, 59, 59, 997);
                param.Add(x => x.CreatedDateTimeUtc >= dteInicial && x.CreatedDateTimeUtc <= dteFinal);
            }
        }
        return param;
    }

    private async Task<List<MasterHouseAssociationResponse>> PrepareAndSaveMasterHouseAssociation(
        UserSession userSession,
        List<House> houses,
        AddMasterHouseAssociationRequest request)
    {
        List<MasterHouseAssociationResponse> response = new();

        foreach (var item in request.Masters)
        {
            var documentId =
                _masterHouseAssociacaoRepository.GetDocumentNumberByMaster(userSession.CompanyId, request.FreightFowarderId, item.MasterNumber) ??
                Ulid.NewUlid().ToString();

            var houseList = houses.Where(x => item.HouseIds.Contains(x.Id)).ToList();
            
            var processDate = houseList[0].DataProcessamento;

            var carrieDeclarationDate = item.CarrierDeclarationDate;

            var association = GenerateNewMasterHouseAssociation(
                userSession, processDate, houseList, documentId,item.MasterNumber, item.CarrierDeclarationDate);

            if (await SaveMasterHouseAssociation(association) > 0)
            {
                foreach (int houseId in item.HouseIds)
                {
                    MasterHouseAssociationChild child = new MasterHouseAssociationChild
                    {
                        EmpresaId = userSession.CompanyId,
                        CreatedDateTimeUtc = DateTime.UtcNow,
                        CriadoPeloId = userSession.UserId,
                        HouseId = houseId,
                        MasterHouseAssociationId = association.Id
                    };

                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociationChild(child);
                }
                _ = await _houseRepository.SaveChanges();
            }

            response.Add(MasterHouseAssociationResponse.FromMasterHouseAssociacao(association));
        }
        return response;
    }

    private async Task<List<MasterHouseAssociationResponse>> PrepareUpdateAndSaveMasterHouseAssociation(
        UserSession userSession,
        List<MasterHouseAssociacao> masterHouseAssociatios,
        List<House> houses,
        UpdateMasterHouseAssociationRequest request)
    {
        List<MasterHouseAssociationResponse> response = new();

        foreach (var association in masterHouseAssociatios)
        {
            foreach (var masterHouseAssociationChild in association.MasterHouseAssociationChildren)
            {
                masterHouseAssociationChild.DataExclusao = DateTime.UtcNow;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociationChild(masterHouseAssociationChild);
            }
        }

        foreach (var item in request.Associations)
        {
            var houseItems = houses.Where(x => item.HouseIds.Contains(x.Id)).ToList();
            var actualAssociation = masterHouseAssociatios.First(x => x.MessageHeaderDocumentId == item.MessageHeaderDocumentoId);

            var carrieDeclarationDate = item.CarrierDeclarationDate;

            GenerateUpdateMasterHouseAssociation(userSession, ref actualAssociation, houseItems, carrieDeclarationDate);

            if (await UpdateMasterHouseAssociation(actualAssociation) > 0)
            {
                foreach (int houseId in item.HouseIds)
                {
                    MasterHouseAssociationChild child = new MasterHouseAssociationChild
                    {
                        EmpresaId = userSession.CompanyId,
                        CreatedDateTimeUtc = DateTime.UtcNow,
                        CriadoPeloId = userSession.UserId,
                        HouseId = houseId,
                        MasterHouseAssociationId = actualAssociation.Id
                    };

                    _masterHouseAssociacaoRepository.InsertMasterHouseAssociationChild(child);
                }
                _ = await _houseRepository.SaveChanges();
            }

            response.Add(MasterHouseAssociationResponse.FromMasterHouseAssociacao(actualAssociation));
        }
        return response;
    }

    private async Task<List<MasterHouseAssociationResponse>> PrepareRemoveAndSaveMasterHouseAssociation(
        UserSession userSession,
        List<MasterHouseAssociacao> masterHouseAssociatios)
    {
        List<MasterHouseAssociationResponse> response = new();

        foreach (var association in masterHouseAssociatios)
        {
            association.DataExclusao = DateTime.UtcNow;

            foreach (var masterHouseAssociationChild in association.MasterHouseAssociationChildren)
            {
                masterHouseAssociationChild.DataExclusao = DateTime.UtcNow;
                _masterHouseAssociacaoRepository.UpdateMasterHouseAssociationChild(masterHouseAssociationChild);
            }

            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);

            _ = await _masterHouseAssociacaoRepository.SaveChangesAsync();
        }
        return new List<MasterHouseAssociationResponse>();
    }

    private async Task<int> SaveMasterHouseAssociation(MasterHouseAssociacao associacao)
    {
        _masterHouseAssociacaoRepository.InsertMasterHouseAssociacao(associacao);

        if (await _masterHouseAssociacaoRepository.SaveChangesAsync())
        {
            return associacao.Id;
        }
        return 0;
    }

    private async Task<int> UpdateMasterHouseAssociation(MasterHouseAssociacao associacao)
    {
        _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(associacao);

        if (await _masterHouseAssociacaoRepository.SaveChangesAsync())
            return associacao.Id;

        return 0;
    }

    private static MasterHouseAssociacao GenerateNewMasterHouseAssociation(
        UserSession userSession,
        DateTime processDate,
        List<House> houses,
        string documentId,
        string masterNumber,
        DateTime? carrieDeclarationDate)
    {
        if (houses.Count == 0) return null;

        string destinationLocation = houses[0].AeroportoDestinoCodigo;
        string originLocation = houses[0].AeroportoOrigemCodigo;
        string weightUnit = "KGM";
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

        return new MasterHouseAssociacao
        {
            ConsigmentItemQuantity = houses.Count,
            CreatedDateTimeUtc = DateTime.UtcNow,
            FinalDestinationLocation = destinationLocation,
            GrossWeight = totalWeight,
            GrossWeightUnit = weightUnit,
            MasterNumber = masterNumber,
            MessageHeaderDocumentId = documentId,
            OriginLocation = originLocation,
            PackageQuantity = totalPackages,
            TotalPieceQuantity = totalPieces,
            CriadoPeloId = userSession.UserId,
            EmpresaId = userSession.CompanyId,
            CarrierDeclarationDate = carrieDeclarationDate,
            AgenteDeCargaId = houses[0].AgenteDeCargaId,
            ProcessDate = processDate.Date
        };
    }

    private void GenerateUpdateMasterHouseAssociation(
        UserSession userSession,
        ref MasterHouseAssociacao masterHouseAssociacao,
        List<House> houses,
        DateTime? carrieDeclarationDate)
    {
        if (houses.Count == 0) return;

        string destinationLocation = houses[0].AeroportoDestinoCodigo;
        string originLocation = houses[0].AeroportoOrigemCodigo;
        string weightUnit = "KGM";
        string masterNumber = houses[0].MasterNumeroXML;
        DateTime processDate = houses[0].DataProcessamento;
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

        masterHouseAssociacao.ConsigmentItemQuantity = houses.Count;
        masterHouseAssociacao.ModifiedDateTimeUtc = DateTime.UtcNow;
        masterHouseAssociacao.FinalDestinationLocation = destinationLocation;
        masterHouseAssociacao.GrossWeight = totalWeight;
        masterHouseAssociacao.GrossWeightUnit = weightUnit;
        masterHouseAssociacao.MasterNumber = masterNumber;
        masterHouseAssociacao.OriginLocation = originLocation;
        masterHouseAssociacao.PackageQuantity = totalPackages;
        masterHouseAssociacao.TotalPieceQuantity = totalPieces;
        masterHouseAssociacao.ModificadoPeloId = userSession.UserId;
        masterHouseAssociacao.CarrierDeclarationDate = carrieDeclarationDate;
        var associationId = masterHouseAssociacao.Id;
        houses.ForEach(house => house.MasterHouseAssociacaoId = associationId);
    }

    private static ApiResponse<HouseResponseDto> ErrorHandling(Exception exception)
    {
        if (exception?.InnerException is SqlException sqlEx)
        {
            //This is a DbUpdateException on a SQL database

            if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
            {
                //We have an error we can process
                return new ApiResponse<HouseResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"Já existe um House cadastrado com o mesmo número e voo !"
                            }
                    }
                };
            }
            else
            {
                return new ApiResponse<HouseResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"{sqlEx.Message}"
                            }
                    }
                };
            }
        }
        else
        {
            return new ApiResponse<HouseResponseDto>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"9999",
                                Mensagem = $"{exception.Message}"
                            }
                    }
            };
        }

    }
    #endregion Private
}
