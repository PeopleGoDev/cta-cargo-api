using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
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
        var lista = await _masterRepository.GetMasterById(userSession.CompanyId, masterId) ??
            throw new BusinessException("Master não encontrado!");

        var dto = _mapper.Map<MasterResponseDto>(lista);

        return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
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
        if (input.VooId == 0 && input.NumeroVooXML.Trim().Length != 6)
            throw new BusinessException("Número do voo deve conter apenas 6 caracteres!");

        Voo voo;

        if (input.VooId > 0)
            voo = await _vooRepository.GetVooByIdSimple(userSession.CompanyId, input.VooId);
        else
        {
            DateTime dataVoo = new DateTime(input.DataVoo.Year,
                input.DataVoo.Month,
                input.DataVoo.Day,
                0, 0, 0, 0, DateTimeKind.Unspecified);

            voo = _vooRepository.GetVooIdByDataVooNumero(userSession.CompanyId, dataVoo, input.NumeroVooXML.Trim());
        }

        return await ProcessInsertMaster(userSession, input, voo);
    }
    public async Task<ApiResponse<MasterResponseDto>> AtualizarMaster(UserSession userSession, MasterUpdateRequestDto input)
    {
        DateTime dataVoo = new DateTime(input.DataVoo.Year,
            input.DataVoo.Month,
            input.DataVoo.Day,
            0, 0, 0, 0, DateTimeKind.Unspecified);

        Voo voo;

        if (input.VooId > 0)
            voo = await _vooRepository.GetVooByIdSimple(userSession.CompanyId, input.VooId);
        else
        {
            voo = _vooRepository.GetVooIdByDataVooNumero(userSession.CompanyId, dataVoo, input.NumeroVooXML);
        }

        if (voo == null)
            throw new BusinessException($"Voo # {input.NumeroVooXML} não foi encontrado na data do voo {input.DataVoo.ToString("dd/MM/yyyy")}.");

        var master = await _masterRepository.GetMasterById(userSession.CompanyId, input.MasterId);

        if (master == null)
            throw new BusinessException($"Master não encontrado!");

        if (master.SituacaoRFBId == RFStatusEnvioType.Received)
            throw new BusinessException($"Master não pode ser alterado, pois está em processamento na Receita Federal.");

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);

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

        master.AutenticacaoSignatariaLocal = input.AeroportoOrigemCodigo;

        var ulds = await _uldMasterRepository.GetUldListByMasterNumberVooId(userSession.CompanyId, input.Numero, voo.Id);

        _validadorMaster.InserirErrosMaster(master);
        _masterRepository.UpdateMaster(master);

        foreach (var uld in ulds)
        {
            uld.SummaryDescription = master.DescricaoMercadoria;
            uld.PortOfOrign = master.AeroportoOrigemCodigo;
            uld.PortOfDestiny = master.AeroportoDestinoCodigo;
            if (uld.MasterId == null)
                uld.MasterId = master.Id;
            _uldMasterRepository.UpdateUldMaster(uld);
        }

        if (await _masterRepository.SaveChanges())
        {
            master.ErrosMaster = master.ErrosMaster.Where(x => x.DataExclusao == null).ToList();

            var MasterResponseDto = _mapper.Map<MasterResponseDto>(master);
            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = MasterResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        };

        throw new BusinessException("Não foi possível atualizar Master: Erro Desconhecido!");

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
    public async Task<ApiResponse<List<MasterResponseDto>>> ImportFile(UserSession userSession, MasterFileImportRequest input, Stream stream)
    {
        var fileImport = _masterRepository.GetMasterFileImportById(userSession.CompanyId, input.FileImportId);

        if (fileImport == null)
            throw new BusinessException("Template não definido para importação!");

        var lines = FileService.ReadLines(stream, Encoding.UTF8, fileImport);

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
                if (!line.IsError)
                {
                    var insertMaster = FileService.ReadLineToClass<MasterInsertRequestDto>(line.ParsedFields, fileImport);

                    insertMaster.VooId = input.VooId;
                    insertMaster.IndicadorAwbNaoIata = IsAwbNonIata(insertMaster.Numero);

                    var response = await ProcessInsertMaster(userSession, insertMaster, voo, "Importação Manual");
                    if (responses.Dados == null)
                        responses.Dados = new List<MasterResponseDto>();
                    responses.Dados.Add(response.Dados);
                    continue;
                }
                responses.Notificacoes.Add(new() { Codigo = "ERR1202", Mensagem = $"Erro na lina {fileLine} do arquivo: {line.Error}" });
            }
            catch (Exception ex)
            {
                if (responses.Notificacoes == null)
                    responses.Notificacoes = new List<Notificacao>();

                string message = $"Erro na lina {fileLine} do arquivo: {ex.InnerException?.Message ?? ex.Message}";

                responses.Notificacoes.Add(new() { Codigo = "ERR1201", Mensagem = message });
                _masterRepository.ClearChangeTracker();
            }
            fileLine++;
        }
        return responses;

    }
    public ApiResponse<List<MasterFileResponseDto>> GetFilesToImport(UserSession userSession)
    {
        var data = _masterRepository.GetMasterFileImportList(userSession.CompanyId);

        if (data == null) return default;

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

        DateTime dataLimite = DateTime.Today.AddYears(-1);

        int? masterId = await _masterRepository.GetMasterIdByNumberValidate(voo.CiaAereaId, input.Numero, dataLimite);

        if (masterId > 0)
            throw new BusinessException($"Já existe um master no número {input.Numero} dentro dos últimos 365 dias!");

        var ulds = await _uldMasterRepository.GetUldListByMasterNumberVooId(userSession.CompanyId, input.Numero, voo.Id);

        var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
        var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);

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

        _validadorMaster.InserirErrosMaster(master);

        master.AutenticacaoSignatariaNome = string.IsNullOrEmpty(input.AssinaturaTransportadorNome) ? voo.CompanhiaAereaInfo.Nome : input.AssinaturaTransportadorNome;
        master.AutenticacaoSignatarioData = input.AssinaturaTransportadorData ?? DateTime.Now;
        master.AutenticacaoSignatariaLocal = input.AeroportoOrigemCodigo ?? voo.PortoIataOrigemInfo.Nome;

        _masterRepository.CreateMaster(userSession.CompanyId, master);



        if (await _masterRepository.SaveChanges())
        {
            foreach (var uld in ulds)
            {
                uld.SummaryDescription = master.DescricaoMercadoria;
                uld.PortOfOrign = master.AeroportoOrigemCodigo;
                uld.PortOfDestiny = master.AeroportoDestinoCodigo;
                if (uld.MasterId == null)
                    uld.MasterId = master.Id;
                _uldMasterRepository.UpdateUldMaster(uld);
            }
            _uldMasterRepository.SaveChanges();

            var MasterResponseDto = _mapper.Map<MasterResponseDto>(master);

            return
                new ApiResponse<MasterResponseDto>
                {
                    Dados = MasterResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }

        throw new BusinessException("Não Foi possível adicionar o Master: Erro Desconhecido!");

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

        if (!int.TryParse(e, out int digits))
            return true;

        var digitos7 = Convert.ToInt32(e.Substring(3, 7));
        var digito = Convert.ToInt32(e.Substring(10, 1));
        var digitoesperado = digitos7 % 7;
        if (digito == digitoesperado)
            return false;
        return true;
    }
    #endregion
}

public class FileService
{
    public static List<FileImportLineStatus> ReadLines(Stream stream,
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
    private static List<FileImportLineStatus> ReadCSVFile(string[] lines, FileImport fileImport)
    {
        int line = fileImport.FirstLineTitle ? 1 : 0;
        var result = new List<FileImportLineStatus>();

        for (int i = line; i < lines.Length; i++)
        {
            try
            {
                TextFieldParser parser = new(new StringReader(lines[i]));

                parser.SetDelimiters(fileImport.Configuration1);
                parser.HasFieldsEnclosedInQuotes = Convert.ToBoolean(fileImport.Configuration2);

                string[] fields = null;

                while (!parser.EndOfData)
                {
                    fields = parser.ReadFields();
                }

                parser.Close();

                if (fields.Length != fileImport.Details.ToArray().Length)
                    throw new BusinessException("Formato da linha invalida");

                result.Add(new FileImportLineStatus
                {
                    LineNumber = i,
                    IsError = false,
                    ParsedFields = fields
                });
            }
            catch (Exception ex)
            {
                result.Add(new FileImportLineStatus
                {
                    LineNumber = i,
                    IsError = true,
                    ParsedFields = null,
                    Error = ex.Message
                });
            }
        }

        return result;
    }
    public static T ReadLineToClass<T>(string[] line, FileImport fileImport)
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

public class FileImportLineStatus
{
    public int LineNumber { get; set; }
    public bool IsError { get; set; }
    public string Error { get; set; }
    public string[] ParsedFields { get; set; }
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