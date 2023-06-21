using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface ICiaAereaService
    {
        Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea(CiaAereaUpdateRequest ciaAereaRequest);
        Task<ApiResponse<CiaAereaResponseDto>> CiaAereaPorId(int ciaId);
        Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(int ciaId);
        Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea(CiaAereaInsertRequest ciaAereaRequest);
        Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiaAereas(int empresaId);
        Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiaAereasSimples(int empresaId);
    }
}