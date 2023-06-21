using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IAgenteDeCargaService
    {
        Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga(AgenteDeCargaUpdateRequest agenteDeCargaRequest);
        Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(int agenteId);
        Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga(AgenteDeCargaInsertRequest agenteDeCargaRequest);
        Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga(int empresaId);
        Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgenteDeCargaSimples(int empresaId);
        Task<ApiResponse<AgenteDeCargaResponseDto>> PegarAgenteDeCargaPorId(int agenteId);
    }
}