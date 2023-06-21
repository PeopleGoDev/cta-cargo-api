using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IPortoIATAService
    {
        Task<ApiResponse<PortoIATAResponseDto>> AtualizarPortoIATA(PortoIATAUpdateRequestDto portoIATARequest);
        Task<ApiResponse<PortoIATAResponseDto>> ExcluirPortoIATA(int portoIATAId);
        Task<ApiResponse<PortoIATAResponseDto>> InserirPortoIATA(PortoIATAInsertRequestDto portoIATARequest);
        Task<ApiResponse<IEnumerable<PortoIATAResponseDto>>> ListarPortosIATA(int empresaId);
        Task<ApiResponse<PortoIATAResponseDto>> PortoIATAPorId(int portoIATAId);
    }
}