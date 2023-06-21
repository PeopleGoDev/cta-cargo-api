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
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class AgenteDeCargaService : IAgenteDeCargaService
    {
        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;

        private readonly IAgenteDeCargaRepository _agenteDeCargaRepository;
        private readonly IMapper _mapper;
        public AgenteDeCargaService(IAgenteDeCargaRepository agenteDeCargaRepository, IMapper mapper)
        {
            _agenteDeCargaRepository = agenteDeCargaRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> PegarAgenteDeCargaPorId(int agenteId)
        {
            try
            {
                var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(agenteId);

                if (agenteDeCarga == null)
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Agente de Carga não encontrado !"
                                }
                            }
                        };
                }
                var dto = _mapper.Map<AgenteDeCargaResponseDto>(agenteDeCarga);
                return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<AgenteDeCargaResponseDto>
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
        public async Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga(int empresaId)
        {
            try
            {
                var lista = await _agenteDeCargaRepository.GetAllAgenteDeCarga(empresaId);

                var dto = _mapper.Map<IEnumerable<AgenteDeCargaResponseDto>>(lista);

                return
                        new ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>
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
        public async Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgenteDeCargaSimples(int empresaId)
        {
            try
            {
                var lista = await _agenteDeCargaRepository.GetAllAgenteDeCarga(empresaId);

                var dto = from c in lista
                          select new AgenteDeCargaListaSimplesResponse()
                          {
                              Nome = c.Nome,
                              Numero = c.Numero,
                              AgenteDeCargaId = c.Id
                          };

                return
                        new ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>
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
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga(AgenteDeCargaInsertRequest agenteDeCargaRequest)
        {
            try
            {
                var agenteDeCarga = _mapper.Map<AgenteDeCarga>(agenteDeCargaRequest);

                agenteDeCarga.CreatedDateTimeUtc = DateTime.UtcNow;

                agenteDeCarga.CriadoPeloId = agenteDeCargaRequest.UsuarioId;

                _agenteDeCargaRepository.CreateAgenteDeCarga(agenteDeCarga);

                if (await _agenteDeCargaRepository.SaveChanges())
                {
                    var ciaAereaResponseDTO = _mapper.Map<AgenteDeCargaResponseDto>(agenteDeCarga);
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = ciaAereaResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar o agente de carga !"
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
                        new ApiResponse<AgenteDeCargaResponseDto>
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
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga(AgenteDeCargaUpdateRequest agenteDeCargaRequest)
        {
            try
            {
                var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(agenteDeCargaRequest.AgenteDeCargaId);

                if (agenteDeCarga == null)
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Agente de Carga não encontrado !"
                                }
                            }
                        };
                }

                _mapper.Map(agenteDeCargaRequest, agenteDeCarga);

                agenteDeCarga.ModifiedDateTimeUtc = DateTime.UtcNow;

                agenteDeCarga.ModificadoPeloId = agenteDeCargaRequest.UsuarioId;

                _agenteDeCargaRepository.UpdateAgenteDeCarga(agenteDeCarga);

                if (await _agenteDeCargaRepository.SaveChanges())
                {
                    var ciaAereaResponseDTO = _mapper.Map<AgenteDeCargaResponseDto>(agenteDeCarga);
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = ciaAereaResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar o agente de carga!"
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
                        new ApiResponse<AgenteDeCargaResponseDto>
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
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(int agenteId)
        {
            try
            {
                var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(agenteId);
                if (agenteDeCarga == null)
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Agente de Carga não encontrada !"
                                }
                            }
                        };
                }

                agenteDeCarga.DataExclusao = DateTime.UtcNow;

                _agenteDeCargaRepository.UpdateAgenteDeCarga(agenteDeCarga);

                if (await _agenteDeCargaRepository.SaveChanges())
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<AgenteDeCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar o Agente de Carga!"
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
                        new ApiResponse<AgenteDeCargaResponseDto>
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
        private ApiResponse<AgenteDeCargaResponseDto> ErrorHandling(Exception exception)
        {
            var sqlEx = exception?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                {
                    //We have an error we can process
                    return new ApiResponse<AgenteDeCargaResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"Agente de Carga já Cadastrado !"
                                }
                        }
                    };
                }
                else
                {
                    return new ApiResponse<AgenteDeCargaResponseDto>
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
                return new ApiResponse<AgenteDeCargaResponseDto>
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
