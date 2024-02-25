using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class UploadController : Controller
{
    private readonly IUploadService _uploadService;
    public UploadController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]
    [Authorize(Roles = "AdminCiaAerea")]
    [Route("UploadCertificadoDigital")]
    public async Task<ApiResponse<UploadCertificadoResponseDto>> UploadCertificadoDigital([FromForm] UploadFileRequest input, IFormFile file)
    {
        if (file.Length > 0)
        {
            using (var ms = file.OpenReadStream())
            {
                var response = await _uploadService.UploadArquivo(HttpContext.GetUserSession(), input, ms);
                return new()
                {
                    Dados = response,
                    Sucesso = true,
                };
            }
        }

        return new()
        {
            Sucesso = false,
            Notificacoes = new List<Notificacao>() {
                    new Notificacao()
                    {
                        Codigo = "9999",
                        Mensagem = "Não foi possível processar arquivo zerado!"
                    }
                }
        };
    }

}
