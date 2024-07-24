using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class PortoIATAController: Controller
{
    private readonly IPortoIataService _portoIATAService;

    public PortoIATAController(IPortoIataService portoIATAService)
    {
        _portoIATAService = portoIATAService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterPortoIATAPorId")]
    public async Task<ApiResponse<PortoIataResponseDto>> ObterPortoIATAPorId(int portoIATAId) =>
        await _portoIATAService.PortoIataPorId(HttpContext.GetUserSession(), portoIATAId);

    [HttpGet]
    [Authorize]
    [Route("ListarPortosIATA")]
    public async Task<ApiResponse<IEnumerable<PortoIataResponseDto>>> ListarPortosIATA() => 
        await _portoIATAService.ListarPortosIata(HttpContext.GetUserSession());

    [HttpPost]
    [Authorize]
    [Route("InserirPortoIATA")]
    public async Task<ApiResponse<PortoIataResponseDto>> InserirPortoIATA([FromBody]PortoIataInsertRequestDto input) => 
        await _portoIATAService.InserirPortoIata(HttpContext.GetUserSession(), input);

    [HttpPost]
    [Authorize]
    [Route("AtualizarPortoIATA")]
    public async Task<ApiResponse<PortoIataResponseDto>> AtualizarPortoIATA([FromBody]PortoIataUpdateRequestDto input) =>
        await _portoIATAService.AtualizarPortoIata(HttpContext.GetUserSession(), input);

    [HttpDelete]
    [Authorize]
    [Route("ExcluirPortoIATA")]
    public async Task<ApiResponse<PortoIataResponseDto>> ExcluirPortoIATA(int portoIATAId) =>
        await _portoIATAService.ExcluirPortoIata(HttpContext.GetUserSession(), portoIATAId);
}
