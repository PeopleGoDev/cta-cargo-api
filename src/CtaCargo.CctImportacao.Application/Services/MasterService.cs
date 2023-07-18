using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services
{
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
            try
            {
                DateTime dataLimite = DateTime.Today.AddYears(-1);

                DateTime dataVoo = new DateTime(input.DataVoo.Year,
                    input.DataVoo.Month,
                    input.DataVoo.Day,
                    0, 0, 0, 0);

                Voo voo = await _vooRepository.GetVooIdByDataVooNumero(dataVoo, input.NumeroVooXML);

                if (voo == null)
                    throw new Exception($"Voo # { input.NumeroVooXML } não foi encontrado na data do voo { input.DataVoo.ToString("dd/MM/yyyy") }.");

                if(voo.SituacaoRFBId == RFStatusEnvioType.Received)
                    throw new Exception($"O Voo # { voo.Numero } não pode ser alterado, pois está em processamento na Receita Federal. Faça a verificação do Voo para atualizar o Status.");

                if(voo.SituacaoRFBId == RFStatusEnvioType.Processed)
                    throw new Exception($"O Voo # { voo.Numero } não pode ser alterado, pois foi submetido a RFB.");

                int masterId = await _masterRepository.GetMasterIdByNumberValidate(voo.CiaAereaId, input.Numero, dataLimite);

                if (masterId > 0)
                    throw new Exception("Master já existe em um periodo menor de 365 dias!");

                var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);

                var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);

                int? codigoNaturezaCargaId = 0;

                if( input.NaturezaCarga != null && input.NaturezaCarga.Length ==3)
                    codigoNaturezaCargaId = await _naturezaCargaRepository.GetNaturezaCargaIdByCodigo(input.NaturezaCarga);

                var master = _mapper.Map<Master>(input);

                master.EmpresaId = userSession.CompanyId;
                master.CiaAereaId = voo.CiaAereaId;
                master.CreatedDateTimeUtc = DateTime.UtcNow;
                master.VooId = voo.Id;
                master.VooNumeroXML = input.NumeroVooXML;
                master.AeroportoOrigemId = null;

                if(codigoOrigemId > 0)
                    master.AeroportoOrigemId = codigoOrigemId;

                master.AeroportoDestinoId = null;

                if (codigoDestinoId > 0)
                    master.AeroportoDestinoId = codigoDestinoId;

                master.NaturezaCargaId = null;

                if (codigoNaturezaCargaId > 0)
                    master.NaturezaCargaId = codigoNaturezaCargaId;

                _validadorMaster.TratarErrosMaster(master);

                master.AutenticacaoSignatariaNome = "1234567890";
                master.AutenticacaoSignatarioData = DateTime.Now;
                master.AutenticacaoSignatariaLocal = input.AeroportoOrigemCodigo;

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
                            Sucesso = true,
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
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
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
                                    Mensagem = $"Não Foi possível adicionar o Master: {ex.Message}"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<MasterResponseDto>> AtualizarMaster(UserSession userSession, MasterUpdateRequestDto input)
        {
            try
            {
                DateTime dataVoo = new DateTime(input.DataVoo.Year,
                    input.DataVoo.Month,
                    input.DataVoo.Day,
                    0, 0, 0, 0);

                Voo voo = await _vooRepository.GetVooIdByDataVooNumero(dataVoo, input.NumeroVooXML);

                if (voo == null)
                    throw new Exception($"Voo # { input.NumeroVooXML } não foi encontrado na data do voo { input.DataVoo.ToString("dd/MM/yyyy") }.");

                var master = await _masterRepository.GetMasterById(userSession.CompanyId, input.MasterId);

                if (master == null)
                    throw new Exception($"Master não encontrado!");

                if (master.SituacaoRFBId == RFStatusEnvioType.Received)
                    throw new Exception($"Master não pode ser alterado, pois está em processamento na Receita Federal.");

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

                _validadorMaster.TratarErrosMaster(master);
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
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
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
                                    Mensagem = $"Não foi possível atualiza o Master: {ex.Message} !"
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
                        _validadorMaster.TratarErrosMaster(master);
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
        
        #region Metodos Privado
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
        #endregion
    }
}
