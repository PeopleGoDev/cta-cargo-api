using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IAgenteDeCargaService
{
    Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga(UserSession userSession, AgenteDeCargaUpdateRequest agenteDeCargaRequest);
    Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(UserSession userSession, int agenteId);
    Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga(UserSession userSession, AgenteDeCargaInsertRequest agenteDeCargaRequest);
    Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga(UserSession userSession);
    Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgenteDeCargaSimples(UserSession userSession);
    Task<ApiResponse<AgenteDeCargaResponseDto>> PegarAgenteDeCargaPorId(UserSession userSession, int agenteId);
}