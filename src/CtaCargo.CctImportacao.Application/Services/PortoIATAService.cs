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
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class PortoIATAService : IPortoIataService
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
    public async Task<ApiResponse<PortoIataResponseDto>> PortoIataPorId(UserSession userSession, int portoIataId)
    {
        var lista = await _portoIATARepository.GetPortoIATAById(userSession.CompanyId, portoIataId);
        if (lista == null)
            throw new BusinessException("Porto IATA não encontrado");

        var dto = _mapper.Map<PortoIataResponseDto>(lista);
        return
                new ApiResponse<PortoIataResponseDto>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }
    public async Task<ApiResponse<IEnumerable<PortoIataResponseDto>>> ListarPortosIata(UserSession userSession)
    {
        var lista = await _portoIATARepository.GetAllPortosIATA(userSession.CompanyId);
        var dto = _mapper.Map<IEnumerable<PortoIataResponseDto>>(lista);
        return
                new ApiResponse<IEnumerable<PortoIataResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }
    public async Task<ApiResponse<PortoIataResponseDto>> InserirPortoIata(UserSession userSession, PortoIataInsertRequestDto portoIataRequest)
    {
        var portoIata = _mapper.Map<PortoIata>(portoIataRequest);
        portoIata.CriadoPeloId = userSession.UserId;
        portoIata.EmpresaId = userSession.CompanyId;
        portoIata.CreatedDateTimeUtc = DateTime.UtcNow;

        _portoIATARepository.CreatePortoIATA(portoIata);

        if (await _portoIATARepository.SaveChanges())
        {
            var PortoIATAResponseDto = _mapper.Map<PortoIataResponseDto>(portoIata);
            return
                new ApiResponse<PortoIataResponseDto>
                {
                    Dados = PortoIATAResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
            throw new BusinessException("Não Foi possível adicionar Porto IATA: Erro Desconhecido");

    }
    public async Task<ApiResponse<PortoIataResponseDto>> AtualizarPortoIata(UserSession userSession, PortoIataUpdateRequestDto portoIataRequest)
    {
        var portoIata = await _portoIATARepository.GetPortoIATAById(userSession.CompanyId, portoIataRequest.PortoIataId);
        if (portoIata == null)
            throw new BusinessException("Não foi possível atualizar Porto IATA: Porto IATA não encontrado");

        _mapper.Map(portoIataRequest, portoIata);
        portoIata.ModifiedDateTimeUtc = DateTime.UtcNow;
        portoIata.ModificadoPeloId = userSession.UserId;

        _portoIATARepository.UpdatePortoIATA(portoIata);

        if (await _portoIATARepository.SaveChanges())
        {
            var PortoIATAResponseDto = _mapper.Map<PortoIataResponseDto>(portoIata);
            return
                new ApiResponse<PortoIataResponseDto>
                {
                    Dados = PortoIATAResponseDto,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
            throw new BusinessException("Não foi possível atualizar Porto IATA: Erro Desconhecido");

    }
    public async Task<ApiResponse<PortoIataResponseDto>> ExcluirPortoIata(UserSession userSession, int portoIataId)
    {
        var portoIATARepo = await _portoIATARepository.GetPortoIATAById(userSession.CompanyId, portoIataId);
        if (portoIATARepo == null)
            throw new BusinessException("Não foi possível excluir Porto Iata: Porto Iata não encontrado!");

        _portoIATARepository.DeletePortoIATA(portoIATARepo);

        if (await _portoIATARepository.SaveChanges())
        {
            return
                new ApiResponse<PortoIataResponseDto>
                {
                    Dados = null,
                    Sucesso = true,
                    Notificacoes = null
                };
        }
        else
            throw new BusinessException("Não foi possível excluir Porto IATA: Erro Desconhecido");

    }
}
