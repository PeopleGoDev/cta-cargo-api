using System.Collections.Generic;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class MasterController : Controller
    {
        private readonly IMasterService _masterService;
        public MasterController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet]
        [Authorize]
        [Route("ObterMasterPorId")]
        public async Task<ApiResponse<MasterResponseDto>> ObterMasterPorId(int masterId)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.MasterPorId(userSession, masterId);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarMasters")]
        public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMasters(MasterListarRequest input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.ListarMasters(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarMastersPorDataCriacao")]
        public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMastersPorDataCriacao(MasterHousePorDataCriacaoRequest input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.ListarMastersPorDataCriacao(userSession, input);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarMastersVoo")]
        public async Task<ApiResponse<IEnumerable<MasterVooResponseDto>>> ListarMastersVoo(int vooId)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.ListarMastersVoo(userSession, vooId);
        }
        
        [HttpGet]
        [Authorize]
        [Route("ListarMastersListaPorVooId")]
        public async Task<ApiResponse<IEnumerable<MasterListaResponseDto>>> ListarMastersListaPorVooId(int vooId)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.ListarMasterListaPorVooId(userSession, vooId);
        }

        [HttpPost]
        [Authorize]
        [Route("InserirMaster")]
        public async Task<ApiResponse<MasterResponseDto>> InserirMaster([FromBody]MasterInsertRequestDto input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.InserirMaster(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarMaster")]
        public async Task<ApiResponse<MasterResponseDto>> AtualizarMaster([FromBody]MasterUpdateRequestDto input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.AtualizarMaster(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarReeviarMaster")]
        public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> AtualizarReeviarMaster([FromBody] AtualizarMasterReenviarRequest input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.AtualizarReenviarMaster(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("ExcluirMaster")]
        public async Task<ApiResponse<string>> ExcluirMaster(ExcluirMastersByIdRequest input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _masterService.ExcluirMaster(userSession, input);
        }
    }
}
