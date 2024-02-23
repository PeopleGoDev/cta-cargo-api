using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Application.Validator;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services;

public class SubmeterReceitaService : ISubmeterReceitaService
{
    private readonly ICertitificadoDigitalSupport _certificadoDigitalSupport;
    private readonly IVooRepository _vooRepository;
    private readonly IMasterRepository _masterRepository;
    private readonly IHouseRepository _houseRepository;
    private readonly IMasterHouseAssociacaoRepository _masterHouseAssociacaoRepository;
    private readonly IAutenticaReceitaFederal _autenticaReceitaFederal;
    private readonly IUploadReceitaFederal _uploadReceitaFederal;
    private readonly IValidadorMaster _validadorMaster;
    private readonly IMotorIata _motorIata;
    private readonly IMapper _mapper;

    #region Construtor
    public SubmeterReceitaService(ICertitificadoDigitalSupport certificadoDigitalSupport,
        IVooRepository vooRepository,
        IAutenticaReceitaFederal autenticaReceitaFederal,
        IMasterRepository masterRepository,
        IHouseRepository houseRepository,
        IMasterHouseAssociacaoRepository masterHouseAssociacaoRepository,
        IUploadReceitaFederal flightUploadReceitaFederal,
        IMotorIata motorIata, 
        IValidadorMaster validadorMaster,
        IMapper mapper)
    {
        _certificadoDigitalSupport = certificadoDigitalSupport;
        _vooRepository = vooRepository;
        _autenticaReceitaFederal = autenticaReceitaFederal;
        _masterRepository = masterRepository;
        _houseRepository = houseRepository;
        _uploadReceitaFederal = flightUploadReceitaFederal;
        _motorIata = motorIata;
        _validadorMaster = validadorMaster;
        _masterHouseAssociacaoRepository = masterHouseAssociacaoRepository;
        _mapper = mapper;
    }
    #endregion

    #region Métodos Publicos
    public async Task<ApiResponse<string>> SubmeterVoo(UserSession userSession, FlightUploadRequest input)
    {
        var situacaoRFBvoo = await _vooRepository.GetVooRFBStatus(input.FlightId.Value);

        if (situacaoRFBvoo == null)
            throw new BusinessException("Voo não encontrado!");

        if (situacaoRFBvoo.GhostFlight)
            throw new BusinessException("Upload do voo não permitido. Voo fictício!");

        switch (situacaoRFBvoo.SituacaoRFB)
        {
            case Master.RFStatusEnvioType.Received:

                return await VerificarVooEntregue(userSession, input);

            case Master.RFStatusEnvioType.Processed:

                if (situacaoRFBvoo.Reenviar)
                    return await SubmeterVooInterno(userSession, input, true);

                throw new BusinessException("Voo já foi submetido!");

            case Master.RFStatusEnvioType.NoSubmitted:
            case Master.RFStatusEnvioType.Rejected:

                return await SubmeterVooInterno(userSession, input);

            default:

                throw new BusinessException("Erro na execução da tarefa: Status do vôo não identicado!");
        }
    }
    public async Task<ApiResponse<string>> SubmitFlightSchedule(UserSession userSession, FlightUploadRequest input)
    {
        var situacaoRFBvoo = await _vooRepository.GetVooRFBStatus(input.FlightId.Value);

        switch (situacaoRFBvoo.ScheduleSituationRFB)
        {
            case Master.RFStatusEnvioType.Received:
                return await CheckScheduleFlightProtocol(userSession, input);

            case Master.RFStatusEnvioType.Processed:
                if (situacaoRFBvoo.Reenviar)
                    return await SubmitScheduleFlightInternal(userSession, input, true);

                throw new BusinessException("Voo já foi submetido!");
            case Master.RFStatusEnvioType.NoSubmitted:
            case Master.RFStatusEnvioType.Rejected:
                return await SubmitScheduleFlightInternal(userSession, input);

            default:
                throw new BusinessException("Erro na execução da tarefa: Status do vôo não identicado!");
        }
    }
    public async Task<ApiResponse<string>> SubmeterVooTrecho(UserSession userSession, FlightUploadRequest input)
    {
        if (input.FlightId == null)
            throw new BusinessException("Erro paramêtro: Voo não informado");

        if (input.ItineraryId == null)
            throw new BusinessException("Erro paramêtro: Trecho não informado");

        if (input.DepartureTime == null)
            throw new BusinessException("Erro paramêtro: Data atual de saida não informada");

        Voo voo = await _vooRepository.GetVooWithULDById(userSession.CompanyId, input.FlightId.Value);

        if (voo == null)
            throw new BusinessException("Voo não encontrado !");

        if (voo.SituacaoRFBId == Master.RFStatusEnvioType.Processed)
        {
            bool found = false;
            voo.DataEmissaoXML = voo.DataEmissaoXML.Value.AddMinutes(1);
            voo.Reenviar = false;
            foreach (var trecho in voo.Trechos)
            {
                if (trecho.Id == input.ItineraryId)
                {
                    found = true;
                    trecho.DataHoraSaidaAtual = input.DepartureTime.Value;
                    this._vooRepository.UpdateTrecho(trecho);
                    break;
                }
            }

            if (!found)
                throw new BusinessException("Trecho informado não encontrado");

            var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, voo.CiaAereaId);

            if (certificate.HasError)
                throw new BusinessException(certificate.Error);

            TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

            string xml = _motorIata.GenFlightManifest(voo, input.ItineraryId, input.DepartureTime);

            var response = _uploadReceitaFederal.SubmitFlight(voo.CompanhiaAereaInfo.CNPJ, xml, token, certificate.Certificate);

            return await ProcessarRetornoEnvioArquivoVoo(response, voo);
        }
        else
        {
            throw new BusinessException("Operação não permitida!");
        }
    }
    public async Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterVooMaster(UserSession userSession, FlightUploadRequest input)
    {
        return await EnviarMastersAutomatico(userSession, input);
    }
    public async Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterMasterSelecionado(UserSession userSession, FlightUploadRequest input)
    {

        var masters = await _masterRepository.GetMastersForUploadSelected(userSession.CompanyId, input.idList);

        if (masters == null)
            throw new BusinessException("Master(s) não encontrado!");

        int ciaId = masters[0].VooInfo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        var result = await SubmeterMastersAutomatico(masters, certificate.Certificate, token);

        return new()
        {
            Sucesso = true,
            Dados = result,
            Notificacoes = null
        };

    }
    public async Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterMasterAcao(UserSession userSession, MasterUploadInput input)
    {
        return await EnviarMastersAcao(userSession, input);
    }
    public async Task<ApiResponse<string>> VerificarProtocoloVoo(FlightUploadRequest input)
    {
        try
        {
            Voo voo = await _vooRepository.GetVooById(input.FlightId.Value);
            if (voo == null)
                throw new Exception("Voo não encontrado !");

            string protolo = voo.ProtocoloRFB;
            int ciaId = voo.CiaAereaId;

            X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

            TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

            ProtocoloReceitaCheckFile response = _uploadReceitaFederal.CheckFileProtocol(protolo, token);

            if (await ProcessaRetornoChecagemArquivoVoo(response, voo))
            {
                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Arquivo processado pela Receita Federal com sucesso!",
                    Notificacoes = null
                };
            }
            else
            {
                switch (response.status)
                {
                    case "Rejected":
                        return new ApiResponse<string>()
                        {
                            Sucesso = false,
                            Dados = "O arquivo do voo foi rejeitado pela Receita Federal !",
                            Notificacoes = null
                        };
                    case "Received":
                        return new ApiResponse<string>()
                        {
                            Sucesso = false,
                            Dados = "O arquivo do voo ainda não foi processado, tente novamente mais tarde !",
                            Notificacoes = null
                        };
                    default:
                        return new ApiResponse<string>()
                        {
                            Sucesso = false,
                            Dados = $"O status recebido da Receita Federal inesperado : { response.status } !",
                            Notificacoes = null
                        };
                }

            }

        }
        catch (Exception ex)
        {
            return new ApiResponse<string>()
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = new List<Notificacao>()
                {
                    new Notificacao()
                    {
                        Codigo = "9999",
                        Mensagem = $"Erro na exceção da tarefa: { ex.Message }"
                    }
                }
            };
        }

    }
    public async Task<MasterResponseDto> SubmeterMasterExclusion(UserSession userSession, MasterExclusaoRFBInput input)
    {
        var master = await _masterRepository.GetMasterForUploadById(userSession.CompanyId, input.MasterId);

        if (master is null)
            throw new BusinessException("Master não encontrado!");

        if (master.SituacaoDeletionRFBId == 2)
            throw new BusinessException("Exclusão do Master no Portal Único já confirmado!");

        if (master.SituacaoRFBId is not RFStatusEnvioType.Processed)
            throw new BusinessException("House não está com o status \"Submetido a Receita Federal\"!");

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, master.CiaAereaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        if (master.SituacaoDeletionRFBId == 1)
        {
            var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloDeletionRFB, token);
            await ProcessaRetornoChecagemCancelarMaster(res, master);
        }
        else
        {
            await CancelarMaster(master, certificate.Certificate, token);
        }
        
        return _mapper.Map<MasterResponseDto>(master);
    }
    #endregion

    #region Upload Voo
    private async Task<ApiResponse<string>> SubmeterVooInterno(UserSession userSession, FlightUploadRequest input, bool reenviar = false)
    {
        Voo voo = await _vooRepository.GetVooWithULDById(userSession.CompanyId, input.FlightId.Value);

        if (voo is null)
            throw new BusinessException("Voo não encontrado !");

        VooEntityValidator validator = new VooEntityValidator();

        var resultValidator = validator.Validate(voo);

        if (!resultValidator.IsValid)
            return GeraErrorValidator(resultValidator);

        if (reenviar || voo.ScheduleSituationRFB == RFStatusEnvioType.Processed)
            voo.DataEmissaoXML = voo.DataEmissaoXML.Value.AddMinutes(1);

        voo.Reenviar = false;

        int ciaId = voo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        if(input.DepartureTime is not null)
            voo.DataHoraSaidaReal = input.DepartureTime.Value;

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        string xml = _motorIata.GenFlightManifest(voo);

        var response = _uploadReceitaFederal.SubmitFlight(voo.CompanhiaAereaInfo.CNPJ, xml, token, certificate.Certificate);

        return await ProcessarRetornoEnvioArquivoVoo(response, voo);
        
    }
    private async Task<ApiResponse<string>> SubmitScheduleFlightInternal(UserSession userSession, FlightUploadRequest input, bool reenviar = false)
    {
        Voo voo = await _vooRepository.GetVooWithULDById(userSession.CompanyId, input.FlightId.Value);

        if (voo is null)
            throw new BusinessException("Voo não encontrado !");

        VooEntityValidator validator = new VooEntityValidator();

        var resultValidator = validator.Validate(voo);

        if (!resultValidator.IsValid)
            return GeraErrorValidator(resultValidator);

        if (reenviar)
        {
            voo.DataEmissaoXML = voo.DataEmissaoXML.Value.AddMinutes(1);
            voo.Reenviar = false;
        }

        int ciaId = voo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        string xml = _motorIata.GenFlightManifest(voo,null,null,true);

        var response = _uploadReceitaFederal.SubmitFlight(voo.CompanhiaAereaInfo.CNPJ, xml, token, certificate.Certificate);

        return await ProcessScheduleFlight(response, voo);

    }
    private async Task<ApiResponse<string>> VerificarVooEntregue(UserSession userSession, FlightUploadRequest input)
    {
        var voo = await _vooRepository.GetVooById(input.FlightId.Value);

        int ciaId = voo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        var res = _uploadReceitaFederal.CheckFileProtocol(voo.ProtocoloRFB, token);

        if (await ProcessaRetornoChecagemArquivoVoo(res, voo))
        {
            return new ApiResponse<string>()
            {
                Sucesso = true,
                Dados = "Enviado com sucesso !",
                Notificacoes = null
            };
        }

        var notifications = new List<Notificacao>();
        notifications.Add(new Notificacao { Codigo = "99XE", Mensagem = "Não foi possível verificar voo !" });

        return new ApiResponse<string>()
        {
            Sucesso = false,
            Dados = "Não foi possível verificar voo !",
            Notificacoes = notifications
        };
    }
    private async Task<ApiResponse<string>> CheckScheduleFlightProtocol(UserSession userSession, FlightUploadRequest input)
    {
        var voo = await _vooRepository.GetVooById(input.FlightId.Value);

        int ciaId = voo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        var res = _uploadReceitaFederal.CheckFileProtocol(voo.ProtocoloScheduleRFB, token);

        if (await ProcessCheckScheduleFlight(res, voo))
            return new ApiResponse<string>()
            {
                Sucesso = true,
                Dados = "Enviado com sucesso !",
                Notificacoes = null
            };

        var notifications = new List<Notificacao>();
        notifications.Add(new Notificacao { Codigo = "99XE", Mensagem = "Não foi possível verificar voo !" });

        return new ApiResponse<string>()
        {
            Sucesso = false,
            Dados = "Não foi possível verificar voo !",
            Notificacoes = notifications
        };
    }
    #endregion

    #region Upload Master
    private async Task<ApiResponse<IEnumerable<FileUploadResponse>>> EnviarMastersAcao(UserSession userSession, MasterUploadInput input)
    {
        var masters = await _masterRepository.GetMastersForUploadById(userSession.CompanyId, input.MasterId);

        if (masters == null)
            throw new Exception("Masters não encontrado !");

        int ciaId = masters[0].VooInfo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        var result = await SubmeterMastersAcao(masters, certificate.Certificate, token, input.PurposeCode);

        return new ()
        {
            Sucesso = true,
            Dados = result,
            Notificacoes = null
        };
    }
    private async Task<IEnumerable<FileUploadResponse>> SubmeterMastersAcao(List<Master> masters, X509Certificate2 certificado, TokenResponse token, IataXmlPurposeCode purposeCode)
    {
        List<FileUploadResponse> result = new List<FileUploadResponse>();

        foreach (Master master in masters)
        {
            try
            {
                string xml = "";

                if (master.SituacaoRFBId == Master.RFStatusEnvioType.Received)
                {
                    var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloRFB, token);
                    var responseChecagem = await ProcessaRetornoChecagemArquivoMaster(res, master);
                    result.Add(responseChecagem);
                    continue;
                }

                switch (purposeCode)
                {
                    case IataXmlPurposeCode.Creation:
                        xml = _motorIata.GenMasterManifest(master, purposeCode);
                        break;
                    case IataXmlPurposeCode.Update:
                        xml = _motorIata.GenMasterManifest(master, purposeCode);
                        break;
                    case IataXmlPurposeCode.Deletion:
                        xml = _motorIata.GenMasterManifest(master, purposeCode);
                        break;
                }
                var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);
                var responseSubmit = await ProcessarRetornoEnvioArquivoMaster(response, master);
                result.Add(responseSubmit);
                continue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        };
        return result;
    }
    private async Task<ApiResponse<IEnumerable<FileUploadResponse>>> EnviarMastersAutomatico(UserSession userSession, FlightUploadRequest input)
    {
        var masters = await _masterRepository.GetMastersForUploadByVooId(userSession.CompanyId, input.FlightId.Value);

        if (masters == null)
            throw new BusinessException("Não foi possivel selecionar Masters durante o upload!");

        int ciaId = masters[0].VooInfo.CiaAereaId;

        var certificate = await _certificadoDigitalSupport.GetCertificateForAirCompany(userSession.UserId, ciaId);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate);

        var result = await SubmeterMastersAutomatico(masters, certificate.Certificate, token);

        return new ()
        {
            Sucesso = true,
            Dados = result,
            Notificacoes = null
        };
    }
    private async Task<IEnumerable<FileUploadResponse>> SubmeterMastersAutomatico(List<Master> masters, X509Certificate2 certificado, TokenResponse token)
    {
        List<FileUploadResponse> respList = new List<FileUploadResponse>();

        foreach (Master master in masters)
        {
            try
            {
                if (master.SituacaoRFBId == Master.RFStatusEnvioType.Received)
                {
                    var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloRFB, token);
                    var responseStatus = await ProcessaRetornoChecagemArquivoMaster(res, master);
                    respList.Add(responseStatus);
                    continue;
                }
                if(master.Reenviar)
                {
                    string xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Update);
                    var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);
                    var responseStatus = await ProcessarRetornoEnvioArquivoMaster(response, master);

                    respList.Add(responseStatus);
                    continue;
                }
                if(master.SituacaoRFBId == Master.RFStatusEnvioType.NoSubmitted || master.SituacaoRFBId == Master.RFStatusEnvioType.Rejected)
                {
                    string xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Creation);
                    var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);
                    var responseStatus = await ProcessarRetornoEnvioArquivoMaster(response, master);
                    respList.Add(responseStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        };
        return respList;
    }
    private async Task<FileUploadResponse> ProcessarRetornoEnvioArquivoMaster(ReceitaRetornoProtocol response, Master master)
    {
        FileUploadResponse resp = new FileUploadResponse
        {
            Id = master.Id,
            Protocol = master.ProtocoloRFB,
            Status = response.StatusCode
        };
        master.StatusCodeRFB = response.StatusCode;

        switch (response.StatusCode)
        {
            case ("Received"):
                master.SituacaoRFBId = Master.RFStatusEnvioType.Received;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.ProtocoloRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                break;
            case "Rejected":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                master.DescricaoErroRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                resp.Status = response.StatusCode;
                _validadorMaster.InserirErrosMaster(master);
                break;
            case "Processed":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Processed;
                master.Reenviar = false;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.Reenviar = false;
                master.DataChecagemDeletionRFB = null;
                master.CodigoErroDeletionRFB = null;
                master.DataProtocoloDeletionRFB = null;
                master.DescricaoErroDeletionRFB = null;
                master.ProtocoloDeletionRFB = null;
                master.SituacaoDeletionRFBId = 0;
                master.DataProtocoloRFB = response.IssueDateTime;
                master.DataChecagemRFB = response.IssueDateTime;
                break;
            default:
                break;
        }
        _masterRepository.UpdateMaster(master);
        await _masterRepository.SaveChanges();
        return resp;
    }
    private async Task<FileUploadResponse> ProcessaRetornoChecagemArquivoMaster(ProtocoloReceitaCheckFile response, Master master)
    {
        FileUploadResponse resp = new FileUploadResponse
        {
            Id = master.Id,
            Protocol = master.ProtocoloRFB,
            Status = response.status
        };

        switch (response.status)
        {
            case "Rejected":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                _validadorMaster.InserirErrosMaster(master);
                resp.ErrorCode = response.errorList[0].code;
                resp.Message = response.errorList[0].description;
                if (response.errorList.Length > 0)
                {
                    master.CodigoErroRFB = response.errorList[0].code;
                    master.DescricaoErroRFB = response.errorList[0].description;
                    master.DataChecagemRFB = response.dateTime;
                }
                _masterRepository.UpdateMaster(master);
                await _masterRepository.SaveChanges();
                break;
            case "Processed":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Processed;
                master.Reenviar = false;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.Reenviar = false;
                master.DataChecagemDeletionRFB = null;
                master.CodigoErroDeletionRFB = null;
                master.DataProtocoloDeletionRFB = null;
                master.DescricaoErroDeletionRFB = null;
                master.ProtocoloDeletionRFB = null;
                master.SituacaoDeletionRFBId = 0;
                master.DataChecagemRFB = response.dateTime;
                _masterRepository.UpdateMaster(master);
                await _masterRepository.SaveChanges();
                break;
            case "Received":
                break;
            default:
                break;
        }

        return resp;
    }
    #endregion

    #region Métodos Privados
    private ApiResponse<string> GeraErrorValidator(ValidationResult resultValidator)
    {
        ApiResponse<string> apiResponseError = new ApiResponse<string>()
        {
            Sucesso = false,
            Dados = null,
            Notificacoes = new List<Notificacao>()
        };
        foreach (var item in resultValidator.Errors)
        {
            apiResponseError.Notificacoes.Add(new Notificacao()
            {
                Codigo = "9999",
                Mensagem = item.ErrorMessage
            });
        }

        return apiResponseError;
    }
    #endregion

    #region Verificação Arquivo Voo
    private async Task<ApiResponse<string>> ProcessarRetornoEnvioArquivoVoo(ReceitaRetornoProtocol response, Voo voo)
    {
        switch (response.StatusCode)
        {
            case ("Received"):
                voo.SituacaoRFBId = RFStatusEnvioType.Received;
                voo.StatusId = 2;
                voo.CodigoErroRFB = null;
                voo.DescricaoErroRFB = null;
                voo.ProtocoloRFB = response.Reason;
                voo.DataProtocoloRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Dados Recebidos pela Receita Federal com Sucesso! Submeta o voo novamente para verificar o status de processamento",
                    Notificacoes = null
                };
            case "Rejected":
                voo.StatusId = 1;
                voo.SituacaoRFBId = RFStatusEnvioType.Rejected;
                voo.DescricaoErroRFB = response.Reason;
                voo.DataProtocoloRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = false,
                    Dados = $"Erro ao submeter voo: ${response.Reason}",
                    Notificacoes = null
                };
            case "Processed":
                voo.StatusId = 2;
                voo.SituacaoRFBId = RFStatusEnvioType.Processed;
                voo.CodigoErroRFB = null;
                voo.DescricaoErroRFB = null;
                voo.DataProtocoloRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Voo processamento pela Receita Federal com sucesso!",
                    Notificacoes = null
                };
            default:
                return new ApiResponse<string>()
                {
                    Sucesso = false,
                    Dados = $"Não foi possível identifcar o status da requisição: ${response?.Reason}",
                    Notificacoes = null
                };
        }
    }
    private async Task<ApiResponse<string>> ProcessScheduleFlight(ReceitaRetornoProtocol response, Voo voo)
    {
        switch (response.StatusCode)
        {
            case ("Received"):
                voo.ScheduleSituationRFB = RFStatusEnvioType.Received;
                voo.ScheduleErrorCodeRFB = null;
                voo.ScheduleErrorDescriptionRFB = null;
                voo.ProtocoloScheduleRFB = response.Reason;
                voo.ScheduleProtocolTimeRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Dados Recebidos pela Receita Federal com Sucesso! Submeta o voo novamente para verificar o status de processamento",
                    Notificacoes = null
                };
            case "Rejected":
                voo.ScheduleSituationRFB = RFStatusEnvioType.Rejected;
                voo.ScheduleErrorDescriptionRFB = response.Reason;
                voo.ScheduleProtocolTimeRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = false,
                    Dados = $"Erro ao submeter voo: ${response.Reason}",
                    Notificacoes = null
                };
            case "Processed":
                voo.ScheduleSituationRFB = RFStatusEnvioType.Processed;
                voo.ScheduleErrorCodeRFB = null;
                voo.ScheduleErrorDescriptionRFB = null;
                voo.ScheduleProtocolTimeRFB = response.IssueDateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Voo processamento pela Receita Federal com sucesso!",
                    Notificacoes = null
                };
            default:
                return new ApiResponse<string>()
                {
                    Sucesso = false,
                    Dados = $"Não foi possível identifcar o status da requisição: ${response?.Reason}",
                    Notificacoes = null
                };
        }
    }
    private async Task<bool> ProcessaRetornoChecagemArquivoVoo(ProtocoloReceitaCheckFile response, Voo voo)
    {
        switch (response.status)
        {
            case "Rejected":
                voo.SituacaoRFBId = RFStatusEnvioType.Rejected;
                if (response.errorList.Length > 0)
                {
                    voo.CodigoErroRFB = response.errorList[0].code;
                    voo.DescricaoErroRFB = response.errorList[0].description;
                    voo.DataChecagemRFB = response.dateTime;
                }
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return false;
            case "Processed":
                voo.SituacaoRFBId = RFStatusEnvioType.Processed;
                voo.DataChecagemRFB = response.dateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return true;
            case "Received":
                return false;
            default:
                return false;
        }
    }
    private async Task<bool> ProcessCheckScheduleFlight(ProtocoloReceitaCheckFile response, Voo voo)
    {
        switch (response.status)
        {
            case "Rejected":
                voo.ScheduleSituationRFB = RFStatusEnvioType.Rejected;
                if (response.errorList.Length > 0)
                {
                    voo.ScheduleErrorCodeRFB = response.errorList[0].code;
                    voo.ScheduleErrorDescriptionRFB = response.errorList[0].description;
                    voo.ScheduleCheckTimeRFB = response.dateTime;
                }
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return false;
            case "Processed":
                voo.ScheduleSituationRFB = RFStatusEnvioType.Processed;
                voo.ScheduleCheckTimeRFB = response.dateTime;
                _vooRepository.UpdateVoo(voo);
                await _vooRepository.SaveChanges();
                return true;
            case "Received":
                return false;
            default:
                return false;
        }
    }
    #endregion

    #region Master Cancelar
    private async Task<ApiResponse<string>> CancelarMaster(
        Master master,
        X509Certificate2 certificado,
        TokenResponse token)
    {
        var xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Deletion);
        var response = _uploadReceitaFederal.SubmitWaybill(master.CiaAereaInfo.CNPJ, xml, token, certificado);
        await ProcessaRetornoEnvioCancelarMaster(response, master);

        if (response.StatusCode == "Rejected")
            throw new BusinessException(response.Reason);

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Enviado com sucesso !",
            Notificacoes = null
        };
    }
    private async Task ProcessaRetornoChecagemCancelarMaster(
        ProtocoloReceitaCheckFile response,
        Master master)
    {
        switch (response.status)
        {
            case "Rejected":
                master.SituacaoDeletionRFBId = 3;
                if (response.errorList.Length > 0)
                {
                    master.CodigoErroDeletionRFB = response.errorList[0].code;
                    master.DescricaoErroDeletionRFB = string.Join("\n", response.errorList.Select(x => x.description));
                    master.DataChecagemDeletionRFB = response.dateTime;
                }
                _masterRepository.UpdateMaster(master);
                break;
            case "Processed":
                master.SituacaoDeletionRFBId = 2;
                master.DataChecagemDeletionRFB = response.dateTime;
                master.DescricaoErroDeletionRFB = null;
                master.SituacaoRFBId = 0;
                master.DataChecagemRFB = null;
                master.CodigoErroRFB = null;
                master.DataProcessadoRFB = null;
                master.ProtocoloRFB = null;
                _masterRepository.UpdateMaster(master);
                break;
            default:
                break;
        }

        await _masterRepository.SaveChanges();
    }
    private async Task ProcessaRetornoEnvioCancelarMaster(
        ReceitaRetornoProtocol response,
        Master master)
    {
        switch (response.StatusCode)
        {
            case "Received":
                master.SituacaoDeletionRFBId = 1;
                master.CodigoErroDeletionRFB = null;
                master.DescricaoErroDeletionRFB = null;
                master.ProtocoloDeletionRFB = response.Reason;
                master.DataProtocoloDeletionRFB = response.IssueDateTime;
                _masterRepository.UpdateMaster(master);
                break;
            case "Rejected":
                master.SituacaoDeletionRFBId = 3;
                master.CodigoErroDeletionRFB = response.StatusCode;
                master.DescricaoErroDeletionRFB = response.Reason;
                master.DataProtocoloDeletionRFB = response.IssueDateTime;
                _masterRepository.UpdateMaster(master);
                break;
            case "Processed":
                master.SituacaoDeletionRFBId = 2;
                master.DataChecagemDeletionRFB = response.IssueDateTime;
                master.DescricaoErroDeletionRFB = null;
                master.SituacaoRFBId = 0;
                master.DataChecagemRFB = null;
                master.CodigoErroRFB = null;
                master.DataProcessadoRFB = null;
                master.ProtocoloRFB = null;
                _masterRepository.UpdateMaster(master);
                break;
        }

        await _masterRepository.SaveChanges();
    }
    #endregion
}
