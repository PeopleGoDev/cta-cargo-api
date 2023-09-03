using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class VooService : IVooService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;

    private readonly IVooRepository _vooRepository;
    private readonly ICiaAereaRepository _ciaAereaRepository;
    private readonly IPortoIATARepository _portoIATARepository;
    private readonly IUldMasterRepository _uldMasterRepository;
    private readonly IMapper _mapper;

    public VooService(IVooRepository vooRepository, IMapper mapper, ICiaAereaRepository ciaAereaRepository, IPortoIATARepository portoIATARepository, IUldMasterRepository uldMasterRepository)
    {
        _ciaAereaRepository = ciaAereaRepository;
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
        _mapper = mapper;
        _uldMasterRepository = uldMasterRepository;
    }
    public async Task<ApiResponse<VooResponseDto>> VooPorId(int vooId, UserSession userSessionInfo)
    {

        var voo = await _vooRepository.GetVooById(vooId);
        if (voo == null)
        {
            return
                new ApiResponse<VooResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Voo não encontrado !"
                            }
                    }
                };
        }

        return new ApiResponse<VooResponseDto>
        {
            Dados = voo, // Implicit convertion
            Sucesso = true,
            Notificacoes = null
        };
    }
    public ApiResponse<IEnumerable<VooTrechoResponse>> VooTrechoPorVooId(UserSession userSessionInfo, int vooId)
    {

        var lista = _vooRepository.GetTrechoByVooId(vooId).ToList();
        if (lista == null)
        {
            return
                new ApiResponse<IEnumerable<VooTrechoResponse>>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Trecho não encontrado !"
                            }
                    }
                };
        }
        var dto = (from c in lista
                   select new VooTrechoResponse(c.Id, c.AeroportoDestinoCodigo));
        return
                new ApiResponse<IEnumerable<VooTrechoResponse>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }
    public async Task<ApiResponse<VooUploadResponse>> VooUploadPorId(int vooId, UserSession userSessionInfo)
    {
        var voo = await _vooRepository.GetVooById(vooId);

        if (voo == null)
            throw new BusinessException("Voo não encontrado!");

        var response = new VooUploadResponse
        {
            AeroportoDestinoCodigo = voo.AeroportoDestinoCodigo,
            AeroportoOrigemCodigo = voo.AeroportoOrigemCodigo,
            DataCriacao = voo.CreatedDateTimeUtc,
            DataHoraChegadaEstimada = voo.DataHoraChegadaEstimada,
            DataHoraSaidaReal = voo.DataHoraSaidaReal,
            DataProtocoloRFB = voo.DataProtocoloRFB,
            DataVoo = voo.DataVoo,
            ErroCodigoRFB = voo.CodigoErroRFB,
            ErroDescricaoRFB = voo.DescricaoErroRFB,
            Numero = voo.Numero,
            ProtocoloRFB = voo.ProtocoloRFB,
            Reenviar = voo.Reenviar,
            SituacaoRFBId = (int)voo.SituacaoRFBId,
            StatusId = (int)voo.StatusId,
            UsuarioCriacao = voo.UsuarioCriacaoInfo?.Nome,
            VooId = voo.Id,
            Trechos = voo.Trechos.Select(x => new VooTrechoResponse(x.Id, x.AeroportoDestinoCodigo,
                x.DataHoraChegadaEstimada,
                x.DataHoraSaidaEstimada)
            ).ToList(),
            ULDs = new List<UldMasterNumeroQuery>()
        };

        foreach (var trecho in voo.Trechos)
        {

            var result = trecho.ULDs.Where(x => x.DataExclusao == null)
                .Select(c => new UldMasterNumeroQueryChildren
                {
                    Id = c.Id,
                    DataCricao = c.CreatedDateTimeUtc,
                    MasterNumero = c.MasterNumero,
                    Peso = c.Peso,
                    PesoUnidade = c.PesoUN,
                    QuantidadePecas = c.QuantidadePecas,
                    UldCaracteristicaCodigo = c.ULDCaracteristicaCodigo,
                    UldId = c.ULDId,
                    UldIdPrimario = c.ULDIdPrimario,
                    UsuarioCriacao = c.UsuarioCriacaoInfo?.Nome,
                    TotalParcial = c.TotalParcial
                }).ToList();

            var result1 = result
                .GroupBy(g => new { g.UldCaracteristicaCodigo, g.UldId, g.UldIdPrimario })
                .Select(s => new UldMasterNumeroQuery
                {
                    ULDCaracteristicaCodigo = s.Key.UldCaracteristicaCodigo,
                    ULDId = s.Key.UldId,
                    ULDIdPrimario = s.Key.UldIdPrimario,
                    ULDs = s.ToList()
                }).ToList();

            response.ULDs.AddRange(result1);
        }

        return new ApiResponse<VooUploadResponse>
        {
            Dados = response,
            Sucesso = true,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<IEnumerable<VooResponseDto>>> ListarVoos(VooListarInputDto input, UserSession userSessionInfo)
    {
        if (input.DataInicial == null || input.DataFinal == null)
            throw new Exception("Datas parametros não referenciadas");

        QueryJunction<Voo> param = new QueryJunction<Voo>();

        param.Add(x => x.EmpresaId == userSessionInfo.CompanyId);
        DateTime dataInicial = new DateTime(
            input.DataInicial.Value.Year,
            input.DataInicial.Value.Month,
            input.DataInicial.Value.Day, 0, 0, 0, 0);
        DateTime dataFinal = new DateTime(
            input.DataFinal.Value.Year,
            input.DataFinal.Value.Month,
            input.DataFinal.Value.Day, 23, 59, 59, 997);
        param.Add(x => x.DataExclusao == null);
        param.Add(x => x.DataVoo >= dataInicial && x.DataVoo <= dataFinal);

        if (input.DataVoo != null)
            param.Add(x => x.DataVoo == input.DataVoo);

        var lista = await _vooRepository.GetAllVoos(param);

        var response = new List<VooResponseDto>();
        foreach (var voo in lista)
        {
            response.Add(voo); // Implicit convertion
        }

        return new ApiResponse<IEnumerable<VooResponseDto>>
        {
            Dados = response,
            Sucesso = true,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<IEnumerable<VooListaResponseDto>>> ListarVoosLista(VooListarInputDto input, UserSession userSessionInfo)
    {
        QueryJunction<Voo> param = new QueryJunction<Voo>();

        param.Add(x => x.EmpresaId == userSessionInfo.CompanyId);
        param.Add(x => x.DataExclusao == null);

        if (input.DataInicial != null && input.DataFinal != null)
        {
            DateTime dataInicial = new DateTime(
                input.DataInicial.Value.Year,
                input.DataInicial.Value.Month,
                input.DataInicial.Value.Day, 0, 0, 0, 0);
            DateTime dataFinal = new DateTime(
                input.DataFinal.Value.Year,
                input.DataFinal.Value.Month,
                input.DataFinal.Value.Day, 23, 59, 59, 997);

            param.Add(x => x.DataVoo >= dataInicial && x.DataVoo <= dataFinal);
        }

        if (input.DataVoo != null)
        {
            DateTime dataVoo = new DateTime(input.DataVoo.Value.Year,
                input.DataVoo.Value.Month,
                input.DataVoo.Value.Day,
                0, 0, 0, 0);
            param.Add(x => x.DataVoo == dataVoo);
        }

        var lista = await _vooRepository.GetVoosByDate(param);

        var dto = (from c in lista
                   select new VooListaResponseDto
                   {
                       CertificadoValidade = c.CertificadoValidade,
                       CiaAereaNome = c.CiaAereaNome,
                       Numero = c.Numero,
                       SituacaoVoo = (Dtos.Enum.RecordStatus)c.SituacaoVoo,
                       VooId = c.VooId,
                       Trechos = (from t in c.Trechos select new VooTrechoResponse(t.id, t.portoDestino))
                   });

        return new ApiResponse<IEnumerable<VooListaResponseDto>>
        {
            Dados = dto,
            Sucesso = true,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<VooResponseDto>> InserirVoo(VooInsertRequestDto input, UserSession userSession, string inputMode="Manual")
    {
        if (!ValidarNumeroVoo(input.Numero))
            throw new BusinessException("Número do voo invalido!");

        CiaAerea cia = await _ciaAereaRepository.GetCiaAereaByIataCode(userSession.CompanyId, input.Numero.Substring(0, 2));

        if (cia == null)
            throw new BusinessException($"Companhia Aérea {input.Numero.Substring(0, 2)} não cadastrada!");

        if (input.Trechos == null || input.Trechos.Count == 0)
            throw new BusinessException($"É necessário ao menos um aeroporto de chegada!");

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.Trechos.LastOrDefault().AeroportoDestinoCodigo);

        input.DataVoo = new DateTime(
            input.DataVoo.Year,
            input.DataVoo.Month,
            input.DataVoo.Day,
            0, 0, 0, 0);

        var voo = new Voo();

        voo.EmpresaId = userSession.CompanyId;
        voo.CriadoPeloId = userSession.UserId;
        voo.Environment = userSession.Environment;
        voo.InputMode = inputMode;
        voo.CreatedDateTimeUtc = DateTime.UtcNow;
        voo.CiaAereaId = cia.Id;
        voo.Numero = input.Numero;
        voo.CiaAereaId = cia.Id;
        voo.DataVoo = input.DataVoo;
        voo.DataHoraSaidaReal = input.DataHoraSaidaReal;
        voo.DataHoraSaidaEstimada = input.DataHoraSaidaPrevista;
        voo.PortoIataOrigemId = codigoOrigemId > 0 ? codigoOrigemId : null;
        voo.PortoIataDestinoId = codigoDestinoId > 0 ? codigoDestinoId : null;
        voo.DataEmissaoXML = DateTime.UtcNow;
        voo.AeroportoOrigemCodigo = input.AeroportoOrigemCodigo;
        voo.AeroportoDestinoCodigo = input.Trechos.LastOrDefault().AeroportoDestinoCodigo;

        VooEntityValidator validator = new VooEntityValidator();

        foreach (var item in input.Trechos)
        {
            var trecho = new VooTrecho
            {
                AeroportoDestinoCodigo = item.AeroportoDestinoCodigo,
                CreatedDateTimeUtc = DateTime.UtcNow,
                CriadoPeloId = userSession.UserId,
                DataHoraChegadaEstimada = item.DataHoraChegadaEstimada,
                DataHoraSaidaEstimada = item.DataHoraSaidaEstimada,
                EmpresaId = userSession.CompanyId,
                VooId = voo.Id
            };
            var codigoPortoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(item.AeroportoDestinoCodigo);
            trecho.PortoIataDestinoId = codigoPortoDestinoId > 0 ? codigoPortoDestinoId : null;

            voo.Trechos.Add(trecho);
        }

        var result = validator.Validate(voo);
        voo.StatusId = result.IsValid ? 1 : 0;

        _vooRepository.CreateVoo(voo);

        if (await _vooRepository.SaveChanges())
        {
            return
                new ApiResponse<VooResponseDto>
                {
                    Dados = voo, // Implicit convertion
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        throw new Exception("Não Foi possível adicionar o voo: Erro Desconhecido!");
    }
    public async Task<ApiResponse<VooResponseDto>> AtualizarVoo(VooUpdateRequestDto input, UserSession userSessionInfo)
    {
        var voo = await _vooRepository.GetVooById(input.VooId);

        if (voo == null)
            throw new BusinessException("Não foi possível atualiza o voo: Voo não encontrado !");

        if (input.Trechos == null || input.Trechos.Count == 0)
            throw new BusinessException($"É necessário ao menos um aeroporto de chegada!");

        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.Trechos.LastOrDefault().AeroportoDestinoCodigo);

        voo.ModifiedDateTimeUtc = DateTime.UtcNow;
        voo.ModificadoPeloId = userSessionInfo.UserId;
        if(input.Numero != null)
            voo.Numero = input.Numero;
        if (input.DataVoo != null)
            voo.DataVoo = input.DataVoo.Value;
        if (input.AeroportoOrigemCodigo != null)
        {
            var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
            voo.AeroportoOrigemCodigo = input.AeroportoOrigemCodigo;
            voo.PortoIataOrigemId = codigoOrigemId;
        }
        if (input.DataHoraSaidaReal != null)
            voo.DataHoraSaidaReal = input.DataHoraSaidaReal;
        if (input.DataHoraSaidaPrevista != null)
            voo.DataHoraSaidaEstimada = input.DataHoraSaidaPrevista;

        voo.PortoIataDestinoId = codigoDestinoId > 0 ? codigoDestinoId  : null;

        VooEntityValidator validator = new VooEntityValidator();

        foreach (var trecho in voo.Trechos)
        {
            if (!input.Trechos.Any(x => x.Id == trecho.Id))
            {
                _vooRepository.RemoveTrecho(trecho);
            };
        }

        foreach (var item in input.Trechos)
        {
            if (item.Id == null)
            {
                var trecho = new VooTrecho
                {
                    AeroportoDestinoCodigo = item.AeroportoDestinoCodigo,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    CriadoPeloId = userSessionInfo.UserId,
                    DataHoraChegadaEstimada = item.DataHoraChegadaEstimada,
                    DataHoraSaidaEstimada = item.DataHoraSaidaEstimada,
                    EmpresaId = userSessionInfo.CompanyId,
                    VooId = voo.Id
                };
                var codigoPortoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(item.AeroportoDestinoCodigo);
                trecho.PortoIataDestinoId = codigoPortoDestinoId > 0 ? codigoPortoDestinoId : null;

                voo.Trechos.Add(trecho);
            }
            else
            {
                var trecho = _vooRepository.SelectTrecho(item.Id.Value);

                trecho.ModificadoPeloId = userSessionInfo.UserId;
                trecho.ModifiedDateTimeUtc = DateTime.UtcNow;

                if (item.AeroportoDestinoCodigo != null)
                {
                    trecho.AeroportoDestinoCodigo = item.AeroportoDestinoCodigo;
                    var codigoPortoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(item.AeroportoDestinoCodigo);
                    trecho.PortoIataDestinoId = codigoPortoDestinoId > 0 ? codigoPortoDestinoId : null;
                }
                if (item.DataHoraChegadaEstimada != null)
                    trecho.DataHoraChegadaEstimada = item.DataHoraChegadaEstimada;
                if (item.DataHoraSaidaEstimada != null)
                    trecho.DataHoraSaidaEstimada = item.DataHoraSaidaEstimada;

                _vooRepository.UpdateTrecho(trecho);
            }
        }

        var result = validator.Validate(voo);
        voo.StatusId = result.IsValid ? 1 : 0;

        _vooRepository.UpdateVoo(voo);

        if (await _vooRepository.SaveChanges())
        {
            voo.Trechos = voo.Trechos.Where(x => x.DataExclusao == null).ToList();
            return
                new ApiResponse<VooResponseDto>
                {
                    Dados = voo, // Implicit convertion
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        throw new BusinessException("Não Foi possível adicionar o voo: Erro Desconhecido!");
    }
    public async Task<ApiResponse<VooResponseDto>> AtualizarReenviarVoo(int vooId, UserSession userSessionInfo)
    {

        var voo = await _vooRepository.GetVooById(vooId);

        if (voo == null)
        {
            return new ApiResponse<VooResponseDto>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Voo não encontrado !"
                        }
                }
            };
        }

        voo.Reenviar = true;
        VooEntityValidator validator = new VooEntityValidator();
        var result = validator.Validate(voo);
        voo.StatusId = result.IsValid ? 1 : 0;
        _vooRepository.UpdateVoo(voo);

        if (await _vooRepository.SaveChanges())
        {
            return new ApiResponse<VooResponseDto>
            {
                Dados = voo,
                Sucesso = true,
                Notificacoes = null
            };
        }
        else
        {
            return new ApiResponse<VooResponseDto>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Não foi possível atualiza o voo: Erro Desconhecido!"
                        }
                }
            };
        }
    }
    public async Task<ApiResponse<VooResponseDto>> ExcluirVoo(int vooId, UserSession userSessionInfo)
    {
        var vooRepo = await _vooRepository.GetVooForExclusionById(userSessionInfo.CompanyId, vooId);

        if (vooRepo == null)
            throw new BusinessException("Não foi possível excluir voo: Voo não encontrado !");

        if(vooRepo.Masters != null && vooRepo.Masters.Count > 0)
            throw new BusinessException("Não é possível excluir voo com master atrelado. Exclua/Mova o(s) master(s) atrelado(s) a este voo para continuar com a exclusão !");

        _vooRepository.DeleteVoo(vooRepo);

        if (await _vooRepository.SaveChanges())
        {
            return
                new ApiResponse<VooResponseDto>
                {
                    Dados = null,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
            throw new BusinessException("Não foi possível excluir voo: Erro Desconhecido !");
    }
    private ApiResponse<VooResponseDto> ErrorHandling(Exception exception)
    {
        var sqlEx = exception?.InnerException as SqlException;
        if (sqlEx != null)
        {
            //This is a DbUpdateException on a SQL database

            if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
            {
                //We have an error we can process
                return new ApiResponse<VooResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"Já existe um voo cadastrado com a mesma data !"
                            }
                    }
                };
            }
            else
            {
                return new ApiResponse<VooResponseDto>
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
            return new ApiResponse<VooResponseDto>
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
    private bool ValidarNumeroVoo(string voo)
    {
        var regex = @"^([A-Z0-9]{2}[0-9]{4})$";
        var match = Regex.Match(voo, regex);

        return match.Success;
    }
}
