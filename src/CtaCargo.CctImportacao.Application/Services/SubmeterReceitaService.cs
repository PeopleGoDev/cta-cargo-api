using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services
{
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

        #region Construtor
        public SubmeterReceitaService(ICertitificadoDigitalSupport certificadoDigitalSupport,
            IVooRepository vooRepository,
            IAutenticaReceitaFederal autenticaReceitaFederal,
            IMasterRepository masterRepository,
            IHouseRepository houseRepository,
            IMasterHouseAssociacaoRepository masterHouseAssociacaoRepository,
            IUploadReceitaFederal flightUploadReceitaFederal,
            IMotorIata motorIata, IValidadorMaster validadorMaster = null)
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
        }
        #endregion

        #region Métodos Publicos
        public async Task<ApiResponse<string>> SubmeterVoo(UserSession userSession, VooUploadInput input)
        {
            try
            {
                var situacaoRFBvoo = await _vooRepository.GetVooRFBStatus(input.VooId);

                if(situacaoRFBvoo == null)
                    throw new Exception("Voo não encontrado !");

                if (situacaoRFBvoo.Reenviar)
                {
                    return await SubmeterVooInterno(userSession, input, true);
                }
                else
                {
                    switch (situacaoRFBvoo.SituacaoRFB)
                    {
                        case Master.RFStatusEnvioType.Received:
                            return await VerificarVooEntregue(userSession, input);
                        case Master.RFStatusEnvioType.Processed:
                            return new ApiResponse<string>()
                            {
                                Sucesso = true,
                                Dados = "Enviado com sucesso !",
                                Notificacoes = null
                            };
                            // return await EnviarMastersAutomatico(userSession, input);
                        case Master.RFStatusEnvioType.NoSubmitted:
                        case Master.RFStatusEnvioType.Rejected:
                            return await SubmeterVooInterno(userSession, input);
                        default:
                            return new ApiResponse<string>()
                            {
                                Sucesso = false,
                                Dados = null,
                                Notificacoes = new List<Notificacao>() {
                                    new Notificacao()
                                    {
                                        Codigo = "9999",
                                        Mensagem = $"Erro na execução da tarefa: Status do vôo não identicado !"
                                    }
                                }
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
                            Mensagem = $"Erro na execução da tarefa: { ex.Message }!"
                        }
                    }
                };
            }

        }
        public async Task<ApiResponse<string>> SubmeterVooMaster(UserSession userSession, VooUploadInput input)
        {
            try
            {
                return await EnviarMastersAutomatico(userSession, input);
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
                            Mensagem = $"Erro na exceção da tarefa: { ex.Message }!"
                        }
                    }
                };
            }

        }
        public async Task<ApiResponse<string>> SubmeterMasterAcao(UserSession userSession, MasterUploadInput input)
        {
            try
            {
                return await EnviarMastersAcao(userSession, input);
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
                            Mensagem = $"Erro na exceção da tarefa: { ex.Message }!"
                        }
                    }
                };
            }

        }
        public async Task<ApiResponse<string>> VerificarProtocoloVoo(VooUploadInput input)
        {
            try
            {
                Voo voo = await _vooRepository.GetVooById(input.VooId);
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
        public async Task<ApiResponse<string>> SubmeterMasterExclusion(UserSession userSession, MasterExclusaoRFBInput input)
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

                List<Notificacao> res1 = await ProcessaRetornoChecagemArquivoMaster(res, master);

                if (res1 != null && res1.Count > 0)
                    return new ApiResponse<string>()
                    {
                        Sucesso = false,
                        Dados = null,
                        Notificacoes = res1
                    };

                return new ApiResponse<string>()
                {
                    Sucesso = true,
                    Dados = "Enviado com sucesso !",
                    Notificacoes = null
                };
            }

            var result = await SubmeterMastersDeletion(master, certificado, token);

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
        public async Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(UserSession userSession, SubmeterRFBMasterHouseRequest input)
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

                await SubmeterAssociacaoHouseMasterList(input.Masters, houses, certificado, token);

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
        #endregion

        #region Upload Voo
        private async Task<ApiResponse<string>> SubmeterVooInterno(UserSession userSession, VooUploadInput input, bool reenviar = false)
        {
            Voo voo = await _vooRepository.GetVooWithULDById(userSession.CompanyId, input.VooId);

            if (voo == null)
                throw new Exception("Voo não encontrado !");

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
        private async Task<ApiResponse<string>> VerificarVooEntregue(UserSession userSession, VooUploadInput input)
        {
            var voo = await _vooRepository.GetVooById(input.VooId);

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
        private async Task<ApiResponse<string>> EnviarMastersAcao(UserSession userSession, MasterUploadInput input)
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
        private async Task<List<Notificacao>> SubmeterMastersAcao(List<Master> masters, X509Certificate2 certificado, TokenResponse token, IataXmlPurposeCode purposeCode)
        {

            List<Notificacao> notificacoes = new List<Notificacao>();

            foreach (Master master in masters)
            {
                try
                {
                    string xml = "";

                    if (master.SituacaoRFBId == Master.RFStatusEnvioType.Received)
                    {
                        var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloRFB, token);

                        var listaErros = await ProcessaRetornoChecagemArquivoMaster(res, master);

                        if (listaErros != null)
                            notificacoes.AddRange(listaErros);

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

                    bool processa = await ProcessarRetornoEnvioArquivoMaster(response, master);

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
        private async Task<List<Notificacao>> SubmeterMastersDeletion(Master master, X509Certificate2 certificado, TokenResponse token)
        {

            List<Notificacao> notificacoes = new List<Notificacao>();

            try
            {

                var xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Deletion);
  
                var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);

                bool processa = await ProcessarRetornoEnvioMasterExclusion(response, master);

                if (!processa)
                {
                    if (response.StatusCode == "Rejected")
                        notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = response.Reason });
                }

            }
            catch (Exception ex)
            {
                notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = ex.Message });
            }

            return notificacoes;
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

        private async Task<ApiResponse<string>> EnviarMastersAutomatico(UserSession userSession, VooUploadInput input)
        {
            var masters = await _masterRepository.GetMastersForUploadByVooId(userSession.CompanyId, input.VooId);

            if (masters == null)
                throw new Exception("Não foi possivel selecionar Masters durante o upload!");

            int ciaId = masters[0].VooInfo.CiaAereaId;

            X509Certificate2 certificado = await _certificadoDigitalSupport.GetCertificateCiaAereaAsync(ciaId);

            if (certificado == null)
                certificado = await _certificadoDigitalSupport.GetCertificateUsuarioAsync(userSession.UserId);

            if (certificado == null)
                throw new Exception("Certificado digital da companhia aérea/usuário não cadastrado !");

            TokenResponse token = _autenticaReceitaFederal.GetTokenAuthetication(certificado);

            var result = await SubmeterMastersAutomatico(masters, certificado, token);

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
        private async Task<List<Notificacao>> SubmeterMastersAutomatico(List<Master> masters, X509Certificate2 certificado, TokenResponse token)
        {
            List<Notificacao> notificacoes = new List<Notificacao>();

            foreach (Master master in masters)
            {
                try
                {
                    if (master.SituacaoRFBId == Master.RFStatusEnvioType.Received)
                    {
                        var res = _uploadReceitaFederal.CheckFileProtocol(master.ProtocoloRFB, token);

                        var listaErros = await ProcessaRetornoChecagemArquivoMaster(res, master);

                        if (listaErros != null)
                            notificacoes.AddRange(listaErros);

                        continue;
                    }
                    if(master.Reenviar)
                    {
                        string xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Update);

                        var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);

                        bool processa = await ProcessarRetornoEnvioArquivoMaster(response, master);
                        if (!processa)
                        {
                            if (response.StatusCode == "Rejected")
                                notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = response.Reason });
                        }

                        continue;
                    }
                    if(master.SituacaoRFBId == Master.RFStatusEnvioType.NoSubmitted || master.SituacaoRFBId == Master.RFStatusEnvioType.Rejected)
                    {
                        string xml = _motorIata.GenMasterManifest(master, IataXmlPurposeCode.Creation);

                        var response = _uploadReceitaFederal.SubmitWaybill(master.VooInfo.CompanhiaAereaInfo.CNPJ, xml, token, certificado);

                        bool processa = await ProcessarRetornoEnvioArquivoMaster(response, master);
                        if (!processa)
                        {
                            if (response.StatusCode == "Rejected")
                                notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = response.Reason });
                        }
                    }
                }
                catch (Exception ex)
                {
                    notificacoes.Add(new Notificacao { Codigo = "9999", Mensagem = ex.Message });
                }
            };
            return notificacoes;
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

        #region Upload Associação House x Master
        private async Task SubmeterAssociacaoHouseMasterList(List<SubmeterRFBMasterHouseItemRequest> Masters, List<House> houses, X509Certificate2 certificado, TokenResponse token)
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
                    await SubmeterHouseMasterAssociacao(freightFowarderCnpj, masterInfo, houseList, token, certificado);
                    houseList = new List<House>();
                    houseList.Add(house);
                    curMaster = house.MasterNumeroXML;
                    masterInfo = Masters.FirstOrDefault(x => x.MasterNumber == curMaster);
                }
            }
            await SubmeterHouseMasterAssociacao(freightFowarderCnpj, masterInfo, houseList, token, certificado);
        }
        private async Task SubmeterHouseMasterAssociacao(string FreightFowarderTaxId, SubmeterRFBMasterHouseItemRequest masterInfo, List<House> houses, TokenResponse token, X509Certificate2 certificado)
        {
            var associacao = await _masterHouseAssociacaoRepository
                .SelectMasterHouseAssociacaoByMaster(masterInfo.MasterNumber);

            var operation = IataXmlPurposeCode.Creation;

            if (associacao != null) {

                if (associacao.SituacaoAssociacaoRFBId == 1)
                {
                    var res = _uploadReceitaFederal.CheckFileProtocol(associacao.ProtocoloAssociacaoRFB, token);
                    await ProcessaRetornoChecagemArquivoHouseMaster(res, houses);
                    return;
                }

                if (!CheckUploadAvailability(houses))
                    return;

                operation = IataXmlPurposeCode.Update;
            }

            string xmlAssociacao = _motorIata
                .GenMasterHouseManifest(masterInfo, houses, operation);

            var responseAssociacao = _uploadReceitaFederal.SubmitHouseMaster(FreightFowarderTaxId, xmlAssociacao, token, certificado);

            await ProcessarRetornoEnvioArquivoHouseMaster(responseAssociacao, houses);
            
            return;
        }
        private bool CheckUploadAvailability(List<House> houses)
        {
            if(houses.Count == 0) return false;
            return true;
        }
        private async Task ProcessarRetornoEnvioArquivoHouseMaster(ReceitaRetornoProtocol response, List<House> houses)
        {
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
        private async Task ProcessaRetornoChecagemArquivoHouseMaster(ProtocoloReceitaCheckFile response, List<House> houses)
        {
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
            await _houseRepository.SaveChanges();
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
        private async Task<List<Notificacao>> ProcessaRetornoChecagemArquivoMaster(ProtocoloReceitaCheckFile response, Master master)
        {
            List<Notificacao> notificacoes = new List<Notificacao>();
            switch (response.status)
            {
                case "Rejected":
                    master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                    _validadorMaster.TratarErrosMaster(master);
                    if (response.errorList.Length > 0)
                    {
                        master.CodigoErroRFB = response.errorList[0].code;
                        master.DescricaoErroRFB = response.errorList[0].description;
                        master.DataChecagemRFB = response.dateTime;
                        foreach (ErrorListCheckFileRFB item in response.errorList)
                        {
                            notificacoes.Add(new Notificacao { Codigo = item.code, Mensagem = item.description });
                        }
                    }
                    _masterRepository.UpdateMaster(master);
                    await _masterRepository.SaveChanges();
                    return notificacoes;
                case "Processed":
                    master.SituacaoRFBId = Master.RFStatusEnvioType.Processed;
                    master.Reenviar = false;
                    master.CodigoErroRFB = null;
                    master.DescricaoErroRFB = null;
                    master.ProtocoloRFB = response.protocolNumber;
                    master.DataChecagemRFB = response.dateTime;
                    _masterRepository.UpdateMaster(master);
                    await _masterRepository.SaveChanges();
                    return null;
                case "Received":
                    return null;
                default:
                    return null;
            }
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
        private async Task<bool> ProcessarRetornoEnvioArquivoMaster(ReceitaRetornoProtocol response, Master master)
        {
            switch (response.StatusCode)
            {
                case ("Received"):
                    master.SituacaoRFBId = Master.RFStatusEnvioType.Received;
                    master.StatusId = 2;
                    master.Reenviar = false;
                    master.CodigoErroRFB = null;
                    master.DescricaoErroRFB = null;
                    master.ProtocoloRFB = response.Reason;
                    master.DataProtocoloRFB = response.IssueDateTime;
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();
                    return false;
                case "Rejected":
                    master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                    master.DescricaoErroRFB = response.Reason;
                    master.DataProtocoloRFB = response.IssueDateTime;
                    _validadorMaster.TratarErrosMaster(master);
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();
                    return false;
                case "Processed":
                    master.SituacaoRFBId = Master.RFStatusEnvioType.Processed;
                    master.Reenviar = false;
                    master.CodigoErroRFB = null;
                    master.DescricaoErroRFB = null;
                    master.DataProtocoloRFB = response.IssueDateTime;
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();
                    return true;
                default:
                    return false;
            }
        }
        private async Task<bool> ProcessarRetornoEnvioMasterExclusion(ReceitaRetornoProtocol response, Master master)
        {

            MasterEntityValidator validator = new MasterEntityValidator();

            switch (response.StatusCode)
            {
                case ("Received"):

                    master.SituacaoRFBId = Master.RFStatusEnvioType.ReceivedDeletion;
                    master.CodigoErroRFB = null;
                    master.DescricaoErroRFB = null;
                    master.ProtocoloRFB = response.Reason;
                    master.DataProtocoloRFB = response.IssueDateTime;
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();

                    return false;

                case "Rejected":

                    master.SituacaoRFBId = Master.RFStatusEnvioType.Rejected;
                    master.DescricaoErroRFB = response.Reason;
                    master.DataProtocoloRFB = response.IssueDateTime;
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();

                    return false;

                case "Processed":

                    var resultProcessed = validator.Validate(master);
                    master.StatusId = resultProcessed.IsValid ? 1 : 0;
                    master.SituacaoRFBId = 0;
                    master.CodigoErroRFB = null;
                    master.DescricaoErroRFB = null;
                    master.DataProtocoloRFB = null;
                    _masterRepository.UpdateMaster(master);
                    await _vooRepository.SaveChanges();

                    return true;

                default:
                    return false;
            }
        }
        #endregion
    }
}