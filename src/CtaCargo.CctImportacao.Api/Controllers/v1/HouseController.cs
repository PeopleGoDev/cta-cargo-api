using System.Collections.Generic;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class HouseController : Controller
{
    private readonly IHouseService _houseService;
    public HouseController(IHouseService houseService)
    {
        _houseService = houseService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterHousePorId")]
    public async Task<ApiResponse<HouseResponseDto>> ObterHousePorId(int houseId)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.HousePorId(userSession, houseId);
    }

    [HttpPost]
    [Authorize]
    [Route("ListarHouses")]
    public async Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(HouseListarRequest input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.ListarHouses(userSession, input);
    }

    [HttpPost]
    [Authorize]
    [Route("listhouseassociationupload")]
    public async Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUpload(HouseListarRequest input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.SelectHouseAssociationForUpload(userSession, input);
    }

    [HttpPost]
    [Authorize]
    [Route("ListarHousesPorDataCriacao")]
    public async Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(MasterHousePorDataCriacaoRequest input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.ListarHousesPorDataCriacao(userSession, input);
    }
    
    [HttpPost]
    [Authorize]
    [Route("InserirHouse")]
    public async Task<ApiResponse<HouseResponseDto>> InserirHouse([FromBody]HouseInsertRequestDto input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.InserirHouse(userSession, input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarHouse([FromBody]HouseUpdateRequestDto input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.AtualizarHouse(userSession, input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReenviarHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarHouse([FromQuery] int houseId)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.AtualizarReenviarHouse(userSession, houseId);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReenviarAssociacaoHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarAssociacaoHouse([FromQuery] int houseId)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.AtualizarReenviarAssociacaoHouse(userSession, houseId);
    }

    [HttpDelete]
    [Authorize]
    [Route("ExcluirHouse")]
    public async Task<ApiResponse<HouseResponseDto>> ExcluirHouse(int houseId)
    {
        var userSession = HttpContext.GetUserSession();
        return await _houseService.ExcluirHouse(userSession, houseId);
    }
}
