using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services;

public class MasterService : IMasterService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;

    private readonly IMasterRepository _masterRepository;
    private readonly IVooRepository _vooRepository;
    private readonly IPortoIATARepository _portoIATARepository;
    private readonly INaturezaCargaRepository _naturezaCargaRepository;
    private readonly IErroMasterRepository _erroMasterRepository;
    private readonly IUldMasterRepository _uldMasterRepository;
    private readonly IValidadorMaster _validadorMaster;

    private readonly IMapper _mapper;
    public MasterService(IMasterRepository masterRepository,
        IVooRepository vooRepository,
        IPortoIATARepository portoIATARepository,
        IErroMasterRepository erroMasterRepository,
        INaturezaCargaRepository naturezaCargaRepository,
        IUldMasterRepository uldMasterRepository,
        IMapper mapper, 
        IValidadorMaster validadorMaster = null)
    {
        _masterRepository = masterRepository;
        _vooRepository = vooRepository;
        _portoIATARepository = portoIATARepository;
        _naturezaCargaRepository = naturezaCargaRepository;
        _erroMasterRepository = erroMasterRepository;
        _uldMasterRepository = uldMasterRepository;
        _mapper = mapper;
        _validadorMaster = validadorMaster;
    }
    public async Task<ApiResponse<MasterResponseDto>> MasterPorId(UserSession userSession, int masterId)
    {
        try
        {
            var lista = await _masterRepository.GetMasterById(userSession.CompanyId, masterId);
            if (lista == null)
            {
                return
                    new ApiResponse<MasterResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Master não encontrado !"
                            }
                        }
                    };
            }
            var dto = _mapper.Map<MasterResponseDto>(lista);
            return
                    new ApiResponse<MasterResponseDto>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<MasterResponseDto>
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
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMasters(UserSession userSession, MasterListarRequest input)
    {
        try
        {

            QueryJunction<Master> param = new QueryJunction<Master>();
            param.Add(x => x.DataExclusao == null);

            param.Add(x => x.EmpresaId == userSession.CompanyId);

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
            if (input.VooId != null)
                param.Add(x => x.VooId == input.VooId);

            var lista = await _masterRepository.GetAllMasters(param.ToPredicate());

            var dto = _mapper.Map<IEnumerable<MasterResponseDto>>(lista);

            return
                    new ApiResponse<IEnumerable<MasterResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<MasterResponseDto>>
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
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMastersPorDataCriacao(UserSession userSession, MasterHousePorDataCriacaoRequest input)
    {
        try
        {
            var lista = await _masterRepository.GetAllMastersByDataCriacao(userSession.CompanyId, input.DataCriacao);
            var dto = _mapper.Map<IEnumerable<MasterResponseDto>>(lista);
            return
                    new ApiResponse<IEnumerable<MasterResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<MasterResponseDto>>
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
    public async Task<ApiResponse<IEnumerable<MasterListaResponseDto>>> ListarMasterListaPorVooId(UserSession userSession, int vooId)
    {
        try
        {
            var lista = await _masterRepository.GetMastersListaByVooId(userSession.CompanyId, vooId);

            var dto = _mapper.Map<IEnumerable<MasterListaResponseDto>>(lista);
            return
                    new ApiResponse<IEnumerable<MasterListaResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<MasterListaResponseDto>>
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
    public async Task<ApiResponse<IEnumerable<MasterVooResponseDto>>> ListarMastersVoo(UserSession userSession, int vooId)
    {
        try
        {
            var lista = await _masterRepository.GetMastersVoo(userSession.CompanyId, vooId); ;
            var dto = _mapper.Map<IEnumerable<MasterVooResponseDto>>(lista);
            return
                    new ApiResponse<IEnumerable<MasterVooResponseDto>>
                    {
                        Dados = dto,
                        Sucesso = true,
                        Notificacoes = null
                    };
        }
        catch (Exception ex)
        {
            return
                    new ApiResponse<IEnumerable<MasterVooResponseDto>>
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
    public async Task<ApiResponse<MasterResponseDto>> InserirMaster(UserSession userSession, MasterInsertRequestDto input)
    {
        Voo voo;

        if (input.VooId > 0)
            voo = await _vooRepository.GetVooByIdSimple(userSession.CompanyId, input.VooId);
        else if (input.NumeroVooXML.Trim().Length != 6)
            throw new BusinessException("Número do voo deve conter apenas 6 caracteres!");
        else
        {
            DateTime dataVoo = new DateTime(input.DataVoo.Year,
                input.DataVoo.Month,
                input.DataVoo.Day,
                0, 0, 0, 0);

            voo = _vooRepository.GetVooIdByDataVooNumero(userSession.CompanyId, dataVoo, input.NumeroVooXML);
        }

        return await ProcessInsertMaster(userSession, input, voo);
    }
    public async Task<ApiResponse<MasterResponseDto>> AtualizarMaster(UserSession userSession, MasterUpdateRequestDto input)
    {
        DateTime dataVoo = new DateTime(input.DataVoo.Year,
            input.DataVoo.Month,
            input.DataVoo.Day,
            0, 0, 0, 0);

        Voo voo = _vooRepository.GetVooIdByDataVooNumero(userSession.CompanyId, dataVoo, input.NumeroVooXML);

        if (voo == null)
            throw new BusinessException($"Voo # {input.NumeroVooXML} não foi encontrado na data do voo {input.DataVoo.ToString("dd/MM/yyyy")}.");

        var master = await _masterRepository.GetMasterById(userSession.CompanyId, input.MasterId);

        if (master == null)
            throw new BusinessException($"Master não encontrado!");

        if (master.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new BusinessException($"Master não pode ser alterado, pois está em processamento na Receita Federal.");

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);
        int? codigoNaturezaCargaId = 0;

        if (input.NaturezaCarga != null && input.NaturezaCarga.Length == 3)
            codigoNaturezaCargaId = await _naturezaCargaRepository.GetNaturezaCargaIdByCodigo(input.NaturezaCarga);

        _mapper.Map(input, master);

        if (master.SituacaoRFBId == RFStatusEnvioType.Processed || master.SituacaoRFBId == RFStatusEnvioType.ProcessedDeletion)
            master.Reenviar = true;

        master.ModifiedDateTimeUtc = DateTime.UtcNow;
        master.ModificadoPeloId = userSession.UserId;
        master.VooId = voo.Id;
        master.VooNumeroXML = input.NumeroVooXML;
        master.AeroportoOrigemId = null;
        if (codigoOrigemId > 0)
            master.AeroportoOrigemId = codigoOrigemId;

        master.AeroportoDestinoId = null;
        if (codigoDestinoId > 0)
            master.AeroportoDestinoId = codigoDestinoId;

        master.NaturezaCargaId = null;
        if (codigoNaturezaCargaId > 0)
            master.NaturezaCargaId = codigoNaturezaCargaId;

        master.AutenticacaoSignatariaLocal = input.AeroportoOrigemCodigo;

        _validadorMaster.InserirErrosMaster(master);
        _masterRepository.UpdateMaster(master);

        if (await _masterRepository.SaveChanges())
        {
            var MasterResponseDto = _mapper.Map<MasterResponseDto>(master);
            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = MasterResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
        {
            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Não foi possível atualizar Master: Erro Desconhecido!"
                            }
                    }
                };
        }
    }
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> AtualizarReenviarMaster(UserSession userSession, AtualizarMasterReenviarRequest input)
    {
        try
        {
            var masters = await _masterRepository.GetMasterByIds(userSession.CompanyId, input.MasterIds);

            if (masters == null)
            {
                return
                new ApiResponse<IEnumerable<MasterResponseDto>>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Master(s) não encontrado(s) !"
                        }
                    }
                };
            }
            foreach (Master master in masters)
            {
                if (master.SituacaoRFBId == RFStatusEnvioType.Processed || master.SituacaoRFBId == RFStatusEnvioType.ProcessedDeletion)
                {
                    master.Reenviar = true;
                    _validadorMaster.InserirErrosMaster(master);
                    _masterRepository.UpdateMaster(master);
                }
            }

            if (await _masterRepository.SaveChanges())
            {
                var MasterResponseDto = _mapper.Map<IEnumerable<MasterResponseDto>>(masters);
                return
                new ApiResponse<IEnumerable<MasterResponseDto>>
                {
                    Dados = MasterResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
            }
            else
            {
                return
                new ApiResponse<IEnumerable<MasterResponseDto>>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Não foi possível atualizar Master: Erro Desconhecido!"
                        }
                    }
                };
            }
        }
        catch (Exception ex)
        {
            return
            new ApiResponse<IEnumerable<MasterResponseDto>>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                    new Notificacao
                    {
                        Codigo = "9999",
                        Mensagem = $"Não foi possível atualiza o Master: {ex.Message} !"
                    }
                }
            };
        }
    }
    public async Task<ApiResponse<string>> ExcluirMaster(UserSession userSession, ExcluirMastersByIdRequest input)
    {
        try
        {
            var masters = await _masterRepository.GetMasterByIds(userSession.CompanyId, input.MasterIds);

            if (masters == null)
            {
                return
                new ApiResponse<string>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Não foi possível excluir Master: Master não encontrado !"
                        }
                    }
                };
            }

            foreach (Master master in masters)
            {
                if (master.SituacaoRFBId == RFStatusEnvioType.Received || master.SituacaoRFBId == RFStatusEnvioType.ReceivedDeletion)
                    throw new Exception("Master em processamento na Receita Federal. Verifique o status do master, submeta o master para 'Exclusion' na Receita Federal, só então prossiga com a exclusão.");

                if (master.SituacaoRFBId == RFStatusEnvioType.Processed)
                    throw new Exception("Master processado na Receita Federal. Submeta o master para 'Exclusion' na Receita Federal e então prossiga com a exclusão.");

                var uldsMaster = await _uldMasterRepository.GetUldMasterByMasterId(master.Id);
                _uldMasterRepository.DeleteUldMasterList(uldsMaster, userSession.UserId);
                _masterRepository.DeleteMaster(userSession.CompanyId, master);
            }

            if (await _masterRepository.SaveChanges())
            {
                return
                new ApiResponse<string>
                {
                    Dados = "Excluido com sucesso!",
                    Sucesso = true,
                    Notificacoes = null
                };
            }
            else
            {
                return
                new ApiResponse<string>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                        new Notificacao
                        {
                            Codigo = "9999",
                            Mensagem = "Não foi possível excluir Master: Erro Desconhecido!"
                        }
                    }
                };
            }
        }
        catch (Exception ex)
        {
            return
            new ApiResponse<string>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = new List<Notificacao>() {
                    new Notificacao
                    {
                        Codigo = "9999",
                        Mensagem = $"Não foi possível excluir Master: {ex.Message} !"
                    }
                }
            };
        }

    }
    public async Task<ApiResponse<List<MasterResponseDto>>> ImportFile(UserSession userSession, MasterFileImportRequest input,  Stream stream)
    {
        var fileImport = _masterRepository.GetMasterFileImportById(userSession.CompanyId, input.FileImportId);

        if (fileImport == null)
            throw new BusinessException("Template não definido para importação!");

        var fileService = new FileService();

        var lines = fileService.ReadLines(stream, Encoding.UTF8, fileImport);

        stream.Close();

        var responses = new ApiResponse<List<MasterResponseDto>>();
        responses.Sucesso = true;

        Voo voo = await _vooRepository.GetVooByIdSimple(userSession.CompanyId, input.VooId);

        if (voo == null)
            throw new BusinessException($"Voo selecionado não encontrado !");

        if (voo.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new BusinessException($"O Voo # {voo.Numero} não pode ser alterado, pois está em processamento na Receita Federal. Faça a verificação do Voo para atualizar o Status.");

        if (voo.SituacaoRFBId == RFStatusEnvioType.Processed && !voo.Reenviar)
            throw new BusinessException($"O Voo # {voo.Numero} não pode ser alterado, pois foi submetido a RFB.");

        int fileLine = 1 + (fileImport.FirstLineTitle ? 1 : 0);

        foreach (var line in lines) 
        {
            try
            {
                var insertMaster = fileService.ReadLineToClass<MasterInsertRequestDto>(line, fileImport);

                insertMaster.VooId = input.VooId;
                insertMaster.IndicadorAwbNaoIata = IsAwbNonIata(insertMaster.Numero);

                var response = await ProcessInsertMaster(userSession, insertMaster, voo, "Importação Manual");
                if (responses.Dados == null)
                    responses.Dados = new List<MasterResponseDto>();
                responses.Dados.Add(response.Dados);
            }
            catch (Exception ex)
            {
                if(responses.Notificacoes == null)
                    responses.Notificacoes = new List<Notificacao>();

                string message = $"Erro na lina {fileLine} do arquivo:{ex.InnerException?.Message ?? ex.Message}";

                responses.Notificacoes.Add(new
                        Notificacao
                { Codigo = "ERR1201", Mensagem = message });
                _masterRepository.ClearChangeTracker();
            }
            fileLine++;
        }
        return responses;

    }
    public async Task<ApiResponse<List<MasterFileResponseDto>>> GetFilesToImport(UserSession userSession)
    {
        var data = _masterRepository.GetMasterFileImportList(userSession.CompanyId);
        if(data == null)
            return default(ApiResponse<List<MasterFileResponseDto>>);

        return new ApiResponse<List<MasterFileResponseDto>>
        {
            Dados = (from c in data
                     select new MasterFileResponseDto
                     {
                         FileImportId = c.Id,
                         Description = c.Description
                     }).ToList(),
            Sucesso = true
        };
    }
    #region Metodos Privado
    private async Task<ApiResponse<MasterResponseDto>> ProcessInsertMaster(UserSession userSession, MasterInsertRequestDto input, Voo voo, string inputMode = "Manual")
    {
        if (voo == null)
            throw new BusinessException($"Voo # {input.NumeroVooXML} não foi encontrado na data do voo {input.DataVoo.ToString("dd/MM/yyyy")}.");

        //if (voo.SituacaoRFBId == RFStatusEnvioType.Received)
        //    throw new BusinessException($"O Voo # {voo.Numero} não pode ser alterado, pois está em processamento na Receita Federal. Faça a verificação do Voo para atualizar o Status.");

        //if (voo.SituacaoRFBId == RFStatusEnvioType.Processed && !voo.Reenviar)
        //    throw new BusinessException($"O Voo # {voo.Numero} não pode ser alterado, pois foi submetido a RFB.");

        DateTime dataLimite = DateTime.Today.AddYears(-1);

        int masterId = await _masterRepository.GetMasterIdByNumberValidate(voo.CiaAereaId, input.Numero, dataLimite);

        if (masterId > 0)
            throw new BusinessException($"Já existe um master no número {input.Numero} dentro dos últimos 365 dias!");

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);

        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);

        int? codigoNaturezaCargaId = 0;

        if (input.NaturezaCarga != null && input.NaturezaCarga.Length == 3)
            codigoNaturezaCargaId = await _naturezaCargaRepository.GetNaturezaCargaIdByCodigo(input.NaturezaCarga);

        var master = _mapper.Map<Master>(input);
        master.ErrosMaster = new List<ErroMaster>();

        master.EmpresaId = userSession.CompanyId;
        master.Environment = userSession.Environment;
        master.InputMode = inputMode;
        master.CiaAereaId = voo.CiaAereaId;
        master.CriadoPeloId = userSession.UserId;
        master.CreatedDateTimeUtc = DateTime.UtcNow;
        master.VooId = voo.Id;
        master.VooNumeroXML = input.NumeroVooXML;
        master.AeroportoOrigemId = null;

        if (codigoOrigemId > 0)
            master.AeroportoOrigemId = codigoOrigemId;

        master.AeroportoDestinoId = null;

        if (codigoDestinoId > 0)
            master.AeroportoDestinoId = codigoDestinoId;

        master.NaturezaCargaId = null;

        if (codigoNaturezaCargaId > 0)
            master.NaturezaCargaId = codigoNaturezaCargaId;

        _validadorMaster.InserirErrosMaster(master);

        master.AutenticacaoSignatariaNome = string.IsNullOrEmpty(input.AssinaturaTransportadorNome) ? voo.CompanhiaAereaInfo.Nome : input.AssinaturaTransportadorNome;
        master.AutenticacaoSignatarioData = input.AssinaturaTransportadorData ?? DateTime.Now;
        master.AutenticacaoSignatariaLocal = input.AeroportoOrigemCodigo ?? voo.PortoIataOrigemInfo.Nome;

        _masterRepository.CreateMaster(userSession.CompanyId, master);

        if (await _masterRepository.SaveChanges())
        {
            var MasterResponseDto = _mapper.Map<MasterResponseDto>(master);

            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = MasterResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
        {
            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = "9999",
                                Mensagem = "Não Foi possível adicionar o Master: Erro Desconhecido!"
                            }
                    }
                };
        }

    }
    private ApiResponse<MasterResponseDto> ErrorHandling(Exception exception)
    {
        var sqlEx = exception?.InnerException as SqlException;
        if (sqlEx != null)
        {
            //This is a DbUpdateException on a SQL database

            if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
            {
                //We have an error we can process
                return new ApiResponse<MasterResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                            new Notificacao
                            {
                                Codigo = $"SQL{sqlEx.Number.ToString()}",
                                Mensagem = $"Já existe um Master cadastrado com o mesmo número e voo !"
                            }
                    }
                };
            }
            else
            {
                return new ApiResponse<MasterResponseDto>
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
            return new ApiResponse<MasterResponseDto>
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

    private bool IsAwbNonIata(string e)
    {
        if (e.Length != 11)
            throw new Exception("Número do master deve conter 11 digitos");

        if (!int.TryParse(e,out int digits))
            return true;

        var digitos7 = Convert.ToInt32(e.Substring(3,7));
        var digito = Convert.ToInt32(e.Substring(10,1));
        var digitoesperado = digitos7 % 7;
        if (digito == digitoesperado)
            return false;
        return true;
    }
    #endregion
}

public class FileService
{
    public List<string[]> ReadLines(Stream stream, 
        Encoding encoding, 
        FileImport fileImport)
    {
        var lines = new List<string>();
        using (var reader = new StreamReader(stream, encoding))
        {
            
            string line = String.Empty;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }
        return ReadCSVFile(lines.ToArray(), fileImport);
    }
    private List<string[]> ReadCSVFile(string[] lines,FileImport fileImport)
    {
        int line = fileImport.FirstLineTitle ? 1 : 0;
        var result = new List<string[]>();

        for (int i = line; i < lines.Length; i++)
        {
            TextFieldParser parser = new TextFieldParser(new StringReader(lines[i]));

            parser.SetDelimiters(fileImport.Configuration1);
            parser.HasFieldsEnclosedInQuotes = Convert.ToBoolean(fileImport.Configuration2);
            
            string[] fields = null;

            while (!parser.EndOfData)
            {
                Console.WriteLine(i);
                fields = parser.ReadFields();
            }

            parser.Close();

            if (fields.Length != fileImport.Details.ToArray().Length)
                throw new BusinessException("Formato do arquivo invalido!");

            result.Add(fields);
        }

        return result;
    }
    public T ReadLineToClass<T>(string[] line, FileImport fileImport)
        where T : new()
    {
        var mapFields = fileImport.Details.OrderBy(x => x.Sequency).ToArray();

        var newItem = new T();

        try
        {
            for (int i = 0; i < line.Length; i++)
            {
                PropertyInfo propertyInfo = newItem.GetType().GetProperty(mapFields[i].ColumnAssociate);
                switch (mapFields[i].ColumnAssociate)
                {
                    case "DataEmissaoXML":
                        propertyInfo.SetValue(newItem, DateTime.ParseExact(line[i], "yyyy-MM-ddTHH:mm:ss", null), null);
                        break;
                    case "NCMLista":
                        propertyInfo.SetValue(newItem, line[i].Split(","));
                        break;
                    default:
                        propertyInfo.SetValue(newItem, Convert.ChangeType(line[i], propertyInfo.PropertyType), null);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Erro na conversão da linha paro o objeto : {ex.Message}");
        }

        return newItem;
    }
}

public class MasterFileImportRequest
{
    public int VooId { get; set; }
    public int FileImportId { get; set; }
}

public class MasterFileResponseDto
{
    public int FileImportId { get; set; }
    public string Description { get; set; }
}