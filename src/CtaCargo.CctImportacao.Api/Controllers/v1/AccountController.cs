using System;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CtaCargo.CctImportacao.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Autenticar")]
        [AllowAnonymous]
        public async Task<ApiResponse<UsuarioLoginResponse>> Login([FromBody]UsuarioLoginRequest usuarioLogin)
        {
            return await _accountService.AutenticarUsuario(usuarioLogin);
        }
    }
}
