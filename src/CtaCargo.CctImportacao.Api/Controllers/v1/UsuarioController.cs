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
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("ObterUsuarioPorId")]
    public async Task<ApiResponse<UsuarioResponseDto>> ObterUsuarioPorId(int usuarioId)
    {
        return await _usuarioService.UsuarioPorId(usuarioId);
    }

    [HttpGet]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("ListarUsuarios")]
    public async Task<ApiResponse<IEnumerable<UsuarioResponseDto>>> ListarUsuarios()
    {
        return await _usuarioService.ListarUsuarios(HttpContext.GetUserSession().CompanyId);
    }

    [HttpPost]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("InserirUsuario")]
    public async Task<ApiResponse<UsuarioResponseDto>> InserirUsuario([FromBody]UsuarioInsertRequest input)
    {
        return await _usuarioService.InserirUsuario(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("AtualizarUsuario")]
    public async Task<ApiResponse<UsuarioResponseDto>> AtualizarUsuario([FromBody]UsuarioUpdateRequest input)
    {
        return await _usuarioService.AtualizarUsuario(input);
    }

    [HttpPost]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("ResetarUsuario")]
    public async Task<ApiResponse<string>> ResetarUsuario([FromBody] UserResetRequest input)
    {
        return await _usuarioService.ResetarUsuario(input);
    }

    [HttpDelete]
    [Authorize(Roles = "AdminUsuarios")]
    [Route("ExcluirUsuario")]
    public async Task<ApiResponse<UsuarioResponseDto>> ExcluirUsuario(int usuarioId)
    {
        return await _usuarioService.ExcluirUsuario(usuarioId);
    }
}
