using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        return await _houseService.ListarHouses(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("listhouseassociationupload")]
    public async Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUpload(HouseListarRequest input)
    {
        return await _houseService.SelectHouseAssociationForUploadAsync(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("ListarHousesPorDataCriacao")]
    public async Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(MasterHousePorDataCriacaoRequest input)
    {
        return await _houseService.ListarHousesPorDataCriacao(HttpContext.GetUserSession(), input);
    }
    
    [HttpPost]
    [Authorize]
    [Route("InserirHouse")]
    public async Task<ApiResponse<HouseResponseDto>> InserirHouse([FromBody]HouseInsertRequestDto input)
    {
        return await _houseService.InserirHouse(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarHouse([FromBody]HouseUpdateRequestDto input)
    {
        return await _houseService.AtualizarHouse(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReenviarHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarHouse([FromQuery] int houseId)
    {
        return await _houseService.AtualizarReenviarHouse(HttpContext.GetUserSession(), houseId);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReenviarAssociacaoHouse")]
    public async Task<ApiResponse<HouseResponseDto>> AtualizarReenviarAssociacaoHouse([FromQuery] int houseId)
    {
        return await _houseService.AtualizarReenviarAssociacaoHouse(HttpContext.GetUserSession(), houseId);
    }

    [HttpDelete]
    [Authorize]
    [Route("ExcluirHouse")]
    public async Task<ApiResponse<HouseResponseDto>> ExcluirHouse(int houseId)
    {
        return await _houseService.ExcluirHouse(HttpContext.GetUserSession(), houseId);
    }

    [HttpPost]
    [Authorize]
    [Route("adicionar-master-house-associacao")]
    public async Task<ApiResponse<List<MasterHouseAssociationResponse>>> AdicionarMAsterHouseAssociacao([FromBody] AddMasterHouseAssociationRequest request)
    {
        var response = await _houseService.IncluirAssociacaoMasterHouse(HttpContext.GetUserSession(), request);

        return new ApiResponse<List<MasterHouseAssociationResponse>>
        {
            Dados = response,
            Sucesso = true
        };
    }

    [HttpPost]
    [Authorize]
    [Route("atualizar-master-house-associacao")]
    public async Task<ApiResponse<List<MasterHouseAssociationResponse>>> UpdateMasterHouseAssociacao([FromBody] UpdateMasterHouseAssociationRequest request)
    {
        var response = await _houseService.AtualizarAssociacaoMasterHouse(HttpContext.GetUserSession(), request);

        return new ApiResponse<List<MasterHouseAssociationResponse>>
        {
            Dados = response,
            Sucesso = true
        };
    }

    [HttpPost]
    [Authorize]
    [Route("desfazer-master-house-associacao")]
    public async Task<ApiResponse<List<MasterHouseAssociationResponse>>> DesfazerMasterHouseAssociacao([FromBody] RemoveMasterHouseAssociationRequest request)
    {
        var response = await _houseService.DesfazerAssociacaoMasterHouse(HttpContext.GetUserSession(), request);

        return new ApiResponse<List<MasterHouseAssociationResponse>>
        {
            Dados = response,
            Sucesso = true
        };
    }

    [HttpPost]
    [Authorize]
    [Route("for-upload-list")]
    public async Task<MasterHouseAssoationForUploadResponse> SelectHouseAssociationForUploadAsync(HouseListarRequest input)
    {
        return await _houseService.SelectHouseAssociationAsync(HttpContext.GetUserSession(), input);
    }
}
