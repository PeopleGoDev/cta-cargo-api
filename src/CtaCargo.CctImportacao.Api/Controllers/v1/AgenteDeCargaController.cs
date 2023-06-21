using System.Collections.Generic;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class AgenteDeCargaController
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
            return await _agenteDeCargaService.PegarAgenteDeCargaPorId(agenteId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarAgentesDeCarga")]
        public async Task<ApiResponse<IEnumerable<AgenteDeCargaResponseDto>>> ListarAgentesDeCarga(int empresaId)
        {
            return await _agenteDeCargaService.ListarAgentesDeCarga(empresaId);
        }

        [HttpPost]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("InserirAgenteDeCarga")]
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> InserirAgenteDeCarga([FromBody] AgenteDeCargaInsertRequest input)
        {
            return await _agenteDeCargaService.InserirAgenteDeCarga(input);
        }

        [HttpPost]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("AtualizarAgenteDeCarga")]
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> AtualizarAgenteDeCarga([FromBody] AgenteDeCargaUpdateRequest input)
        {
            return await _agenteDeCargaService.AtualizarAgenteDeCarga(input);
        }

        [HttpDelete]
        [Authorize(Roles = "AdminCiaAerea")]
        [Route("ExcluirAgenteDeCarga")]
        public async Task<ApiResponse<AgenteDeCargaResponseDto>> ExcluirAgenteDeCarga(int agenteId)
        {
            return await _agenteDeCargaService.ExcluirAgenteDeCarga(agenteId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarAgentesDeCargaSimples")]
        public async Task<ApiResponse<IEnumerable<AgenteDeCargaListaSimplesResponse>>> ListarAgentesDeCargaSimples(int empresaId)
        {
            return await _agenteDeCargaService.ListarAgenteDeCargaSimples(empresaId);
        }
    }
}
