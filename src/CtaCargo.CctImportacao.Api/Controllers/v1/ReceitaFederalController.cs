using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ApiResponse<string>> SubmeterVooCompleto(VooUploadInput input)
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
    public async Task<ApiResponse<string>> SubmeterVooMasterCompleto(VooUploadInput input)
    {
        return await _submeterRFB.SubmeterVooMaster(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("VerificarProtocoloVoo")]
    public async Task<ApiResponse<string>> VerificarProtocoloVoo(VooUploadInput input)
    {
        return await _submeterRFB.VerificarProtocoloVoo(input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterExclusion")]
    public async Task<ApiResponse<string>> SubmeterMasterExclusion(MasterExclusaoRFBInput input)
    {
        return await _submeterRFB.SubmeterMasterExclusion(HttpContext.GetUserSession(), input);
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

        return Ok(await _submeterRFB.CancelarAssociacaoHousesMaster(HttpContext.GetUserSession(), associationId));
    }


}
