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
        var userSession = HttpContext.GetUserSession();
        return await _submeterRFB.SubmeterVoo(userSession, input);
    }

    [HttpPost]
    [Authorize]
    [Route("SubmeterMasterVooCompleto")]
    public async Task<ApiResponse<string>> SubmeterVooMasterCompleto(VooUploadInput input)
    {
        var userSession = HttpContext.GetUserSession();
        return await _submeterRFB.SubmeterVooMaster(userSession, input);
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
        var userSession = HttpContext.GetUserSession();
        return await _submeterRFB.SubmeterMasterExclusion(userSession, input);
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

        var userSession = HttpContext.GetUserSession();
        return Ok(await _submeterRFB.SubmeterAssociacaoHousesMaster(userSession, input));
    }
}
