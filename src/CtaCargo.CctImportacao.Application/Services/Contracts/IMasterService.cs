using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IMasterService
    {
        Task<ApiResponse<MasterResponseDto>> AtualizarMaster(UserSession userSession, MasterUpdateRequestDto input);
        Task<ApiResponse<IEnumerable<MasterResponseDto>>> AtualizarReenviarMaster(UserSession userSession, AtualizarMasterReenviarRequest input);
        Task<ApiResponse<string>> ExcluirMaster(UserSession userSession, ExcluirMastersByIdRequest input);
        Task<ApiResponse<MasterResponseDto>> InserirMaster(UserSession userSession, MasterInsertRequestDto input);
        Task<ApiResponse<IEnumerable<MasterListaResponseDto>>> ListarMasterListaPorVooId(UserSession userSession, int vooId);
        Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMasters(UserSession userSession, MasterListarRequest input);
        Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMastersPorDataCriacao(UserSession userSession, MasterHousePorDataCriacaoRequest input);
        Task<ApiResponse<IEnumerable<MasterVooResponseDto>>> ListarMastersVoo(UserSession userSession, int vooId);
        Task<ApiResponse<MasterResponseDto>> MasterPorId(UserSession userSession, int masterId);
        Task<ApiResponse<List<MasterResponseDto>>> ImportFile(UserSession userSession, MasterFileImportRequest input, Stream stream);
        ApiResponse<List<MasterFileResponseDto>> GetFilesToImport(UserSession userSession);
    }
}