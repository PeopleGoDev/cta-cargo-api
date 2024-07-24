using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class AgenteDeCargaController : Controller
{
    private readonly IAgenteDeCargaService _agenteDeCargaService;
    public AgenteDeCargaController(IAgenteDeCargaService agenteDeCargaService)
    {
        _agenteDeCargaService = agenteDeCargaService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterAgenteDeCargaPorId")]
    public async Task<ApiResponse<AgenteDeCargaResponseDto>> ObterAgenteDeCargaPorId(int agenteId)
    {
        return await _agenteDeCargaService.PegarAgenteDeCargaPorId(HttpContext.GetUserSession(), agenteId);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarAgentesDeCarga")]
    public async Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga()
    {
        return await _agenteDeCargaService.ListarAgentesDeCarga(HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("InserirAgenteDeCarga")]
    public async Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga([FromBody] AgenteDeCargaInsertRequest input)
    {
        return await _agenteDeCargaService.InserirAgenteDeCarga(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("AtualizarAgenteDeCarga")]
    public async Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga([FromBody] AgenteDeCargaUpdateRequest input)
    {
        return await _agenteDeCargaService.AtualizarAgenteDeCarga(HttpContext.GetUserSession(), input);
    }

    [HttpDelete]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("ExcluirAgenteDeCarga")]
    public async Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(int agenteId)
    {
        return await _agenteDeCargaService.ExcluirAgenteDeCarga(HttpContext.GetUserSession(), agenteId);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarAgentesDeCargaSimples")]
    public async Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgentesDeCargaSimples()
    {
        return await _agenteDeCargaService.ListarAgenteDeCargaSimples(HttpContext.GetUserSession());
    }
}
