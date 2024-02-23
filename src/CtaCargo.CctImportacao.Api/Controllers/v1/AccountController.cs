using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class AccountController : Controller
{

    private readonly IAccountService _accountService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IHttpContextAccessor httpContextAccessor,
        IAccountService accountService)
    {
        _accountService = accountService;
        this._httpContextAccessor = httpContextAccessor;
    }

    [HttpPost("Autenticar")]
    [AllowAnonymous]
    public async Task<ApiResponse<UsuarioLoginResponse>> Login([FromBody] UsuarioLoginRequest usuarioLogin)
    {
        return await _accountService.AutenticarUsuario(usuarioLogin);
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<ApiResponse<UsuarioLoginResponse>> Authenticate([FromBody] UsuarioLoginRequest usuarioLogin)
    {
        return await _accountService.AutenticarUsuario(usuarioLogin);
    }

    [HttpGet("register")]
    [Authorize]
    public ApiResponse<RegistryResponse> GetRegister()
    {
        var userSession = HttpContext.GetUserSession();
        var response = _accountService.GetRegistry(userSession);
        return new ApiResponse<RegistryResponse>
        {
            Dados = response,
            Sucesso = true,
        };
    }

    [HttpPut("register")]
    [Authorize]
    public ApiResponse<RegistryUpdateResponse> SetRegister(RegistryUpdateRequest request)
    {
        var userSession = HttpContext.GetUserSession();
        var response = _accountService.SetRegistry(userSession, request);
        return new ApiResponse<RegistryUpdateResponse>
        {
            Dados = response,
            Sucesso = true,
        };
    }

    [HttpGet("new-register")]
    public async Task<ApiResponse<RegistryResponse>> NewRegisterGet([FromHeader(Name = "Registration-Token")] Guid registerSession)
    {
        var response = await _accountService.NewRegistryGet(registerSession.ToString());

        return new()
        {
            Dados = response,
            Sucesso = response is null ? false : true,
        };
    }

    [HttpPost("new-register")]
    public async Task<ApiResponse<RegistryInsertResponse>> NewRegisterPost(
        [FromHeader(Name = "Registration-Token")] Guid registerSession,
        [FromForm] RegistryInsertRequest request,
        IFormFile file = null)
    {
        string fileType = "social-contract";

        if (file?.Length > 0)
        {
            using (var ms = file.OpenReadStream())
            {
                await _accountService.NewRegistryFilePost(registerSession.ToString(), fileType, ms);
            }
        }
        else
            await _accountService.DelRegistryFilePost(registerSession.ToString(), fileType);


        var response = await _accountService.NewRegistryPost(registerSession.ToString(), request);
        return new ApiResponse<RegistryInsertResponse>
        {
            Dados = response,
            Sucesso = true,
        };
    }

    [HttpGet("new-register-aircompany")]
    public async Task<ApiResponse<CiaAereaResponseDto>> NewRegisterAirCompanyGet([FromHeader(Name = "Registration-Token")] Guid registerSession)
    {
        var response = await _accountService.NewRegistryAirCompanyGet(registerSession.ToString());

        return new()
        {
            Dados = response,
            Sucesso = response is null ? false : true,
        };
    }

    [HttpPost("new-register-aircompany")]
    public async Task<ApiResponse<CiaAereaResponseDto>> NewRegisterAirCompanyPost(
        [FromHeader(Name = "Registration-Token")] Guid registerSession,
        [FromForm] RegistryAirCompanyInsertRequest request,
        IFormFile file = null)
    {
        string fileType = "digital-certificate";

        if (file?.Length > 0)
        {
            using (var ms = file.OpenReadStream())
            {
                await _accountService.NewRegistryFilePost(registerSession.ToString(), fileType, ms);
            }
        }
        else
            await _accountService.DelRegistryFilePost(registerSession.ToString(), fileType);

        var response = await _accountService.NewRegistryAirCompanyPost(registerSession.ToString(), request);

        return new()
        {
            Dados = response,
            Sucesso = response is null ? false : true,
        };
    }

    [HttpPost("new-register-package")]
    public async Task<ApiResponse<string>> NewRegisterPackage(
    [FromHeader(Name = "Registration-Token")] Guid registerSession,
    [FromBody] RegistrySelectedPackage request)
    {
        var response = await _accountService.NewRegistryPackage(registerSession.ToString(), request);

        return new()
        {
            Dados = "Sucesso",
            Sucesso = true,
        };
    }
}
