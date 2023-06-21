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
    public class CiaAereaService : ICiaAereaService
    {
        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;

        private readonly ICiaAereaRepository _ciaAereaRepository;
        private readonly IMapper _mapper;
        public CiaAereaService(ICiaAereaRepository ciaAereaRepository, IMapper mapper)
        {
            _ciaAereaRepository = ciaAereaRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CiaAereaResponseDto>> CiaAereaPorId(int ciaId)
        {
            try
            {
                var lista = await _ciaAereaRepository.GetCiaAereaById(ciaId);
                if (lista == null)
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Companhia Aérea não encontrada !"
                                }
                            }
                        };
                }
                var dto = _mapper.Map<CiaAereaResponseDto>(lista);
                return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<CiaAereaResponseDto>
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
        public async Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiaAereas(int empresaId)
        {
            try
            {
                var lista = await _ciaAereaRepository.GetAllCiaAereas(empresaId);
                var dto = _mapper.Map<IEnumerable<CiaAereaResponseDto>>(lista);
                return
                        new ApiResponse<IEnumerable<CiaAereaResponseDto>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<CiaAereaResponseDto>>
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
        public async Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiaAereasSimples(int empresaId)
        {
            try
            {
                var lista = await _ciaAereaRepository.GetAllCiaAereas(empresaId);
                var dto = _mapper.Map<IEnumerable<CiaAreaListaSimplesResponse>>(lista);
                return
                        new ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>
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
        public async Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea(CiaAereaInsertRequest ciaAereaRequest)
        {
            try
            {
                var ciaAereaModel = _mapper.Map<CiaAerea>(ciaAereaRequest);
                ciaAereaModel.CreatedDateTimeUtc = DateTime.UtcNow;
                ciaAereaModel.CriadoPeloId = ciaAereaRequest.UsuarioId;

                _ciaAereaRepository.CreateCiaAerea(ciaAereaModel);

                if (await _ciaAereaRepository.SaveChanges())
                {
                    var ciaAereaResponseDTO = _mapper.Map<CiaAereaResponseDto>(ciaAereaModel);
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = ciaAereaResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar a companhia aerea!"
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
                        new ApiResponse<CiaAereaResponseDto>
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
        public async Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea(CiaAereaUpdateRequest ciaAereaRequest)
        {
            try
            {
                var ciaFromRepo = await _ciaAereaRepository.GetCiaAereaById(ciaAereaRequest.CiaId);
                if (ciaFromRepo == null)
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Companhia Aérea não encontrada !"
                                }
                            }
                        };
                }

                _mapper.Map(ciaAereaRequest, ciaFromRepo);
                ciaFromRepo.ModifiedDateTimeUtc = DateTime.UtcNow;
                ciaFromRepo.ModificadoPeloId = ciaAereaRequest.UsuarioId;

                _ciaAereaRepository.UpdateCiaAerea(ciaFromRepo);

                if (await _ciaAereaRepository.SaveChanges())
                {
                    var ciaAereaResponseDTO = _mapper.Map<CiaAereaResponseDto>(ciaFromRepo);
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = ciaAereaResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar a companhia aerea!"
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
                        new ApiResponse<CiaAereaResponseDto>
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
        public async Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(int ciaId)
        {
            try
            {
                var ciaFromRepo = await _ciaAereaRepository.GetCiaAereaById(ciaId);
                if (ciaFromRepo == null)
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Companhia Aérea não encontrada !"
                                }
                            }
                        };
                }

                ciaFromRepo.DataExclusao = DateTime.UtcNow;

                _ciaAereaRepository.UpdateCiaAerea(ciaFromRepo);

                if (await _ciaAereaRepository.SaveChanges())
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<CiaAereaResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Erro Desconhecido! Não Foi possível adicionar a companhia aerea!"
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
                        new ApiResponse<CiaAereaResponseDto>
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
        private ApiResponse<CiaAereaResponseDto> ErrorHandling(Exception exception)
        {
            var sqlEx = exception?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                {
                    //We have an error we can process
                    return new ApiResponse<CiaAereaResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"Companhia Aérea já Cadastrada !"
                                }
                        }
                    };
                }
                else
                {
                    return new ApiResponse<CiaAereaResponseDto>
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
                return new ApiResponse<CiaAereaResponseDto>
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
