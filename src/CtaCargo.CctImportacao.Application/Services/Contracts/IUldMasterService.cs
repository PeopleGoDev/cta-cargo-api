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
        Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(List<UldMasterUpdateRequest> uldMasterRequest);
        Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(UserSession userSession, List<UldMasterInsertRequest> uldMasterInsert);
        Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(ListaUldMasterRequest input);
        Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(int uldMasterId);
        Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(int vooId);
        Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(int uldId);
        Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumarioPorVooId(ListaUldMasterRequest input);
        Task<ApiResponse<string>> ExcluirUldMaster(UserSession userSession, UldMasterDeleteByIdInput input);
        Task<ApiResponse<string>> ExcluirUld(UserSession userSession, UldMasterDeleteByTagInput input);
    }
}