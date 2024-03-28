using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface ICiaAereaService
{
    Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea(UserSession userSession, CiaAereaUpdateRequest ciaAereaRequest);
    Task<ApiResponse<CiaAereaResponseDto>> CiaAereaPorId(UserSession userSession, int airCompanyId);
    Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(UserSession userSession, int airCompanyId);
    Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea(UserSession userSession, CiaAereaInsertRequest ciaAereaRequest);
    Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiaAereas(UserSession userSession);
    Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiaAereasSimples(UserSession userSession);
}