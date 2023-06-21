using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
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
            try
            {
                var lista = await _vooRepository.GetVooById(vooId);
                if (lista == null)
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
                var dto = _mapper.Map<VooResponseDto>(lista);
                return
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
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
                                    Mensagem = $"Erro na aplicação! {ex.Message} !"
                                }
                            }
                        };
            }

        }

        public async Task<ApiResponse<VooUploadResponse>> VooUploadPorId(int vooId, UserSession userSessionInfo)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(vooId);
                if (voo == null)
                {
                    return
                        new ApiResponse<VooUploadResponse>
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

                var ulds = await _uldMasterRepository.GetUldMasterByVooId(vooId);

                var response = new VooUploadResponse
                {
                    AeroportoDestinoCodigo = voo.AeroportoDestinoCodigo,
                    AeroportoOrigemCodigo = voo.AeroportoOrigemCodigo,
                    DataCriacao = voo.CreatedDateTimeUtc,
                    DataHoraChegadaEstimada = voo.DataHoraChegadaEstimada,
                    DataHoraChegadaReal = voo.DataHoraChegadaReal,
                    DataHoraSaidaEstimada = voo.DataHoraSaidaEstimada,
                    DataHoraSaidaReal = voo.DataHoraSaidaReal,
                    DataProtocoloRFB = voo.DataProtocoloRFB,
                    DataVoo = voo.DataVoo,
                    ErroCodigoRFB = voo.CodigoErroRFB,
                    ErroDescricaoRFB = voo.DescricaoErroRFB,
                    Numero = voo.Numero,
                    PesoBruto = voo.TotalPesoBruto,
                    PesoBrutoUnidade = voo.TotalPesoBrutoUnidade,
                    ProtocoloRFB = voo.ProtocoloRFB,
                    Reenviar = voo.Reenviar,
                    SituacaoRFBId = (int)voo.SituacaoRFBId,
                    StatusId = (int)voo.StatusId,
                    TotalPacotes = voo.TotalPacotes,
                    TotalPecas = voo.TotalPecas,
                    UsuarioCriacao = voo.UsuarioCriacaoInfo?.Nome,
                    Volume = voo.TotalVolumeBruto,
                    VolumeUnidade = voo.TotalVolumeBrutoUnidade,
                    VooId = voo.Id,
                    ULDs = ulds
                };

                return
                        new ApiResponse<VooUploadResponse>
                        {
                            Dados = response,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<VooUploadResponse>
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
        public async Task<ApiResponse<IEnumerable<VooResponseDto>>> ListarVoos(VooListarInputDto input, UserSession userSessionInfo)
        {
            try
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

                var dto = _mapper.Map<IEnumerable<VooResponseDto>>(lista);

                return
                        new ApiResponse<IEnumerable<VooResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<VooResponseDto>>
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
        public async Task<ApiResponse<IEnumerable<VooListaResponseDto>>> ListarVoosLista(VooListarInputDto input, UserSession userSessionInfo)
        {
            try
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

                var dto = _mapper.Map<IEnumerable<VooListaResponseDto>>(lista);

                return
                        new ApiResponse<IEnumerable<VooListaResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<VooListaResponseDto>>
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
        public async Task<ApiResponse<VooResponseDto>> InserirVoo(VooInsertRequestDto input, UserSession userSessionInfo)
        {
            try
            {
                if (ValidarNumeroVoo(input.Numero) == false)
                    throw new Exception("Número do voo invalido!");

                CiaAerea cia = await _ciaAereaRepository.GetCiaAereaByIataCode(userSessionInfo.CompanyId, input.Numero.Substring(0, 2));

                if (cia == null)
                    throw new Exception($"Companhia Aérea { input.Numero.Substring(0, 2) } não cadastrada!");

                var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoOrigemCodigo);
                var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(input.AeroportoDestinoCodigo);

                input.DataVoo = new DateTime(
                    input.DataVoo.Year,
                    input.DataVoo.Month,
                    input.DataVoo.Day,
                    0, 0, 0, 0);

                var voo = _mapper.Map<Voo>(input);

                voo.EmpresaId = userSessionInfo.CompanyId;
                voo.CiaAereaId = cia.Id;
                voo.CreatedDateTimeUtc = DateTime.UtcNow;
                voo.DataEmissaoXML = DateTime.UtcNow;
                voo.CiaAereaId = cia.Id;
                voo.PortoIataOrigemId = null;
                voo.PortoIataDestinoId = null;
                
                if (codigoOrigemId > 0)
                    voo.PortoIataOrigemId = codigoOrigemId;

                if (codigoDestinoId > 0)
                    voo.PortoIataDestinoId = codigoDestinoId;

                VooEntityValidator validator = new VooEntityValidator();

                var result = validator.Validate(voo);
                voo.StatusId = result.IsValid ? 1 : 0;

                _vooRepository.CreateVoo(voo);

                if (await _vooRepository.SaveChanges())
                {
                    var VooResponseDto = _mapper.Map<VooResponseDto>(voo);
                    return
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = VooResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não Foi possível adicionar o voo: Erro Desconhecido!"
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
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não Foi possível adicionar o voo: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<VooResponseDto>> AtualizarVoo(VooUpdateRequestDto vooRequest, UserSession userSessionInfo)
        {
            try
            {
                var voo = await _vooRepository.GetVooById(vooRequest.VooId);

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
                                    Mensagem = "Não foi possível atualiza o voo: Voo não encontrado !"
                                }
                            }
                        };
                }

                var codigoOrigemId = await _portoIATARepository.GetPortoIATAIdByCodigo(vooRequest.AeroportoOrigemCodigo);

                var codigoDestinoId = await _portoIATARepository.GetPortoIATAIdByCodigo(vooRequest.AeroportoDestinoCodigo);

                _mapper.Map(vooRequest, voo);

                voo.ModifiedDateTimeUtc = DateTime.UtcNow;

                voo.DataEmissaoXML = voo.DataEmissaoXML ?? DateTime.UtcNow;

                voo.PortoIataOrigemId = null;

                voo.PortoIataDestinoId = null;

                if (codigoOrigemId > 0)
                    voo.PortoIataOrigemId = codigoOrigemId;

                if (codigoDestinoId > 0)
                    voo.PortoIataDestinoId = codigoDestinoId;

                VooEntityValidator validator = new VooEntityValidator();

                var result = validator.Validate(voo);

                voo.StatusId = result.IsValid ? 1 : 0;

                _vooRepository.UpdateVoo(voo);

                if (await _vooRepository.SaveChanges())
                {
                    var VooResponseDto = _mapper.Map<VooResponseDto>(voo);
                    return
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = VooResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
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
                                    Mensagem = "Não foi possível atualiza o voo: Erro Desconhecido!"
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
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualiza o voo: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<VooResponseDto>> AtualizarReenviarVoo(int vooId, UserSession userSessionInfo)
        {
            try
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

                voo.Reenviar = true;

                VooEntityValidator validator = new VooEntityValidator();

                var result = validator.Validate(voo);

                voo.StatusId = result.IsValid ? 1 : 0;

                _vooRepository.UpdateVoo(voo);

                if (await _vooRepository.SaveChanges())
                {
                    var VooResponseDto = _mapper.Map<VooResponseDto>(voo);
                    return
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = VooResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
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
                                    Mensagem = "Não foi possível atualiza o voo: Erro Desconhecido!"
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
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualiza o voo: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<VooResponseDto>> ExcluirVoo(int vooId, UserSession userSessionInfo)
        {
            try
            {
                var vooRepo = await _vooRepository.GetVooById(vooId);
                if (vooRepo == null)
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
                                    Mensagem = "Não foi possível excluir voo: Voo não encontrado !"
                                }
                            }
                        };
                }

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
                                    Mensagem = "Não foi possível excluir voo: Erro Desconhecido!"
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
                        new ApiResponse<VooResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível excluir voo: {ex.Message} !"
                                }
                            }
                        };
            }

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
}
