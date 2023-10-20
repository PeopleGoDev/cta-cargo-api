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

namespace CtaCargo.CctImportacao.Api.Controllers.v1;

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
    public async Task<ApiResponse<UldMasterResponseDto>> PegarUldMasterPorId(int uldId) =>
        await _uldMasterService.PegarUldMasterPorId(HttpContext.GetUserSession(), uldId);

    [HttpGet]
    [Authorize]
    [Route("ListarUldMasterPorMasterId")]
    public async Task<ApiResponse<List<UldMasterResponseDto>>> ListarUldMasterPorMasterId(int masterId) =>
        await _uldMasterService.ListarUldMasterPorMasterId(HttpContext.GetUserSession(), masterId);

    [HttpGet]
    [Authorize]
    [Route("ListarUldMasterPorVooId")]
    public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorVooId(int vooId) =>
        await _uldMasterService.ListarUldMasterPorVooId(HttpContext.GetUserSession(), vooId);

    [HttpGet]
    [Authorize]
    [Route("ListarUldMasterPorTrechoId")]
    public async Task<ApiResponse<IEnumerable<UldMasterNumeroQuery>>> ListarUldMasterPorTrechoId(int trechoId) =>
        await _uldMasterService.ListarUldMasterPorTrechoId(HttpContext.GetUserSession(), trechoId);

    [HttpPost]
    [Authorize]
    [Route("ListarUldMasterPorLinha")]
    public async Task<ApiResponse<IEnumerable<UldMasterResponseDto>>> ListarUldMasterPorLinha(ListaUldMasterRequest input) =>
        await _uldMasterService.ListarUldMasterPorLinha(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("ListarMasterUldSumario")]
    public async Task<ApiResponse<IEnumerable<MasterNumeroUldSumario>>> ListarMasterUldSumario([FromBody] ListaUldMasterRequest input) =>
        await _uldMasterService.ListarMasterUldSumarioPorVooId(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("InserirUldMaster")]
    public async Task<ApiResponse<List<UldMasterResponseDto>>> InserirUldMaster(List<UldMasterInsertRequest> input) =>
        await _uldMasterService.InserirUldMaster(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("AtualizarUldMaster")]
    public async Task<ApiResponse<List<UldMasterResponseDto>>> AtualizarUldMaster(List<UldMasterUpdateRequest> input) =>
        await _uldMasterService.AtualizarUldMaster(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("ExcluirUldMaster")]
    public async Task<ApiResponse<string>> ExcluirUldMaster(UldMasterDeleteByIdInput input) =>
        await _uldMasterService.ExcluirUldMaster(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("ExcluirUld")]
    public async Task<ApiResponse<string>> ExcluirUld(UldMasterDeleteByTagInput input) =>
        await _uldMasterService.ExcluirUld(HttpContext.GetUserSession(), input);

}
