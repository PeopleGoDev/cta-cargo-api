using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IPortoIataService
    {
        Task<ApiResponse<PortoIataResponseDto>> AtualizarPortoIata(UserSession userSession, PortoIataUpdateRequestDto portoIATARequest);
        Task<ApiResponse<PortoIataResponseDto>> ExcluirPortoIata(UserSession userSession, int portoIATAId);
        Task<ApiResponse<PortoIataResponseDto>> InserirPortoIata(UserSession userSession, PortoIataInsertRequestDto portoIATARequest);
        Task<ApiResponse<IEnumerable<PortoIataResponseDto>>> ListarPortosIata(UserSession userSession);
        Task<ApiResponse<PortoIataResponseDto>> PortoIataPorId(UserSession userSession, int portoIATAId);
    }
}