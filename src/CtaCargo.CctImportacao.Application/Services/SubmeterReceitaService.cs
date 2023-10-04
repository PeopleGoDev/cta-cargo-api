using AutoMapper;
using Azure;
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
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
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
        IMotorIata motorIata, IValidadorMaster validadorMaster = null, IMapper mapper = null)
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

            X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(voo.CiaAereaId);

            if (certificado == null)
                certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

            if (certificado == null)
                throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

            TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

            string xml = _motorIata.GenFlightManifest(voo, input.ItineraryId, input.DepartureTime);

            var response = _uploadReceitaFederal.SubmitFlight(voo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);

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

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            throw new BusinessException("Certificado digital não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

        var result = await SubmeterMastersAutomatico(masters, certificado, token);

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
    public async Task<ApiResponse<FileUploadResponse>> SubmeterMasterExclusion(UserSession userSession, MasterExclusaoRFBInput input)
    {
        var master = await _masterRepository.GetMasterForUploadById(userSession.CompanyId, input.MasterId);

        if (master == null)
            throw new Exception("Master não encontrado !");

        if (master.SituacaoRFBId == Master.RFStatusEnvioType.Received)
            throw new Exception("O master está em processamento na Receita Federal. Veriticar status do master antes de submeter 'Exclusion'.");

        int ciaId = master.VooInfo.CiaAereaId;

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(input.UsuarioId);

        if (certificado == null)
            throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

        if (master.SituacaoRFBId ==  Master.RFStatusEnvioType.ReceivedDeletion)
        {
            // Master submetido para exclusão, verificar status apenas
            var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloRFB, token);

            var res1 = await ProcessaRetornoChecagemArquivoMaster(res, master);

            return new()
            {
                Sucesso = true,
                Dados = res1,
                Notificacoes = null
            };
        }

        var result = await SubmeterMastersDeletion(master, certificado, token);

        return new()
        {
            Sucesso = true,
            Dados = result,
            Notificacoes = null
        };
    }
    public async Task<ApiResponse<string>> SubmeterHousesAgentesDeCarga(SubmeterRFBHouseRequest input)
    {
        try
        {
            return await EnviarHousesAutomatico(input.DataProcessamento, input.AgenteDeCargaId);
        }
        catch(Exception ex)
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
                        Mensagem = $"Erro na execução da tarefa: { ex.Message }!"
                    }
                }
            };
        }
    }
    public async Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(UserSession userSession, 
        SubmeterRFBMasterHouseRequest input)
    {
        var masterNumbers = input.Masters.Select(x => x.MasterNumber).ToArray();

        QueryJunction<House> param = new QueryJunction<House>();
        param.Add(x => masterNumbers.Contains(x.MasterNumeroXML));
        param.Add(x => x.AgenteDeCargaId == input.FreightFowarderId);
        param.Add(x => x.DataExclusao == null);

        try
        {
            var houses = _houseRepository.GetHouseForUploading(param);

            if (houses == null)
                throw new Exception("Não há houses a serem enviados !");

            if (houses.Count() == 0)
                throw new Exception("Nenhum house selecionado !");

            X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateAgenteDeCargaAsync(input.FreightFowarderId);

            TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado, "AGECARGA");

            await SubmeterAssociacaoHouseMasterList(userSession, input.Masters, houses, certificado, token);

            return new ApiResponse<string>()
            {
                Sucesso = true,
                Dados = "Dados submetidos com sucesso!",
                Notificacoes = null
            };
        }
        catch (Exception ex)
        {
            var notificacoes = new List<Notificacao>();
            notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = ex.Message });

            return new ApiResponse<string>()
            {
                Sucesso = false,
                Dados = $"Erro ao submeter arquivo: {ex.Message}",
                Notificacoes = notificacoes
            };
        }
    }
    public async Task<ApiResponse<string>> SubmeterAssociation(UserSession userSession, int associationId)
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

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateAgenteDeCargaAsync(agenteId.Value);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado, "AGECARGA");

        if (association.SituacaoDeletionAssociacaoRFBId == 1)
        {
            var res = _uploadReceitaFederal.CheckFileProtocol(association.ProtocoloDeletionAssociacaoRFB, token);
            await ProcessaRetornoChecagemAssociacaoHouseMaster(res, association, houses);
        }
        else
        {
            await CancelarAssociacaoHouseMasterList(association, userSession, masterInfo, houses, certificado, token);
        }
        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Dados submetidos com sucesso!",
            Notificacoes = null
        };

    }
    public async Task<ApiResponse<HouseResponseDto>> SubmeterHouseExclusion(UserSession userSession, int houseId)
    {
        var house = await _houseRepository.GetHouseByIdForExclusionUpload(userSession.CompanyId, houseId);

        if (house == null)
            throw new BusinessException("House não encontrado ou indisponível!");

        if (house.AgenteDeCargaId == null)
            throw new BusinessException("House não associado ao um agente de carga!");
        
        if (house.SituacaoRFBId == 0)
            throw new BusinessException("House não submetido a RFB. Não é possível submeter o cancelamento");

        if (house.SituacaoRFBId == 1)
            throw new BusinessException("House está submetido, mas não processado. Submeta o house novamente para confirmar processamento");

        if (house.SituacaoRFBId == 3)
            throw new BusinessException("House com erro na tentativa se criação na RFB");
        ;
        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateAgenteDeCargaAsync(house.AgenteDeCargaId.Value);

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado, "AGECARGA");

        if (house.SituacaoDeletionRFBId == 1)
        {
            var res = _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloDeletionRFB, token);
            await ProcessaRetornoChecagemCancelarHouse(res, house);
        }
        else
        {
            await CancelarHouse(house, certificado, token);
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

    #region Upload Voo
    private async Task<ApiResponse<string>> SubmeterVooInterno(UserSession userSession, FlightUploadRequest input, bool reenviar = false)
    {
        Voo voo = await _vooRepository.GetVooWithULDById(userSession.CompanyId, input.FlightId.Value);

        if (voo == null)
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

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

        if (certificado == null)
            throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

        string xml = _motorIata.GenFlightManifest(voo);

        var response = _uploadReceitaFederal.SubmitFlight(voo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);

        return await ProcessarRetornoEnvioArquivoVoo(response, voo);
        
    }
    private async Task<ApiResponse<string>> VerificarVooEntregue(UserSession userSession, FlightUploadRequest input)
    {
        var voo = await _vooRepository.GetVooById(input.FlightId.Value);

        int ciaId = voo.CiaAereaId;

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

        if (certificado == null)
            throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

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
    #endregion

    #region Upload Master
    #region Upload Master Individual
    private async Task<ApiResponse<IEnumerable<FileUploadResponse>>> EnviarMastersAcao(UserSession userSession, MasterUploadInput input)
    {
        var masters = await _masterRepository.GetMastersForUploadById(userSession.CompanyId, input.MasterId);

        if (masters == null)
            throw new Exception("Masters não encontrado !");

        int ciaId = masters[0].VooInfo.CiaAereaId;

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(input.UsuarioId);

        if (certificado == null)
            throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

        var result = await SubmeterMastersAcao(masters, certificado, token, input.PurposeCode);

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
    private async Task<FileUploadResponse> SubmeterMastersDeletion(Master master, X509Certificate2 certificado, TokenResponse token)
    {
        var xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Deletion);
        var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);
        return await ProcessarRetornoEnvioMasterExclusion(response, master);
    }
    private async Task<List<Notificacao>> SubmeterHouseAcao(List<House> houses, X509Certificate2 certificado, TokenResponse token, IataXmlPurposeCode purposeCode)
    {

        List<Notificacao> notificacoes = new List<Notificacao>();

        foreach (House house in houses)
        {
            try
            {
                string xml = "";

                if (house.SituacaoRFBId == 1)
                {
                    var res = _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloRFB, token);

                    var listaErros = await ProcessaRetornoChecagemArquivoHouse(res, house);

                    if (listaErros != null)
                        notificacoes.AddRange(listaErros);

                    continue;
                }

                switch (purposeCode)
                {
                    case IataXmlPurposeCode.Creation:
                        xml = _motorIata.GenHouseManifest(house, purposeCode);
                        break;
                    case IataXmlPurposeCode.Update:
                        xml = _motorIata.GenHouseManifest(house, purposeCode);
                        break;
                    case IataXmlPurposeCode.Deletion:
                        xml = _motorIata.GenHouseManifest(house, purposeCode);
                        break;
                }

                var response = _uploadReceitaFederal.SubmitHouse(house.AgenteDeCargaInfo.CNPJ, xml, token, certificado);

                bool processa = await ProcessarRetornoEnvioArquivoHouse(response, house);

                if (!processa)
                {
                    if (response.StatusCode == "Rejected")
                        notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = response.Reason });
                }

                continue;

            }
            catch (Exception ex)
            {
                notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = ex.Message });
            }
        };
        return notificacoes;
    }
    #endregion
    private async Task<ApiResponse<IEnumerable<FileUploadResponse>>> EnviarMastersAutomatico(UserSession userSession, FlightUploadRequest input)
    {
        var masters = await _masterRepository.GetMastersForUploadByVooId(userSession.CompanyId, input.FlightId.Value);

        if (masters == null)
            throw new BusinessException("Não foi possivel selecionar Masters durante o upload!");

        int ciaId = masters[0].VooInfo.CiaAereaId;

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

        if (certificado == null)
            certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

        if (certificado == null)
            throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

        var result = await SubmeterMastersAutomatico(masters, certificado, token);

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
    #endregion

    #region Upload House
    private async Task<ApiResponse<string>> EnviarHousesAutomatico(DateTime dataProcessamento, int agenteDeCargaId)
    {
        var processDate = new DateTime(dataProcessamento.Year, dataProcessamento.Month, dataProcessamento.Day, 0, 0, 0, 0);
        QueryJunction<House> param = new QueryJunction<House>();
        param.Add(x => x.DataProcessamento == processDate);
        param.Add(x => x.AgenteDeCargaId == agenteDeCargaId);
        param.Add(x => x.DataExclusao == null);

        var houses = _houseRepository.GetHouseForUploading(param);

        if (houses == null)
            throw new Exception("Não foi possivel selecionar Houses para o upload!");

        if (houses.Count() == 0)
            throw new Exception("Nenhum house selecionado !");

        X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateAgenteDeCargaAsync(agenteDeCargaId);

        if (certificado == null)
            throw new Exception("Certificado digital do agente de carga não cadastrado !");

        TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado, "AGECARGA");

        var result = await  SubmeterHousesAutomatico(houses, certificado, token);

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
    private async Task<List<Notificacao>> SubmeterHousesAutomatico(IEnumerable<House> houses, X509Certificate2 certificado, TokenResponse token)
    {
        List<Notificacao> notificacoes = new List<Notificacao>();

        try
        {
            foreach (House house in houses)
            {
                switch (house.SituacaoRFBId)
                {
                    case 1:
                        var res = _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloRFB, token);

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
                            xml = _motorIata.GenHouseManifest(house, IataXmlPurposeCode.Update);
                        else
                            xml = _motorIata.GenHouseManifest(house, IataXmlPurposeCode.Creation);

                        var response = _uploadReceitaFederal.SubmitHouse(house.AgenteDeCargaInfo.CNPJ, xml, token, certificado);

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
        }
        catch (Exception ex)
        {
            notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = ex.Message });
        }
        return notificacoes;
    }
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
                await _vooRepository.SaveChanges();
                return false;
            case "Rejected":
                house.SituacaoRFBId = 3;
                house.DescricaoErroRFB = response.Reason;
                house.DataProtocoloRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                await _vooRepository.SaveChanges();
                return false;
            case "Processed":
                house.SituacaoRFBId = 2;
                house.CodigoErroRFB = null;
                house.DescricaoErroRFB = null;
                house.DataProtocoloRFB = response.IssueDateTime;
                _houseRepository.UpdateHouse(house);
                await _vooRepository.SaveChanges();
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
    private async Task<ApiResponse<string>> CancelarHouse(House house, X509Certificate2 certificado, TokenResponse token)
    {
        if (house.SituacaoRFBId == 2)
        {
            switch (house.SituacaoDeletionRFBId)
            {
                case 0: // Submeter Exclusion
                case 3:
                    var xml = _motorIata.GenHouseManifest(house, IataXmlPurposeCode.Deletion);
                    var response = _uploadReceitaFederal.SubmitHouse(house.AgenteDeCargaInfo.CNPJ, xml, token, certificado);
                    await ProcessaRetornoEnvioCancelarHouse(response, house);

                    if (response.StatusCode == "Rejected")
                        throw new BusinessException(response.Reason);

                    break;
                case 1: // Checar Exclusion
                    var res = _uploadReceitaFederal.CheckFileProtocol(house.ProtocoloDeletionRFB, token);
                    await ProcessaRetornoChecagemCancelarHouse(res, house);
                    break;
            }
        }

        return new ApiResponse<string>()
        {
            Sucesso = true,
            Dados = "Enviado com sucesso !",
            Notificacoes = null
        };
    }

    private async Task ProcessaRetornoChecagemCancelarHouse(ProtocoloReceitaCheckFile response,
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

    private async Task ProcessaRetornoEnvioCancelarHouse(ReceitaRetornoProtocol response,
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
        houses.OrderBy(x => x.MasterNumeroXML);
        string curMaster = houses.FirstOrDefault().MasterNumeroXML;
        var masterInfo = Masters.FirstOrDefault(x => x.MasterNumber == curMaster);
        string freightFowarderCnpj = houses.FirstOrDefault().AgenteDeCargaInfo.CNPJ;

        List<House> houseList = new List<House>();
        foreach (House house in houses.OrderBy(x => x.MasterNumeroXML))
        {
            if(house.MasterNumeroXML == curMaster)
            {
                houseList.Add(house);
                continue;
            }

            if(house.MasterNumeroXML != curMaster)
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
                var res = _uploadReceitaFederal.CheckFileProtocol(associacao.ProtocoloAssociacaoRFB, token);
                await ProcessaRetornoChecagemArquivoHouseMaster(res, associacao, houses);
                return;
            }

            if (!CheckUploadAvailability(houses))
                return;

            if(associacao.SituacaoAssociacaoRFBId == 2)
                operation = IataXmlPurposeCode.Update;
        }

        string xmlAssociacao = _motorIata
            .GenMasterHouseManifest(masterInfo, houses, operation, associacao.CreatedDateTimeUtc);

        var responseAssociacao = _uploadReceitaFederal
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

        string xmlAssociacao = _motorIata
            .GenMasterHouseManifest(masterInfo, houses, operation, associacao.CreatedDateTimeUtc);

        var responseAssociacao = _uploadReceitaFederal
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
        await _vooRepository.SaveChanges();
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
                if(associacao.Id == 0)
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
                associacao.CodigoErroDeletionAssociacaoRFB = response.StatusCode ;
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
        await _vooRepository.SaveChanges();
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
                master.ProtocoloRFB = response.protocolNumber;
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
    private async Task<FileUploadResponse> ProcessarRetornoEnvioArquivoMaster(ReceitaRetornoProtocol response, Master master)
    {
        FileUploadResponse resp = new FileUploadResponse
        {
            Id = master.Id,
            Protocol = master.ProtocoloRFB,
            Status = response.StatusCode
        };
        switch (response.StatusCode)
        {
            case ("Received"):
                master.SituacaoRFBId = Master.RFStatusEnvioType.Received;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.ProtocoloRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            case "Rejected":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                master.DescricaoErroRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                resp.Status = response.StatusCode;
                _validadorMaster.InserirErrosMaster(master);
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            case "Processed":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Processed;
                master.Reenviar = false;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.DataProtocoloRFB = response.IssueDateTime;
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            default:
                break;
        }
        return resp;
    }
    private async Task<FileUploadResponse> ProcessarRetornoEnvioMasterExclusion(ReceitaRetornoProtocol response, Master master)
    {
        var result = new FileUploadResponse
        {
            Id = master.Id,
            Status = response.Reason
        };

        MasterEntityValidator validator = new MasterEntityValidator();

        switch (response.StatusCode)
        {
            case "Received":
                master.SituacaoRFBId = Master.RFStatusEnvioType.ReceivedDeletion;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.ProtocoloRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                result.Protocol = response.Reason;
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            case "Rejected":
                master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                master.DescricaoErroRFB = response.Reason;
                master.DataProtocoloRFB = response.IssueDateTime;
                result.Message = response.Reason;
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            case "Processed":
                var resultProcessed = validator.Validate(master);
                master.StatusId = resultProcessed.IsValid ? 1 : 0;
                master.SituacaoRFBId = 0;
                master.CodigoErroRFB = null;
                master.DescricaoErroRFB = null;
                master.DataProtocoloRFB = null;
                result.Protocol = response.Reason;
                _masterRepository.UpdateMaster(master);
                await _vooRepository.SaveChanges();
                break;
            default:
                break;
        }
        return result;
    }
    #endregion
}