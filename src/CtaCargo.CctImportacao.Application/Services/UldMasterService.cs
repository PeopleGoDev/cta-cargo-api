using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Services
{
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
        public async Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(int uldId)
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
        public async Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(int masterId)
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
        public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(int vooId)
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
        public async Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumarioPorVooId(ListaUldMasterRequest input)
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
        public async Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(ListaUldMasterRequest input)
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
        public async Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(UserSession userSession, List<UldMasterInsertRequest> input)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(input[0].VooId);

                if (voo == null)
                    throw new Exception("Voo não encontrado!");

                if (voo.SituacaoRFBId == RFStatusEnvioType.Received)
                    throw new Exception("Voo processando na Receita Federal não pode ser alterado!");

                if (voo.SituacaoRFBId == RFStatusEnvioType.Processed && !voo.Reenviar)
                    throw new Exception("Voo processado pela Receita Federal não pode ser alterado!");

                List<UldMaster> listaModel = new List<UldMaster>();

                foreach (var item in input)
                {
                    var uld = _mapper.Map<UldMaster>(item);
                    UldMasterEntityValidator validator = new UldMasterEntityValidator();
                    var master = await GetMasterId(userSession.CompanyId, item.MasterNumero);
                    uld.MasterId = master.Id;
                    uld.TotalParcial = master.TotalParcial;
                    var result = validator.Validate(uld);

                    if (!result.IsValid)
                        throw new Exception($"Não foi possível inserir a ULD {item.UldId}: {result.Errors[0].ErrorMessage}");

                    uld.CreatedDateTimeUtc = DateTime.UtcNow;
                    listaModel.Add(uld);
                }

                if (await _uldMasterRepository.CreateUldMasterList(listaModel) == 0)
                    throw new Exception("Não foi possivel inserir ULDs");

                foreach (var item in listaModel)
                {
                    var master = await _masterRepository.GetMasterByNumber(userSession.CompanyId, item.MasterNumero);
                    _validadorMaster.TratarErrosMaster(master);
                    _masterRepository.UpdateMaster(master);
                    await _masterRepository.SaveChanges();
                }

                var masterResponseDto = _mapper.Map<List<UldMasterResponseDto>>(listaModel);

                return
                    new ApiResponse<List<UldMasterResponseDto>>
                    {
                        Dados = masterResponseDto,
                        Sucesso = true,
                        Notificacoes = null
                    };
            }
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
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
                                    Mensagem = $"Não Foi possível inserir a ULD: {ex.Message} !"
                                }
                            }
                        };
            }
        }
        public async Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(UserSession userSession, List<UldMasterUpdateRequest> input)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(input[0].VooId);

                if (voo == null)
                    throw new Exception("Voo não encontrado!");

                if (voo.Reenviar == false && (voo.SituacaoRFBId == RFStatusEnvioType.Received))
                    throw new Exception("Voo processando na Receita Federal não pode ser alterado!");

                if (voo.Reenviar == false && voo.SituacaoRFBId == RFStatusEnvioType.Processed)
                    throw new Exception("Voo processado pela Receita Federal não pode ser alterado!");

                List<UldMasterResponseDto> uldsDto = new List<UldMasterResponseDto>();

                foreach (var item in input)
                {
                    var uld = await _uldMasterRepository.GetUldMasterById(item.Id);

                    if (uld == null)
                    {
                        return new ApiResponse<List<UldMasterResponseDto>>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualiza a ULD: ULD { item.UldCaracteristicaCodigo + item.UldId + item.UldIdPrimario } não encontrado !"
                                }
                            }
                        };
                    }

                    _mapper.Map(item, uld);

                    var master = await GetMasterId(userSession.CompanyId, item.MasterNumero);

                    uld.MasterId = master.Id;
                    uld.TotalParcial = master.TotalParcial;
                    uld.ModifiedDateTimeUtc = DateTime.UtcNow;

                    UldMasterEntityValidator validator = new UldMasterEntityValidator();

                    var result = validator.Validate(uld);

                    if (result.IsValid)
                    {
                        if (await _uldMasterRepository.UpdateUldMaster(uld) > 0)
                        {
                            var masterResponseDto = _mapper.Map<UldMasterResponseDto>(uld);

                            uldsDto.Add(masterResponseDto);
                        }
                        else
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
                                        Mensagem = "Não foi possível atualiza o Master: Erro Desconhecido!"
                                    }
                                    }
                                };
                        }
                    }
                }
                return
                    new ApiResponse<List<UldMasterResponseDto>>
                    {
                        Dados = uldsDto,
                        Sucesso = true,
                        Notificacoes = null
                    };
            }
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
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
                                    Mensagem = $"Não foi possível atualiza o Master: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<string>> ExcluirUldMaster(UserSession userSession, UldMasterDeleteByIdInput input)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(input.VooId);

                if (voo == null)
                    throw new Exception("Voo não encontrado!");

                if (voo.SituacaoRFBId == RFStatusEnvioType.Received)
                    throw new Exception("Voo em processamento na Receita Federal não pode ser alterado!");

                if (voo.SituacaoRFBId == RFStatusEnvioType.Processed)
                    throw new Exception("Voo processado pela Receita Federal não pode ser alterado!");

                var uldMasters = await _uldMasterRepository.GetUldMasterByIdList(input.ListaIds);
                var masterRepo = await _uldMasterRepository.DeleteUldMasterList(uldMasters);

                await AtualizarValidacaoMaster(userSession.CompanyId, uldMasters);

                if (masterRepo == 0)
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
                                    Mensagem = "Não foi possível excluir ULD(s): ULD(s) não encontrado(s) !"
                                }
                            }
                        };
                }

                return
                    new ApiResponse<string>
                    {
                        Dados = "ULD(s) excluidos com sucesso!",
                        Sucesso = true,
                        Notificacoes = null
                    };

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
                                    Mensagem = $"Não foi possível excluir ULD(s): {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<string>> ExcluirUld(UserSession userSession, UldMasterDeleteByTagInput input)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(input.VooId);

                if (voo == null)
                    throw new Exception("Voo não encontrado!");
                
                if(voo.SituacaoRFBId == RFStatusEnvioType.Received)
                    throw new Exception("Voo submetido a Receita Federal não pode ser alterado!");

                if (voo.SituacaoRFBId == RFStatusEnvioType.Processed)
                    throw new Exception("Voo processado pela Receita Federal não pode ser alterado!");

                var uldMasters = await _uldMasterRepository.GetUldMasterByTag(input);

                var masterRepo = await _uldMasterRepository.DeleteUldMasterList(uldMasters);

                await AtualizarValidacaoMaster(userSession.CompanyId, uldMasters);

                if (masterRepo == 0)
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
                                    Mensagem = "Não foi possível excluir ULD(s): ULD(s) não encontrado(s) !"
                                }
                            }
                        };
                }

                return
                    new ApiResponse<string>
                    {
                        Dados = "ULD(s) excluidos com sucesso!",
                        Sucesso = true,
                        Notificacoes = null
                    };

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
                            Mensagem = $"Não foi possível excluir ULD(s): {ex.Message} !"
                        }
                    }
                };
            }

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
        private async Task<Master> GetMasterId(int companyId, string masterNumero)
        {
            var result = await _masterRepository.GetMasterByNumber(companyId, masterNumero);
            return result;
        }
        private async Task AtualizarValidacaoMaster(int companyId, List<UldMaster> uldMasters)
        {
            var grupo = uldMasters.GroupBy(x => new { x.VooId, x.MasterNumero });

            foreach (var uld in grupo)
            {
                var master = await _masterRepository.GetMasterIdByNumber(companyId, uld.Key.VooId, uld.Key.MasterNumero);
                if (master != null)
                {
                    _validadorMaster.TratarErrosMaster(master);
                    _masterRepository.UpdateMaster(master);
                    await _masterRepository.SaveChanges();
                }
            }
        }
        #endregion
    }
}
