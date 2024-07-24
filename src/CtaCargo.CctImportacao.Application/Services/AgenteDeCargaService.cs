using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

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
    public async Task<ApiResponse<AgenteDeCargaResponseDto>> PegarAgenteDeCargaPorId(UserSession userSession, int agenteId)
    {
        var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(userSession.CompanyId, agenteId);

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

    public async Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga(UserSession userSession)
    {
        var lista = await _agenteDeCargaRepository.GetAllAgenteDeCarga(userSession.CompanyId);

        var dto = _mapper.Map<IEnumerable<AgenteDeCargaResponseDto>>(lista);

        return
                new ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgenteDeCargaSimples(UserSession userSession)
    {
        var lista = await _agenteDeCargaRepository.GetAllAgenteDeCarga(userSession.CompanyId);

        var dto = from c in lista
                  select new AgenteDeCargaListaSimplesResponse()
                  {
                      Nome = c.Nome,
                      Numero = c.Numero,
                      AgenteDeCargaId = c.Id
                  };

        return new ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>
        {
            Dados = dto,
            Sucesso = true,
            Notificacoes = null
        };
    }

    public async Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga(UserSession userSession, AgenteDeCargaInsertRequest agenteDeCargaRequest)
    {
        var agenteDeCarga = _mapper.Map<AgenteDeCarga>(agenteDeCargaRequest);

        agenteDeCarga.CreatedDateTimeUtc = DateTime.UtcNow;
        agenteDeCarga.CriadoPeloId = userSession.UserId;
        agenteDeCarga.EmpresaId = userSession.CompanyId;

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar o agente de carga !");
    }

    public async Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga(UserSession userSession, AgenteDeCargaUpdateRequest agenteDeCargaRequest)
    {
        var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(userSession.CompanyId, agenteDeCargaRequest.AgenteDeCargaId);

        if (agenteDeCarga == null)
            throw new BusinessException("Agente de Carga não encontrado !");

        _mapper.Map(agenteDeCargaRequest, agenteDeCarga);

        agenteDeCarga.ModifiedDateTimeUtc = DateTime.UtcNow;
        agenteDeCarga.ModificadoPeloId = userSession.UserId;

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar o agente de carga!");
    }

    public async Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(UserSession userSession, int agenteId)
    {
        var agenteDeCarga = await _agenteDeCargaRepository.GetAgenteDeCargaById(userSession.CompanyId, agenteId);

        if (agenteDeCarga == null)
            throw new BusinessException("Agente de Carga não encontrada!");

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar o Agente de Carga!");
    }
}


