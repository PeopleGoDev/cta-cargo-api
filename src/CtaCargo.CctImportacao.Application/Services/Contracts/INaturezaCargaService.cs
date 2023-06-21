using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface INaturezaCargaService
    {
        Task<ApiResponse<NaturezaCargaResponseDto>> AtualizarNaturezaCarga(NaturezaCargaUpdateRequestDto input);
        Task<ApiResponse<NaturezaCargaResponseDto>> ExcluirNaturezaCarga(int id);
        Task<ApiResponse<NaturezaCargaResponseDto>> InserirNaturezaCarga(NaturezaCargaInsertRequestDto input);
        Task<ApiResponse<IEnumerable<NaturezaCargaResponseDto>>> ListarNaturezaCarga(int empresaId);
        Task<ApiResponse<NaturezaCargaResponseDto>> NaturezaCargaPorId(int id);
    }
}