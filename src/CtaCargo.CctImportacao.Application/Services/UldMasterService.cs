using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services;

public class UldMasterService : IUldMasterService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;
    public const int SqlServerViolationOfUniqueNotFound = 547;

    private readonly IUldMasterRepository _uldMasterRepository;
    private readonly IMasterRepository _masterRepository;
    private readonly IVooRepository _vooRepository;
    private readonly IValidadorMaster _validadorMaster;
    private readonly IMapper _mapper;

    #region Construtores
    public UldMasterService(IUldMasterRepository uldMasterRepository,
        IMasterRepository masterRepository,
        IVooRepository vooRepository,
        IMasterService masterService,
        IMapper mapper, IValidadorMaster validadorMaster)
    {
        _uldMasterRepository = uldMasterRepository;
        _masterRepository = masterRepository;
        _vooRepository = vooRepository;
        _mapper = mapper;
        _validadorMaster = validadorMaster;
    }
    #endregion

    #region Métodos Publicos
    public async Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(UserSession userSession, int uldId)
    {
        try
        {
            var lista = await _uldMasterRepository.GetUldMasterById(uldId);
            if (lista == null)
            {
                return
                    new ApiResponse<UldMasterResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "ULD não encontrado !"
                            }
                        }
                    };
            }
            var dto = _mapper.Map<UldMasterResponseDto>(lista);
            return
                    new ApiResponse<UldMasterResponseDto>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<UldMasterResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Erro na aplicação! {ex.Message} !"
                            }
                        }
                    };
        }

    }
    public async Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(UserSession userSession, int masterId)
    {
        try
        {
            var lista = await _uldMasterRepository.GetUldMasterByMasterId(masterId);
            if (lista == null)
            {
                return
                    new ApiResponse<List<UldMasterResponseDto>>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "ULD não encontrado !"
                            }
                        }
                    };
            }
            var dto = _mapper.Map<List<UldMasterResponseDto>>(lista);
            return
                    new ApiResponse<List<UldMasterResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<List<UldMasterResponseDto>>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Erro na aplicação! {ex.Message} !"
                            }
                        }
                    };
        }

    }
    public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(UserSession userSession, int vooId)
    {
        try
        {
            List<UldMasterNumeroQuery> lista = await _uldMasterRepository.GetUldMasterByVooId(vooId);

            return
                    new ApiResponse<IEnumerable<UldMasterNumeroQuery>>
                    {
                        Dados = lista,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<UldMasterNumeroQuery>>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Erro na aplicação! {ex.Message} !"
                            }
                        }
                    };
        }

    }
    public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorTrechoId(UserSession userSession, int trechoId)
    {
        List<UldMasterNumeroQuery> lista = await _uldMasterRepository.GetUldMasterByTrechoId(trechoId);

        return new ApiResponse<IEnumerable<UldMasterNumeroQuery>>
        {
            Dados = lista,
            Sucesso = true,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumarioPorVooId(UserSession userSession, ListaUldMasterRequest input)
    {
        try
        {
            List<MasterNumeroUldSumario> lista = await _uldMasterRepository.GetUldMasterSumarioByVooId(input.vooId);

            return
                    new ApiResponse<IEnumerable<MasterNumeroUldSumario>>
                    {
                        Dados = lista,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<MasterNumeroUldSumario>>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Erro na aplicação! {ex.Message} !"
                            }
                        }
                    };
        }

    }
    public async Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(UserSession userSession, ListaUldMasterRequest input)
    {
        try
        {
            var lista = await _uldMasterRepository.GetUldMasterByLinha(input.vooId, input.uldLinha);

            var dto = _mapper.Map<IEnumerable<UldMasterResponseDto>>(lista);

            return
                    new ApiResponse<IEnumerable<UldMasterResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<UldMasterResponseDto>>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = $"Erro na aplicação! {ex.Message} !"
                            }
                        }
                    };
        }

    }
    public async Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(UserSession userSession, List<UldMasterInsertRequest> input, string inputMode="Manual")
    {

        var trecho = _vooRepository.SelectTrecho(input[0].TrechoId);

        if (trecho == null)
            throw new BusinessException("Trecho do voo não encontrado !");

        if (trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new BusinessException("Voo aguardando processamento na RFB. Atualize o status do voo!");

        if (trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Processed && !trecho.VooInfo.Reenviar)
            throw new BusinessException("Voo processado pela Receita Federal não pode ser alterado!");

        List<UldMaster> listaModel = new List<UldMaster>();

        foreach (var item in input)
        {
            var uld = new UldMaster
            {
                CreatedDateTimeUtc = DateTime.UtcNow,
                CriadoPeloId = userSession.UserId,
                EmpresaId = userSession.CompanyId,
                Environment = userSession.Environment,
                InputMode = inputMode,
                MasterNumero = item.MasterNumero,
                Peso = item.Peso,
                PesoUN = item.PesoUN,
                QuantidadePecas = item.QuantidadePecas,
                ULDCaracteristicaCodigo = item.UldCaracteristicaCodigo,
                ULDIdPrimario = item.UldIdPrimario,
                ULDId = item.UldId,
                VooTrechoId = trecho.Id,
                VooId = trecho.VooInfo.Id,
                Tranferencia = item.Transferencia,
                TotalParcial = item.TipoDivisao
            };

            UldMasterEntityValidator validator = new UldMasterEntityValidator();

            var masterId = await GetMasterId(userSession.CompanyId, item.MasterNumero);

            if (masterId == null)
                throw new BusinessException($"Master {item.MasterNumero} não foi encontrado no voo selecionado!");

            uld.MasterId = masterId;

            var result = validator.Validate(uld);

            if (!result.IsValid)
                throw new BusinessException($"{result.Errors[0].ErrorMessage}");

            listaModel.Add(uld);
        }

        if (await _uldMasterRepository.CreateUldMasterList(listaModel) == 0)
            throw new Exception("Não foi possivel inserir ULDs");

        var masterResponseDto = _mapper.Map<List<UldMasterResponseDto>>(listaModel);

        masterResponseDto.ForEach(item => item.UsuarioCriacao = userSession.UserName);

        return
            new ApiResponse<List<UldMasterResponseDto>>
            {
                Dados = masterResponseDto,
                Sucesso = true,
                Notificacoes = null
            };

    }
    public async Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(UserSession userSession, List<UldMasterUpdateRequest> input)
    {
        var trecho = _vooRepository.SelectTrecho(input[0].TrechoId);

        if (trecho == null)
            throw new BusinessException("Trecho não encontrado!");

        if (trecho.VooInfo.Reenviar == false && (trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Received))
            throw new BusinessException("Voo processando na Receita Federal não pode ser alterado!");

        if (trecho.VooInfo.Reenviar == false && trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Processed)
            throw new BusinessException("Voo processado pela Receita Federal não pode ser alterado!");

        List<UldMasterResponseDto> uldsDto = new List<UldMasterResponseDto>();

        foreach (var item in input)
        {
            var uld = await _uldMasterRepository.GetUldMasterById(item.Id);

            if (uld == null)
                throw new BusinessException($"Não foi possível atualiza a ULD: ULD {item.UldCaracteristicaCodigo + item.UldId + item.UldIdPrimario} não encontrado !");

            uld.MasterNumero = item.MasterNumero;
            uld.Peso = item.Peso;
            uld.PesoUN = item.PesoUN;
            uld.QuantidadePecas = item.QuantidadePecas;
            uld.ModificadoPeloId = userSession.UserId;
            uld.ModifiedDateTimeUtc = DateTime.UtcNow;
            uld.TotalParcial = item.TipoDivisao;
            uld.Tranferencia = item.Transferencia;
            if (item.UldCaracteristicaCodigo != null)
                uld.ULDCaracteristicaCodigo = item.UldCaracteristicaCodigo;
            if (item.UldId != null)
                uld.ULDId = item.UldId;
            if (item.UldIdPrimario != null)
                uld.ULDIdPrimario = item.UldIdPrimario;

            var masterId = await GetMasterId(userSession.CompanyId, item.MasterNumero);

            if (masterId == null)
                throw new BusinessException($"Master {item.MasterNumero} não foi encontrado no voo selecionado!");

            uld.MasterId = masterId;

            UldMasterEntityValidator validator = new UldMasterEntityValidator();

            var result = validator.Validate(uld);

            if (!result.IsValid)
                throw new BusinessException($"{result.Errors[0].ErrorMessage}");

            _uldMasterRepository.UpdateUldMaster(uld);

            var masterResponseDto = _mapper.Map<UldMasterResponseDto>(uld);

            uldsDto.Add(masterResponseDto);
        }
        if(_uldMasterRepository.SaveChanges() > 0 )
            return
                new ApiResponse<List<UldMasterResponseDto>>
                {
                    Dados = uldsDto,
                    Sucesso = true,
                    Notificacoes = null
                };

        throw new Exception("Internal error!");
    }
    public async Task<ApiResponse<string>> ExcluirUldMaster(UserSession userSession, UldMasterDeleteByIdInput input)
    {
        var trecho = _vooRepository.SelectTrecho(input.TrechoId);

        if (trecho == null)
            throw new BusinessException("Trecho não encontrado!");

        if (trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new BusinessException("Voo estpa sendo processado na RFB. Atualize o status do voo !");

        if (trecho.VooInfo.SituacaoRFBId == RFStatusEnvioType.Processed && !trecho.VooInfo.Reenviar)
            throw new BusinessException("Voo processado pela RFB não pode ser alterado!");

        var uldMasters = await _uldMasterRepository.GetUldMasterByIdList(input.ListaIds);
        _uldMasterRepository.DeleteUldMasterList(uldMasters, userSession.UserId);

        if(_uldMasterRepository.SaveChanges() == 0)
            throw new BusinessException("Não foi possível excluir ULD(s): ULD(s) não encontrado(s) !");

        await AtualizarValidacaoMaster(userSession.CompanyId, uldMasters);

        return new ApiResponse<string>
        {
            Dados = "ULD(s) excluidos com sucesso!",
            Sucesso = true,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<string>> ExcluirUld(UserSession userSession, UldMasterDeleteByTagInput input)
    {
        var voo = await _vooRepository.GetVooById(input.VooId);

        if (voo == null)
            throw new Exception("Voo não encontrado!");

        if (voo.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new Exception("Voo submetido a Receita Federal não pode ser alterado!");

        if (voo.SituacaoRFBId == RFStatusEnvioType.Processed)
            throw new Exception("Voo processado pela Receita Federal não pode ser alterado!");

        var uldMasters = await _uldMasterRepository.GetUldMasterByTag(input);

        _uldMasterRepository.DeleteUldMasterList(uldMasters, userSession.UserId);

        if (_uldMasterRepository.SaveChanges() == 0)
            throw new BusinessException("Não foi possível excluir ULD(s): ULD(s) não encontrado(s) !");

        await AtualizarValidacaoMaster(userSession.CompanyId, uldMasters);

        return new ApiResponse<string>
        {
            Dados = "ULD(s) excluidos com sucesso!",
            Sucesso = true,
            Notificacoes = null
        };
    }
    #endregion

    #region Metodos Privado
    private ApiResponse<List<UldMasterResponseDto>> ErrorHandling(Exception exception)
    {
        var sqlEx = exception?.InnerException as SqlException;
        if (sqlEx != null)
        {
            //This is a DbUpdateException on a SQL database

            if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                return new ApiResponse<List<UldMasterResponseDto>>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"Já existe um Master cadastrado nesta ULD !"
                            }
                    }
                };

            if (sqlEx.Number == SqlServerViolationOfUniqueNotFound)
                return new ApiResponse<List<UldMasterResponseDto>>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"Master não cadastrado no voo selecionado !"
                            }
                    }
                };

            return new ApiResponse<List<UldMasterResponseDto>>
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
        else
        {
            return new ApiResponse<List<UldMasterResponseDto>>
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
    private async Task<int?> GetMasterId(int companyId, string masterNumero)
    {
        var result = await _masterRepository.GetMasterByNumber(companyId, masterNumero);
        if (result == null)
            return null;

        return result.Id;
    }
    private async Task AtualizarValidacaoMaster(int companyId, List<UldMaster> uldMasters)
    {
        var grupo = uldMasters.GroupBy(x => new { x.VooId, x.MasterNumero });

        foreach (var uld in grupo)
        {
            var master = await _masterRepository.GetMasterIdByNumber(companyId, uld.Key.VooId, uld.Key.MasterNumero);
            if (master != null)
            {
                _validadorMaster.InserirErrosMaster(master);
                _masterRepository.UpdateMaster(master);
                await _masterRepository.SaveChanges();
            }
        }
    }
    #endregion
}
