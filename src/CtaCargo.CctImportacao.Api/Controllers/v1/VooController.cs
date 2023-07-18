using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class VooController : Controller
    {
        private readonly IVooService _vooService;
        public VooController(IVooService vooService)
        {
            _vooService = vooService;
        }

        [HttpGet]
        [Authorize]
        [Route("ObterVooPorId")]
        public async Task<ApiResponse<VooResponseDto>> ObterVooPorId(int vooId)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.VooPorId(vooId, sessionInfo);
        }

        [HttpGet]
        [Authorize]
        [Route("ObterVooUploadPorId")]
        public async Task<ApiResponse<VooUploadResponse>> ObterVooUploadPorId(int vooId)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.VooUploadPorId(vooId, sessionInfo);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarVoos")]
        public async Task<ApiResponse<IEnumerable<VooResponseDto>>> ListarVoos([FromBody]VooListarInputDto input)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.ListarVoos(input, sessionInfo);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarVoosLista")]
        public async Task<ApiResponse<IEnumerable<VooListaResponseDto>>> ListarVoosLista ([FromBody] VooListarInputDto input)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.ListarVoosLista(input, sessionInfo);
        }

        [HttpPost]
        [Authorize]
        [Route("InserirVoo")]
        public async Task<ApiResponse<VooResponseDto>> InserirVoo([FromBody]VooInsertRequestDto input)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.InserirVoo(input, sessionInfo);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarVoo")]
        public async Task<ApiResponse<VooResponseDto>> AtualizarVoo([FromBody]VooUpdateRequestDto input)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.AtualizarVoo(input, sessionInfo);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarReenviarVoo")]
        public async Task<ApiResponse<VooResponseDto>> AtualizarReenviarVoo([FromQuery] int vooId)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.AtualizarReenviarVoo(vooId, sessionInfo);
        }

        [HttpDelete]
        [Authorize]
        [Route("ExcluirVoo")]
        public async Task<ApiResponse<VooResponseDto>> ExcluirVoo([FromQuery] int vooId)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return await _vooService.ExcluirVoo(vooId, sessionInfo);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarVooTrechos")]
        public ApiResponse<IEnumerable<VooTrechoResponse>> ListarVooTrechos(int vooId)
        {
            UserSession sessionInfo = GetUserSessionInfo();
            return _vooService.VooTrechoPorVooId(sessionInfo, vooId);
        }

        private UserSession GetUserSessionInfo()
        {
            var identity = User?.Identity as ClaimsIdentity;
            if (identity != null)
            {
                return new UserSession
                {
                    CompanyId = int.Parse(identity.FindFirst("CompanyId").Value),
                    UserId = int.Parse(identity.FindFirst("UserId").Value)
                };
            }
            return null;
        }

    }
}
