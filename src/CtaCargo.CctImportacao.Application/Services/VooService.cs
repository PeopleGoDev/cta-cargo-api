using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Validator;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Domain.Model;

namespace CtaCargo.CctImportacao.Application.Services;

public class VooService : IVooService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;

    private readonly IVooRepository _vooRepository;
    private readonly ICiaAereaRepository _ciaAereaRepository;
    private readonly IPortoIATARepository _portoIATARepository;

    public VooService(IVooRepository vooRepository, 
        ICiaAereaRepository ciaAereaRepository, 
        IPortoIATARepository portoIATARepository)
    {
        _ciaAereaRepository = ciaAereaRepository;
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
    }
    public async Task<ApiResponse<VooResponseDto>> VooPorId(int vooId, UserSession userSessionInfo)
    {
        var voo = await _vooRepository.GetVooById(vooId);
        
        if (voo == null)
            throw new BusinessException("Voo não encontrado!");

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
            throw new BusinessException("Trecho não encontrado");

        var dto = (from c in lista
                   select new VooTrechoResponse { Id = c.Id, AeroportoDestinoCodigo = c.AeroportoDestinoCodigo });
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

        VooUploadResponse response = voo;

        foreach (var trechoResp in response.Trechos)
        {
            var trecho = voo.Trechos.First(x => x.Id == trechoResp.Id);
            trechoResp.ULDs = new List<UldMasterNumeroQuery>();
            
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
                    TotalParcial = c.TotalParcial,
                    AeroportoOrigem = c.PortOfOrign,
                    AeroportoDestino = c.PortOfDestiny,
                    DescricaoMercadoria = c.SummaryDescription
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


            trechoResp.ULDs.AddRange(result1);
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
            throw new BusinessException("Data inicial e Data final são requeridas");

        QueryJunction<Voo> param = new QueryJunction<Voo>();

        param.Add(x => x.EmpresaId == userSessionInfo.CompanyId);
        DateTime dataInicial = new DateTime(
            input.DataInicial.Value.Year,
            input.DataInicial.Value.Month,
            input.DataInicial.Value.Day, 0, 0, 0, DateTimeKind.Unspecified);
        DateTime dataFinal = new DateTime(
            input.DataFinal.Value.Year,
            input.DataFinal.Value.Month,
            input.DataFinal.Value.Day, 23, 59, 59, 997, DateTimeKind.Unspecified);
        param.Add(x => x.DataExclusao == null);
        param.Add(x => x.DataVoo >= dataInicial && x.DataVoo <= dataFinal);

        if (input.DataVoo != null)
            param.Add(x => x.DataVoo == input.DataVoo);

        var lista = await _vooRepository.GetAllVoos(param);

        var response = new List<VooResponseDto>();
        
        foreach (var voo in lista)
            response.Add(voo); // Implicit convertion

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
                input.DataInicial.Value.Day, 0, 0, 0, 0, DateTimeKind.Unspecified);
            DateTime dataFinal = new DateTime(
                input.DataFinal.Value.Year,
                input.DataFinal.Value.Month,
                input.DataFinal.Value.Day, 23, 59, 59, 997, DateTimeKind.Unspecified);

            param.Add(x => x.DataVoo >= dataInicial && x.DataVoo <= dataFinal);
        }

        if (input.DataVoo != null)
        {
            DateTime dataVoo = new DateTime(input.DataVoo.Value.Year,
                input.DataVoo.Value.Month,
                input.DataVoo.Value.Day,
                0, 0, 0, 0, DateTimeKind.Unspecified);
            param.Add(x => x.DataVoo == dataVoo);
        }

        var lista = await _vooRepository.GetVoosByDate(param);

        var dto = (from c in lista
                   select new VooListaResponseDto
                   {
                       CertificadoValidade = c.CertificadoValidade,
                       CiaAereaNome = c.CiaAereaNome,
                       Numero = c.Numero,
                       FlightType = c.FlightType,
                       SituacaoVoo = (Dtos.Enum.RecordStatus)c.SituacaoVoo,
                       VooId = c.VooId,
                       GhostFlight = c.GhostFlight,
                       Trechos = (from t in c.Trechos select new VooTrechoResponse { Id = t.id, AeroportoDestinoCodigo = t.portoDestino })
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

        var codigoOrigem = _portoIATARepository
            .GetPortoIATAByCode(userSession.CompanyId, input.AeroportoOrigemCodigo);
        var codigoDestino = _portoIATARepository
            .GetPortoIATAByCode(userSession.CompanyId, input.Trechos[input.Trechos.Count - 1].AeroportoDestinoCodigo);

        input.DataVoo = new DateTime(
            input.DataVoo.Year,
            input.DataVoo.Month,
            input.DataVoo.Day,
            0, 0, 0, 0, DateTimeKind.Unspecified);

        var voo = new Voo();

        voo.EmpresaId = userSession.CompanyId;
        voo.CriadoPeloId = userSession.UserId;
        voo.Environment = userSession.Environment;
        voo.InputMode = inputMode;
        voo.CreatedDateTimeUtc = DateTime.UtcNow;
        voo.CiaAereaId = cia.Id;
        voo.Numero = input.Numero;
        voo.FlightType = input.FlightType;
        voo.CiaAereaId = cia.Id;
        voo.DataVoo = input.DataVoo;
        voo.DataHoraSaidaReal = input.DataHoraSaidaReal;
        voo.DataHoraSaidaEstimada = input.DataHoraSaidaPrevista;
        voo.CountryOrigin = codigoOrigem != null ? codigoOrigem.SiglaPais : null;
        voo.PrefixoAeronave = input.PrefixoAeronave;
        voo.PortoIataOrigemId = codigoOrigem !=  null ? codigoOrigem.Id : null;
        voo.PortoIataDestinoId = codigoDestino != null ? codigoDestino.Id : null;
        voo.DataEmissaoXML = DateTime.UtcNow;
        voo.AeroportoOrigemCodigo = input.AeroportoOrigemCodigo;
        voo.AeroportoDestinoCodigo = input.Trechos[input.Trechos.Count - 1].AeroportoDestinoCodigo;
        voo.GhostFlight = cia.OnlyGhostFlight;

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
        throw new BusinessException("Não Foi possível adicionar o voo: Erro Desconhecido!");
    }
    public async Task<ApiResponse<VooResponseDto>> AtualizarVoo(VooUpdateRequestDto input, UserSession userSessionInfo)
    {
        var voo = await _vooRepository.GetVooById(input.VooId);

        if (voo == null)
            throw new BusinessException("Voo não encontrado");

        if (input.Trechos == null || input.Trechos.Count == 0)
            throw new BusinessException($"É necessário ao menos um aeroporto de chegada");

        var codigoDestinoId = await _portoIATARepository
            .GetPortoIATAIdByCodigo(input.Trechos[input.Trechos.Count-1].AeroportoDestinoCodigo);

        voo.ModifiedDateTimeUtc = DateTime.UtcNow;
        voo.ModificadoPeloId = userSessionInfo.UserId;
        if(input.Numero != null)
            voo.Numero = input.Numero;

        if (input.DataVoo != null)
            voo.DataVoo = input.DataVoo.Value;

        voo.AeroportoOrigemCodigo = null;
        voo.PortoIataOrigemId = null;
        voo.CountryOrigin = null;

        if (input.AeroportoOrigemCodigo != null)
        {
            var codigoOrigem = _portoIATARepository.GetPortoIATAByCode(userSessionInfo.CompanyId, input.AeroportoOrigemCodigo);
            voo.AeroportoOrigemCodigo = input.AeroportoOrigemCodigo;
            voo.PortoIataOrigemId = codigoOrigem?.Id;
            voo.CountryOrigin = codigoOrigem?.SiglaPais;
        }

        if (input.DataHoraSaidaReal != null)
            voo.DataHoraSaidaReal = input.DataHoraSaidaReal;

        if (input.DataHoraSaidaPrevista != null)
            voo.DataHoraSaidaEstimada = input.DataHoraSaidaPrevista;

        voo.AeroportoDestinoCodigo = input.Trechos[input.Trechos.Count - 1].AeroportoDestinoCodigo;

        voo.PortoIataDestinoId = codigoDestinoId > 0 ? codigoDestinoId  : null;
        
        if(input.PrefixoAeronave != null)
            voo.PrefixoAeronave = input.PrefixoAeronave;

        VooEntityValidator validator = new VooEntityValidator();

        foreach (var trecho in voo.Trechos)
            if (!input.Trechos.Any(x => x.Id == trecho.Id))
                _vooRepository.RemoveTrecho(trecho);

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
    public async Task<ApiResponse<VooResponseDto>> CloneFlightForDeparturing(UserSession userSession, CloneFlightForDeparturingRequest input)
    {
        CiaAerea cia = await _ciaAereaRepository.GetCiaAereaByIataCode(userSession.CompanyId, input.FlightNumber.Substring(0, 2));

        if (cia == null)
            throw new BusinessException($"Companhia Aérea {input.FlightNumber.Substring(0, 2)} não cadastrada!");

        var voo = await _vooRepository.GetVooById(input.FlightId);

        if (voo == null)
            throw new BusinessException("Voo não encontrado");

        Voo newFlight = new Voo();
        newFlight.Trechos = new List<VooTrecho>();

        bool found = false;
        VooTrecho lastSegment = null;

        foreach (var segment in voo.Trechos)
        {
            if (found)
            {
                var newSegment = new VooTrecho
                {
                    AeroportoDestinoCodigo = segment.AeroportoDestinoCodigo,
                    PortoIataDestinoId = segment.PortoIataDestinoId,
                    EmpresaId = segment.EmpresaId,
                    CriadoPeloId = userSession.UserId,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    DataHoraChegadaEstimada = segment.DataHoraChegadaEstimada,
                    DataHoraSaidaEstimada = segment.DataHoraSaidaEstimada,
                    VooId = newFlight.Id
                };
                newSegment.ULDs = new List<UldMaster>();

                foreach (var uld in segment.ULDs)
                {
                    var newUld = new UldMaster
                    {
                        CriadoPeloId = userSession.UserId,
                        EmpresaId = userSession.CompanyId,
                        Environment = userSession.Environment,
                        InputMode = uld.InputMode,
                        MasterId = uld.MasterId,
                        Peso = uld.Peso,
                        PesoUN = uld.PesoUN,
                        MasterNumero = uld.MasterNumero,
                        CreatedDateTimeUtc = DateTime.UtcNow,
                        TotalParcial = uld.TotalParcial,
                        Tranferencia = uld.Tranferencia,
                        ULDCaracteristicaCodigo = uld.ULDCaracteristicaCodigo,
                        ULDId = uld.ULDId,
                        ULDIdPrimario = uld.ULDIdPrimario,
                        ULDObs = uld.ULDObs,
                        QuantidadePecas = uld.QuantidadePecas,
                        VooId = newFlight.Id,
                        VooTrechoId = segment.Id,
                    };

                    newSegment.ULDs.Add(newUld);
                }

                newFlight.Trechos.Add(newSegment);
            }

            if(segment.Id ==  input.SegmentId)
            {
                if (segment.PortoIataDestinoInfo == null || segment.PortoIataDestinoInfo.SiglaPais != "BR")
                    throw new BusinessException("Trecho selecionado não possui Porto IATA ou Porto IATA não é do Brasil");

                found = true;

                newFlight.AeroportoOrigemCodigo = segment.AeroportoDestinoCodigo;
                newFlight.CiaAereaId = voo.CiaAereaId;
                newFlight.CreatedDateTimeUtc = DateTime.UtcNow;
                newFlight.CriadoPeloId = userSession.UserId;
                newFlight.DataHoraSaidaEstimada = segment.DataHoraSaidaEstimada;
                newFlight.DataVoo = new DateTime( 
                    segment.DataHoraSaidaEstimada.Value.Year,
                    segment.DataHoraSaidaEstimada.Value.Month,
                    segment.DataHoraSaidaEstimada.Value.Day,
                    0,0,0, DateTimeKind.Unspecified);
                newFlight.EmpresaId = voo.EmpresaId;
                newFlight.Environment = userSession.Environment;
                newFlight.Numero = input.FlightNumber;
                newFlight.ParentFlightId = voo.Id;
                newFlight.PortoIataOrigemId = segment.PortoIataDestinoId;
                newFlight.SituacaoRFBId = 0;
                newFlight.DataEmissaoXML = DateTime.UtcNow;
            }
            lastSegment = segment;
        }

        if (!found)
            throw new BusinessException("Trecho não encontrado");

        if(lastSegment.Id != input.SegmentId)
        {
            newFlight.PortoIataDestinoId = lastSegment.PortoIataDestinoId;
            newFlight.AeroportoDestinoCodigo = lastSegment.AeroportoDestinoCodigo;
        }

        _vooRepository.CreateVoo(newFlight);

        if(await _vooRepository.SaveChanges())
        {
            return
                new ApiResponse<VooResponseDto>
                {
                    Dados = newFlight, // Implicit convertion
                    Sucesso = true,
                    Notificacoes = null
                };
        }

        throw new BusinessException("Não foi possivel gerar o voo");
    }
    #region Private Methods
    private bool ValidarNumeroVoo(string voo)
    {
        var regex = @"^([A-Z0-9]{2}[0-9]{4})$";
        var match = Regex.Match(voo, regex);

        return match.Success;
    }
    #endregion
}
