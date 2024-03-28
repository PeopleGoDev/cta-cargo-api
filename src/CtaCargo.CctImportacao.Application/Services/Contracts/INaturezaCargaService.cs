using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface INaturezaCargaService
{
    Task<ApiResponse<NaturezaCargaResponseDto>> AtualizarNaturezaCarga(UserSession userSession, NaturezaCargaUpdateRequestDto input);
    Task<ApiResponse<NaturezaCargaResponseDto>> ExcluirNaturezaCarga(int id);
    Task<ApiResponse<NaturezaCargaResponseDto>> InserirNaturezaCarga(UserSession userSession, NaturezaCargaInsertRequestDto input);
    Task<ApiResponse<IEnumerable<NaturezaCargaResponseDto>>> ListarNaturezaCarga(int empresaId);
    Task<ApiResponse<NaturezaCargaResponseDto>> NaturezaCargaPorId(int id);
    IEnumerable<NaturezaCarga> GetSpecialInstructionByDescriptionLike(string like);
    IEnumerable<NaturezaCarga> GetSpecialInstructionByCode(string code);
    IEnumerable<NaturezaCarga> GetSpecialInstructionByCode(string[] codes);
}