using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
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

    public ReceitaFederalController(ISubmeterReceitaService submeterRFB)
    {
        _submeterRFB = submeterRFB;
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterVooCompleto")]
    public async Task<ApiResponse<string>> SubmeterVooCompleto(FlightUploadRequest input)
    {
        return await _submeterRFB.SubmeterVoo(HttpContext.GetUserSession(), input);
    }

    //[HttpPost]
    //[Authorize]
    //[Route("confirmdeparture")]
    //public async Task<ApiResponse<string>> ConfirmeDeparture(ConfirmDepartureRequest input)
    //{
    //    return await _submeterRFB.SubmeterVoo(HttpContext.GetUserSession(), input);
    //}

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
        return await _submeterRFB.VerificarProtocoloVoo(input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterExclusion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<FileUploadResponse>>))]
    public async Task<IActionResult> SubmeterMasterExclusion(MasterExclusaoRFBInput input)
    {
        var response = await _submeterRFB.SubmeterMasterExclusion(HttpContext.GetUserSession(), input);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterHouseAgenteDeCarga")]
    public async Task<ApiResponse<string>> SubmeterHouseAgenteDeCarga(SubmeterRFBHouseRequest input)
    {
        return await _submeterRFB.SubmeterHousesAgentesDeCarga(input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterAssociacaoHouseMaster")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SubmeterAssociacaoHousesMaster(SubmeterRFBMasterHouseRequest input)
    {
        if(input == null) return BadRequest();
        if(input.Masters is null) return BadRequest();
        if(input.Masters.Count == 0)  return BadRequest();

        return Ok(await _submeterRFB.SubmeterAssociacaoHousesMaster(HttpContext.GetUserSession(), input));
    }

    [HttpGet]
    [Authorize]
    [Route("CancelarAssociacaoHouseMaster")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarAssociacaoHousesMaster(int associationId)
    {
        if (associationId == null) return BadRequest();
        if (associationId <= 0) return BadRequest();

        return Ok(await _submeterRFB.SubmeterAssociation(HttpContext.GetUserSession(), associationId));
    }

    [HttpGet]
    [Authorize]
    [Route("CancelarHouse")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<HouseResponseDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarHouses(int houseId)
    {
        if (houseId == null) return BadRequest();
        if (houseId <= 0) return BadRequest();

        return Ok(await _submeterRFB.SubmeterHouseExclusion(HttpContext.GetUserSession(), houseId));
    }
}
