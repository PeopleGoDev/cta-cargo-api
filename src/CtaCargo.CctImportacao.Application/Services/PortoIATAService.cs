using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class PortoIATAService : IPortoIATAService
    {
        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;

        private readonly IPortoIATARepository _portoIATARepository;
        private readonly IMapper _mapper;
        public PortoIATAService(IPortoIATARepository portoIATARepository, IMapper mapper)
        {
            _portoIATARepository = portoIATARepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<PortoIATAResponseDto>> PortoIATAPorId(int portoIATAId)
        {
            try
            {
                var lista = await _portoIATARepository.GetPortoIATAById(portoIATAId);
                if (lista == null)
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Porto IATA não encontrado !"
                                }
                            }
                        };
                }
                var dto = _mapper.Map<PortoIATAResponseDto>(lista);
                return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Erro na aplicação: {ex.Message}"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<IEnumerable<PortoIATAResponseDto>>> ListarPortosIATA(int empresaId)
        {
            try
            {
                var lista = await _portoIATARepository.GetAllPortosIATA(empresaId); ;
                var dto = _mapper.Map<IEnumerable<PortoIATAResponseDto>>(lista);
                return
                        new ApiResponse<IEnumerable<PortoIATAResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<PortoIATAResponseDto>>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Erro na aplicação: {ex.Message}"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<PortoIATAResponseDto>> InserirPortoIATA(PortoIATAInsertRequestDto portoIATARequest)
        {
            try
            {
                var portoIATAModel = _mapper.Map<PortoIata>(portoIATARequest);
                portoIATAModel.CreatedDateTimeUtc = DateTime.UtcNow;

                _portoIATARepository.CreatePortoIATA(portoIATAModel);

                if (await _portoIATARepository.SaveChanges())
                {
                    var PortoIATAResponseDto = _mapper.Map<PortoIATAResponseDto>(portoIATAModel);
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = PortoIATAResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não Foi possível adicionar Porto IATA: Erro Desconhecido!"
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
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não Foi possível adicionar Porto IATA: {ex.Message}"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<PortoIATAResponseDto>> AtualizarPortoIATA(PortoIATAUpdateRequestDto portoIATARequest)
        {
            try
            {
                var portoIATARepo = await _portoIATARepository.GetPortoIATAById(portoIATARequest.PortoIATAId);
                if (portoIATARepo == null)
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar Porto IATA: Porto IATA não encontrado!"
                                }
                            }
                        };
                }

                _mapper.Map(portoIATARequest, portoIATARepo);
                portoIATARepo.ModifiedDateTimeUtc = DateTime.UtcNow;
                _portoIATARepository.UpdatePortoIATA(portoIATARepo);

                if (await _portoIATARepository.SaveChanges())
                {
                    var PortoIATAResponseDto = _mapper.Map<PortoIATAResponseDto>(portoIATARepo);
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = PortoIATAResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar Porto IATA: Erro Desconhecido!"
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
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualizar Porto IATA: {ex.Message}"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<PortoIATAResponseDto>> ExcluirPortoIATA(int portoIATAId)
        {
            try
            {
                var portoIATARepo = await _portoIATARepository.GetPortoIATAById(portoIATAId);
                if (portoIATARepo == null)
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir Porto IATA: Porto IATA não encontrado!"
                                }
                            }
                        };
                }

                _portoIATARepository.DeletePortoIATA(portoIATARepo);

                if (await _portoIATARepository.SaveChanges())
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir Porto IATA: Erro Desconhecido!"
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
                        new ApiResponse<PortoIATAResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível excluir Porto IATA: {ex.Message}"
                                }
                            }
                        };
            }

        }
        private ApiResponse<PortoIATAResponseDto> ErrorHandling(Exception exception)
        {
            var sqlEx = exception?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                {
                    //We have an error we can process
                    return new ApiResponse<PortoIATAResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"Já existe um porto IATA cadastrado com o mesmo código!"
                                }
                        }
                    };
                }
                else
                {
                    return new ApiResponse<PortoIATAResponseDto>
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
                return new ApiResponse<PortoIATAResponseDto>
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
    }
}
