using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao;
using CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao.Request;
using CtaCargo.CctImportacao.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class ReceitaFederalController : Controller
{
    private readonly ISubmeterReceitaService _submeterRFB;
    private readonly IReceitaHouseService _receitaHouseService;
    private readonly IRemoveAssociationService _removeAssociationService;

    public ReceitaFederalController(ISubmeterReceitaService submeterRFB, IReceitaHouseService receitaHouseService, IRemoveAssociationService removeAssociationService)
    {
        _submeterRFB = submeterRFB;
        _receitaHouseService = receitaHouseService;
        _removeAssociationService = removeAssociationService;
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterVooCompleto")]
    public async Task<ApiResponse<string>> SubmeterVooCompleto(FlightUploadRequest input)
    {
        return await _submeterRFB.SubmeterVoo(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("submitscheduledflight")]
    public async Task<ApiResponse<string>> SubmitScheduledFlight(FlightUploadRequest input)
    {
        return await _submeterRFB.SubmitFlightSchedule(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterVooCompleto")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<FileUploadResponse>>))]
    public async Task<IActionResult> SubmeterVooMasterCompleto(FlightUploadRequest input)
    {
        var response = await _submeterRFB.SubmeterVooMaster(HttpContext.GetUserSession(), input);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterSelecionado")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<FileUploadResponse>>))]
    public async Task<IActionResult> SubmeterMasterSelecionado(FlightUploadRequest input)
    {
        if (input.idList == null || input.idList.IsNullOrEmpty())
            throw new BadRequestException("Lista de id(s) de master(s) requerida!");

        var response = await _submeterRFB.SubmeterMasterSelecionado(HttpContext.GetUserSession(), input);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("VerificarProtocoloVoo")]
    public async Task<ApiResponse<string>> VerificarProtocoloVoo(FlightUploadRequest input)
    {
        return await _submeterRFB.VerificarProtocoloVoo(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterExclusion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<MasterResponseDto>))]
    public async Task<IActionResult> SubmeterMasterExclusion(MasterExclusaoRFBInput input)
    {
        var response = await _submeterRFB.SubmeterMasterExclusion(HttpContext.GetUserSession(), input);

        return Ok(new ApiResponse<MasterResponseDto>()
        {
            Dados = response,
            Sucesso = true,
        });
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterHouseAgenteDeCarga")]
    public async Task<ApiResponse<string>> SubmeterHouseAgenteDeCarga(SubmeterRFBHouseRequest input)
    {
        return await _receitaHouseService
            .SubmeterHousesAgentesDeCarga(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterHouseAgenteDeCargaPorIds")]
    public async Task<ApiResponse<string>> SubmeterHouseAgenteDeCargaEIds(SubmeterRFBHouseByIdsRequest input)
    {
        return await _receitaHouseService
            .SubmeterHousesAgentesDeCargaAndIds(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterAssociacaoHouseMaster")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmeterAssociacaoHousesMaster(SubmeterRFBMasterHouseRequest input)
    {
        if (input == null) return BadRequest();
        if (input.Masters is null) return BadRequest();
        if (input.Masters.Count == 0) return BadRequest();

        return Ok(await _receitaHouseService.SubmeterAssociacaoHousesMaster(HttpContext.GetUserSession(), input));
    }

    [HttpPost]
    [Authorize]
    [Route("submit-house-master-association")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitHouseMasterAssociationAsync(SubmitRFBMasterHouseRequest input)
    {
        if (input == null) return BadRequest();
        if (input.AssociationIds is null) return BadRequest();
        if (input.AssociationIds.Length == 0) return BadRequest();

        return Ok(await _receitaHouseService.SubmitHouseMasterAssociationAsync(HttpContext.GetUserSession(), input));
    }

    [HttpPost]
    [Authorize]
    [Route("submit-association-remove")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<MasterHouseAssociationUploadResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmitAssociationRemoveAsync([FromBody] SubmitAssociatonRequest request)
    {
        if (request.freightFowarderId <= 0) return BadRequest();

        var response = await _removeAssociationService.RemoveAssociationAsync(HttpContext.GetUserSession(), request);

        return Ok(new ApiResponse<List<MasterHouseAssociationUploadResponse>>
        {
            Dados = response,
            Sucesso = true
        });
    }

    [HttpPost]
    [Authorize]
    [Route("check-association-remove")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<MasterHouseAssociationUploadResponse>>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarAssociacaoHousesMaster([FromBody] SubmitAssociatonRequest request)
    {
        if (request.freightFowarderId <= 0) return BadRequest();

        var response = await _removeAssociationService.CheckRemoveAssociationAsync(HttpContext.GetUserSession(), request);

        return Ok(new ApiResponse<List<MasterHouseAssociationUploadResponse>>
        {
            Dados = response,
            Sucesso = true
        });
    }

    [HttpGet]
    [Authorize]
    [Route("CancelarHouse")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<HouseResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarHouses(int houseId)
    {
        if (houseId <= 0) return BadRequest();

        return Ok(await _receitaHouseService.SubmeterHouseExclusion(HttpContext.GetUserSession(), houseId));
    }
}
