using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
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
    private readonly IMasterHouseAssociacaoRepository _masterHouseAssociacaoRepository;

    public HouseService(IMapper mapper,
        IHouseRepository houseRepository,
        IPortoIATARepository portoIATARepository,
        IAgenteDeCargaRepository agenteDeCargaRepository,
        IMasterHouseAssociacaoRepository masterHouseAssociacaoRepository)
    {
        _houseRepository = houseRepository;
        _portoIATARepository = portoIATARepository;
        _mapper = mapper;
        _agenteDeCargaRepository = agenteDeCargaRepository;
        _masterHouseAssociacaoRepository = masterHouseAssociacaoRepository;
    }
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
    public async Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUpload(UserSession userSession, HouseListarRequest input)
    {
        var param = GeneratePredicateParam(userSession, input);
        var masters = _houseRepository.GetMastersByParam(param);
     
        if (masters is null) return default;
        if (masters.Length == 0) return default;

        var houses = _houseRepository
            .GetHouseByMasterList(masters);

        var association = await _masterHouseAssociacaoRepository
            .SelectMasterHouseAssociacaoParam(x => x.EmpresaId == userSession.CompanyId && 
                x.DataExclusao == null && masters.Contains(x.MasterNumber));

        var response = new List<MasterHouseAssociationUploadResponse>();
        foreach (var master in masters)
        {
            var findSummary = association.Find(x => x.MasterNumber == master);
            var findHouses = houses.Where(x => x.MasterNumeroXML == master);

            var resp = new MasterHouseAssociationUploadResponse
            {
                Number = master,
                Houses = (from c in findHouses
                          select new MasterHouseAssociationHouseItemResponse
                          {
                              Id = c.Id,
                              Number = c.Numero,
                              AssociationCheckDate = c.DataChecagemAssociacaoRFB,
                              AssociationDate = c.DataProtocoloAssociacaoRFB,
                              AssociationErrorCode = c.CodigoErroAssociacaoRFB,
                              AssociationErrorDescription = c.DescricaoErroAssociacaoRFB,
                              AssociationProtocol = c.ProtocoloAssociacaoRFB,
                              AssociationStatusId = c.SituacaoAssociacaoRFBId,
                              DestinationLocation = c.AeroportoDestinoCodigo,
                              OriginLocation = c.AeroportoOrigemCodigo,
                              DocumentId = findSummary?.MessageHeaderDocumentId,
                              PackageQuantity = c.TotalVolumes,
                              ResendAssociation = c.ReenviarAssociacao,
                              TotalPieceQuantity = c.TotalVolumes,
                              TotalWeight = c.PesoTotalBruto,
                              TotalWeightUnit = c.PesoTotalBrutoUN,
                              ProcessDate = c.DataProcessamento
                          }).ToList()
            };

            if (findSummary != null)
            {
                resp.Summary = new MasterHouseAssociationSummaryUploadResponse
                {
                    Id = findSummary.Id,
                    ConsignmentItemQuantity = findSummary.ConsigmentItemQuantity,
                    DestinationLocation = findSummary.FinalDestinationLocation,
                    DocumentId = findSummary.MessageHeaderDocumentId,
                    IssueDate = findSummary.CreatedDateTimeUtc,
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
                0, 0, 0,DateTimeKind.Unspecified);

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
    private QueryJunction<House> GeneratePredicateParam(UserSession userSession, HouseListarRequest input)
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
    private ApiResponse<HouseResponseDto> ErrorHandling(Exception exception)
    {
        var sqlEx = exception?.InnerException as SqlException;
        if (sqlEx != null)
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
}
