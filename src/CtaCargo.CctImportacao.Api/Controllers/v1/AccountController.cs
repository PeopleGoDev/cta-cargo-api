using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        var response = await _accountService.AutenticarUsuario(usuarioLogin);
        return new()
        {
            Dados = response,
            Sucesso = true
        };
    }
}
