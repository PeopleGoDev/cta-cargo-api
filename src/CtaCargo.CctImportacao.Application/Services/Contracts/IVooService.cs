using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IVooService
    {
        Task<ApiResponse<VooResponseDto>> AtualizarVoo(VooUpdateRequestDto vooRequest, UserSession userSessionInfo);
        Task<ApiResponse<VooResponseDto>> AtualizarReenviarVoo(int vooId, UserSession userSessionInfo);
        Task<ApiResponse<VooResponseDto>> ExcluirVoo(int vooId, UserSession userSessionInfo);
        Task<ApiResponse<VooResponseDto>> InserirVoo(VooInsertRequestDto vooRequest, UserSession userSessionInfo);
        Task<ApiResponse<IEnumerable<VooResponseDto>>> ListarVoos(VooListarInputDto input, UserSession userSessionInfo);
        ApiResponse<IEnumerable<VooTrechoResponse>> VooTrechoPorVooId(UserSession userSessionInfo, int vooId);
        Task<ApiResponse<IEnumerable<VooListaResponseDto>>> ListarVoosLista(VooListarInputDto input, UserSession userSessionInfo);
        Task<ApiResponse<VooResponseDto>> VooPorId(int vooId, UserSession userSessionInfo);
        Task<ApiResponse<VooUploadResponse>> VooUploadPorId(int vooId, UserSession userSessionInfo);
    }
}