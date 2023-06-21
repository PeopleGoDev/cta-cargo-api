using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class UldController: Controller
    {
        private readonly IUldMasterService _uldMasterService;

        public UldController(IUldMasterService uldMasterService)
        {
            _uldMasterService = uldMasterService;
        }

        [HttpGet]
        [Authorize]
        [Route("PegarUldMasterPorId")]
        public async Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(int uldId)
        {
            return await _uldMasterService.PegarUldMasterPorId(uldId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarUldMasterPorMasterId")]
        public async Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(int masterId)
        {
            return await _uldMasterService.ListarUldMasterPorMasterId(masterId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarUldMasterPorVooId")]
        public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(int vooId)
        {
            return await _uldMasterService.ListarUldMasterPorVooId(vooId);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarUldMasterPorLinha")]
        public async Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(ListaUldMasterRequest input)
        {
            return await _uldMasterService.ListarUldMasterPorLinha(input);
        }

        [HttpPost]
        [Authorize]
        [Route("ListarMasterUldSumario")]
        public async Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumario([FromBody] ListaUldMasterRequest input)
        {
            return await _uldMasterService.ListarMasterUldSumarioPorVooId(input);
        }

        [HttpPost]
        [Authorize]
        [Route("InserirUldMaster")]
        public async Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(List<UldMasterInsertRequest> input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _uldMasterService.InserirUldMaster(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarUldMaster")]
        public async Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(List<UldMasterUpdateRequest> input)
        {
            return await _uldMasterService.AtualizarUldMaster(input);
        }

        [HttpPost]
        [Authorize]
        [Route("ExcluirUldMaster")]
        public async Task<ApiResponse<string>> ExcluirUldMaster(UldMasterDeleteByIdInput input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _uldMasterService.ExcluirUldMaster(userSession, input);
        }

        [HttpPost]
        [Authorize]
        [Route("ExcluirUld")]
        public async Task<ApiResponse<string>> ExcluirUld(UldMasterDeleteByTagInput input)
        {
            var userSession = HttpContext.GetUserSession();
            return await _uldMasterService.ExcluirUld(userSession, input);
        }

    }
}
