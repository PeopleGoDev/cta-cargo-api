using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class CiaAereaService : ICiaAereaService
{
    private readonly ICiaAereaRepository _ciaAereaRepository;
    private readonly IMapper _mapper;
    public CiaAereaService(ICiaAereaRepository ciaAereaRepository, IMapper mapper)
    {
        _ciaAereaRepository = ciaAereaRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CiaAereaResponseDto>> CiaAereaPorId(UserSession userSession, int airCompanyId)
    {
        var lista = await _ciaAereaRepository.GetCiaAereaById(userSession.CompanyId, airCompanyId);
        if (lista == null)
            throw new BusinessException("Companhia Aérea não encontrada !");

        var dto = _mapper.Map<CiaAereaResponseDto>(lista);
        return
                new ApiResponse<CiaAereaResponseDto>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiaAereas(UserSession userSession)
    {
        var lista = await _ciaAereaRepository.GetAllCiaAereas(userSession.CompanyId);
        var dto = _mapper.Map<IEnumerable<CiaAereaResponseDto>>(lista);
        return
                new ApiResponse<IEnumerable<CiaAereaResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiaAereasSimples(UserSession userSession)
    {
        var lista = await _ciaAereaRepository.GetAllCiaAereas(userSession.CompanyId);
        var dto = _mapper.Map<IEnumerable<CiaAreaListaSimplesResponse>>(lista);
        return
                new ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea(UserSession userSession, CiaAereaInsertRequest ciaAereaRequest)
    {
        var ciaAereaModel = _mapper.Map<CiaAerea>(ciaAereaRequest);
        ciaAereaModel.CreatedDateTimeUtc = DateTime.UtcNow;
        ciaAereaModel.CriadoPeloId = userSession.UserId;
        ciaAereaModel.EmpresaId = userSession.CompanyId;

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar a companhia aerea!");
    }

    public async Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea(UserSession userSession, CiaAereaUpdateRequest ciaAereaRequest)
    {
        var ciaFromRepo = await _ciaAereaRepository.GetCiaAereaById(userSession.CompanyId, ciaAereaRequest.CiaId);
        if (ciaFromRepo == null)
            throw new BusinessException("Companhia Aérea não encontrada !");

        _mapper.Map(ciaAereaRequest, ciaFromRepo);
        ciaFromRepo.ModifiedDateTimeUtc = DateTime.UtcNow;
        ciaFromRepo.ModificadoPeloId = userSession.UserId;

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar a companhia aerea!");
    }

    public async Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(UserSession userSession, int ciaId)
    {
        var ciaFromRepo = await _ciaAereaRepository.GetCiaAereaById(userSession.CompanyId, ciaId);
        if (ciaFromRepo == null)
            throw new BusinessException("Companhia Aérea não encontrada !");

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
            throw new BusinessException("Erro Desconhecido! Não Foi possível adicionar a companhia aerea!");
    }

}
