using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

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

    public async Task<ApiResponse<NaturezaCargaResponseDto>> InserirNaturezaCarga(UserSession userSession, NaturezaCargaInsertRequestDto input)
    {
        var naturezaCarga = _mapper.Map<NaturezaCarga>(input);

        naturezaCarga.CreatedDateTimeUtc = DateTime.UtcNow;
        naturezaCarga.CriadoPeloId = userSession.UserId;
        naturezaCarga.EmpresaId = userSession.CompanyId;

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
            throw new BusinessException("Não Foi possível adicionar Natureza da Carga: Erro Desconhecido!");

    }

    public async Task<ApiResponse<NaturezaCargaResponseDto>> AtualizarNaturezaCarga(UserSession userSession, NaturezaCargaUpdateRequestDto input)
    {
        var naturezaCarga = await _naturezaCArgaRepository.GetNaturezaCargaById(input.NaturezaCargaId);

        if (naturezaCarga == null)
            throw new BusinessException("Não foi possível atualizar Natureza da Carga: Natureza da Carga não encontrada !");

        _mapper.Map(input, naturezaCarga);

        naturezaCarga.ModifiedDateTimeUtc = DateTime.UtcNow;
        naturezaCarga.ModificadoPeloId = userSession.UserId;

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
            throw new BusinessException("Não foi possível atualizar Natureza da Carga: Erro Desconhecido!");

    }

    public async Task<ApiResponse<NaturezaCargaResponseDto>> ExcluirNaturezaCarga(int id)
    {

        var naturezaCarga = await _naturezaCArgaRepository.GetNaturezaCargaById(id);

        if (naturezaCarga == null)
            throw new BusinessException("Não foi possível excluir Natureza da Carga: Natureza da Carga não encontrado !");

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
            throw new BusinessException("Não foi possível excluir Natureza da Carga: Erro Desconhecido!");
    }

    public IEnumerable<NaturezaCarga> GetSpecialInstructionByDescriptionLike(string like) =>
        _naturezaCArgaRepository.GetTopSpecialInstruction(like, 5);

    public IEnumerable<NaturezaCarga> GetSpecialInstructionByCode(string code) =>
        _naturezaCArgaRepository.GetTopSpecialInstructionByCode(code, 5);

    public IEnumerable<NaturezaCarga> GetSpecialInstructionByCode(string[] codes) =>
        _naturezaCArgaRepository.GetSpecialInstructionByCodeList(codes);

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
