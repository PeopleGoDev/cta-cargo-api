﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class VooController : Controller
{
    private readonly IVooService _vooService;
    public VooController(IVooService vooService)
    {
        _vooService = vooService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterVooPorId")]
    public async Task<ApiResponse<VooResponseDto>> ObterVooPorId(int vooId)
    {
        return await _vooService.VooPorId(vooId, HttpContext.GetUserSession());
    }

    [HttpGet]
    [Authorize]
    [Route("ObterVooUploadPorId")]
    public async Task<ApiResponse<VooUploadResponse>> ObterVooUploadPorId(int vooId)
    {
        return await _vooService.VooUploadPorId(vooId, HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("ListarVoos")]
    public async Task<ApiResponse<IEnumerable<VooResponseDto>>> ListarVoos([FromBody]VooListarInputDto input)
    {
        return await _vooService.ListarVoos(input, HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("ListarVoosLista")]
    public async Task<ApiResponse<IEnumerable<VooListaResponseDto>>> ListarVoosLista ([FromBody] VooListarInputDto input)
    {
        return await _vooService.ListarVoosLista(input, HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("InserirVoo")]
    public async Task<ApiResponse<VooResponseDto>> InserirVoo([FromBody]VooInsertRequestDto input)
    {
        return await _vooService.InserirVoo(input, HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarVoo")]
    public async Task<ApiResponse<VooResponseDto>> AtualizarVoo([FromBody]VooUpdateRequestDto input)
    {
        return await _vooService.AtualizarVoo(input, HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReenviarVoo")]
    public async Task<ApiResponse<VooResponseDto>> AtualizarReenviarVoo([FromQuery] int vooId)
    {
        return await _vooService.AtualizarReenviarVoo(vooId, HttpContext.GetUserSession());
    }

    [HttpDelete]
    [Authorize]
    [Route("ExcluirVoo")]
    public async Task<ApiResponse<VooResponseDto>> ExcluirVoo([FromQuery] int vooId)
    {
        return await _vooService.ExcluirVoo(vooId, HttpContext.GetUserSession());
    }

    [HttpGet]
    [Authorize]
    [Route("ListarVooTrechos")]
    public ApiResponse<IEnumerable<VooTrechoResponse>> ListarVooTrechos(int vooId)
    {
        return _vooService.VooTrechoPorVooId(HttpContext.GetUserSession(), vooId);
    }
}
