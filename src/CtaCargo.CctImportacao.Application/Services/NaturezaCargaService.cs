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
    public class NaturezaCargaService : INaturezaCargaService
    {
        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;

        private readonly INaturezaCargaRepository _naturezaCArgaRepository;
        private readonly IMapper _mapper;
        public NaturezaCargaService(INaturezaCargaRepository naturezaCArgaRepository, IMapper mapper)
        {
            _naturezaCArgaRepository = naturezaCArgaRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<NaturezaCargaResponseDto>> NaturezaCargaPorId(int id)
        {
            try
            {
                var naturezaCarga = await _naturezaCArgaRepository.GetNaturezaCargaById(id);
                if (naturezaCarga == null)
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Natureza da Carga não encontrado!"
                                }
                            }
                        };
                }
                var dto = _mapper.Map<NaturezaCargaResponseDto>(naturezaCarga);
                return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<NaturezaCargaResponseDto>
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
        public async Task<ApiResponse<IEnumerable<NaturezaCargaResponseDto>>> ListarNaturezaCarga(int empresaId)
        {
            try
            {
                var lista = await _naturezaCArgaRepository.GetAllNaturezaCarga(empresaId);

                var dto = _mapper.Map<IEnumerable<NaturezaCargaResponseDto>>(lista);

                return
                        new ApiResponse<IEnumerable<NaturezaCargaResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<NaturezaCargaResponseDto>>
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
        public async Task<ApiResponse<NaturezaCargaResponseDto>> InserirNaturezaCarga(NaturezaCargaInsertRequestDto input)
        {
            try
            {
                var naturezaCarga = _mapper.Map<NaturezaCarga>(input);

                naturezaCarga.CreatedDateTimeUtc = DateTime.UtcNow;

                _naturezaCArgaRepository.CreateNaturezaCarga(naturezaCarga);

                if (await _naturezaCArgaRepository.SaveChanges())
                {
                    var PortoIATAResponseDto = _mapper.Map<NaturezaCargaResponseDto>(naturezaCarga);
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = PortoIATAResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não Foi possível adicionar Natureza da Carga: Erro Desconhecido!"
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
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não Foi possível adicionar Natureza da Carga: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<NaturezaCargaResponseDto>> AtualizarNaturezaCarga(NaturezaCargaUpdateRequestDto input)
        {
            try
            {
                var naturezaCarga = await _naturezaCArgaRepository.GetNaturezaCargaById(input.NaturezaCargaId);

                if (naturezaCarga == null)
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar Natureza da Carga: Natureza da Carga não encontrada !"
                                }
                            }
                        };
                }

                _mapper.Map(input, naturezaCarga);

                naturezaCarga.ModifiedDateTimeUtc = DateTime.UtcNow;

                _naturezaCArgaRepository.UpdateNaturezaCarga(naturezaCarga);

                if (await _naturezaCArgaRepository.SaveChanges())
                {
                    var PortoIATAResponseDto = _mapper.Map<NaturezaCargaResponseDto>(naturezaCarga);
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = PortoIATAResponseDto,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar Natureza da Carga: Erro Desconhecido!"
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
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualizar Natureza da Carga: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<NaturezaCargaResponseDto>> ExcluirNaturezaCarga(int id)
        {
            try
            {
                var naturezaCarga = await _naturezaCArgaRepository.GetNaturezaCargaById(id);

                if (naturezaCarga == null)
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir Natureza da Carga: Natureza da Carga não encontrado !"
                                }
                            }
                        };
                }

                _naturezaCArgaRepository.DeleteNaturezaCarga(naturezaCarga);

                if (await _naturezaCArgaRepository.SaveChanges())
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir Natureza da Carga: Erro Desconhecido!"
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
                        new ApiResponse<NaturezaCargaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível excluir Natureza da Carga: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        private ApiResponse<NaturezaCargaResponseDto> ErrorHandling(Exception exception)
        {
            var sqlEx = exception?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                {
                    //We have an error we can process
                    return new ApiResponse<NaturezaCargaResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"Já existe uma Natureza da Carga cadastrada com o mesmo código!"
                                }
                        }
                    };
                }
                else
                {
                    return new ApiResponse<NaturezaCargaResponseDto>
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
                return new ApiResponse<NaturezaCargaResponseDto>
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
