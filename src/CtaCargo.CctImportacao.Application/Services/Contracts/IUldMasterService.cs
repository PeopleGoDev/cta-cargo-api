using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IUldMasterService
    {
        Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(UserSession userSession, List<UldMasterUpdateRequest> input);
        Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(UserSession userSession, List<UldMasterInsertRequest> uldMasterInsert, string inputMode = "Manual");
        Task<ApiResponse<UldMasterNumeroPatchQuery>> PatchUldMaster(UserSession userSession, UldMasterPatchRequest input, string inputMode = "Manual");
        Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(UserSession userSession, ListaUldMasterRequest input);
        Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(UserSession userSession, int uldMasterId);
        Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(UserSession userSession, int vooId);
        Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorTrechoId(UserSession userSession, int trechoId);
        Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(UserSession userSession, int uldId);
        Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumarioPorVooId(UserSession userSession, ListaUldMasterRequest input);
        Task<ApiResponse<string>> ExcluirUldMaster(UserSession userSession, UldMasterDeleteByIdInput input);
        Task<ApiResponse<string>> ExcluirUld(UserSession userSession, UldMasterDeleteByTagInput input);
    }
}