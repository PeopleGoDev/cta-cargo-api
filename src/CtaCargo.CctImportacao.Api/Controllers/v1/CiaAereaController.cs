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
public class CiaAereaController : Controller
{
    private readonly ICiaAereaService _ciaAereaService;
    public CiaAereaController(ICiaAereaService ciaAereaService)
    {
        _ciaAereaService = ciaAereaService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterCiaAereaPorId")]
    public async Task<ApiResponse<CiaAereaResponseDto>> ObterCiaAereaPorId(int ciaId)
    {
        return await _ciaAereaService.CiaAereaPorId(HttpContext.GetUserSession(), ciaId);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarCiasAereas")]
    public async Task<ApiResponse<IEnumerable<CiaAereaResponseDto>>> ListarCiasAereas()
    {
        return await _ciaAereaService.ListarCiaAereas(HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("InserirCiaAerea")]
    public async Task<ApiResponse<CiaAereaResponseDto>> InserirCiaAerea([FromBody]CiaAereaInsertRequest input)
    {  
        return await _ciaAereaService.InserirCiaAerea(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("AtualizarCiaAerea")]
    public async Task<ApiResponse<CiaAereaResponseDto>> AtualizarCiaAerea([FromBody]CiaAereaUpdateRequest input)
    {
        return await _ciaAereaService.AtualizarCiaAerea(HttpContext.GetUserSession(), input);
    }

    [HttpDelete]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("ExcluirCiaAerea")]
    public async Task<ApiResponse<CiaAereaResponseDto>> ExcluirCiaAerea(int ciaId)
    {
        return await _ciaAereaService.ExcluirCiaAerea(HttpContext.GetUserSession(), ciaId);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarCiasAereasSimples")]
    public async Task<ApiResponse<IEnumerable<CiaAreaListaSimplesResponse>>> ListarCiasAereasSimples()
    {
        return await _ciaAereaService.ListarCiaAereasSimples(HttpContext.GetUserSession());
    }
}
