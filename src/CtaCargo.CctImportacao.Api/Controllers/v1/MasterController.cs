using CtaCargo.CctImportacao.Api.Controllers.Session;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services;
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
public class MasterController : Controller
{
    private readonly IMasterService _masterService;
    public MasterController(IMasterService masterService)
    {
        _masterService = masterService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterMasterPorId")]
    public async Task<ApiResponse<MasterResponseDto>> ObterMasterPorId(int masterId)
    {
        return await _masterService.MasterPorId(HttpContext.GetUserSession(), masterId);
    }

    [HttpPost]
    [Authorize]
    [Route("ListarMasters")]
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMasters(MasterListarRequest input)
    {
        return await _masterService.ListarMasters(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("ListarMastersPorDataCriacao")]
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> ListarMastersPorDataCriacao(MasterHousePorDataCriacaoRequest input)
    {
        return await _masterService.ListarMastersPorDataCriacao(HttpContext.GetUserSession(), input);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarMastersVoo")]
    public async Task<ApiResponse<IEnumerable<MasterVooResponseDto>>> ListarMastersVoo(int vooId)
    {
        return await _masterService.ListarMastersVoo(HttpContext.GetUserSession(), vooId);
    }
    
    [HttpGet]
    [Authorize]
    [Route("ListarMastersListaPorVooId")]
    public async Task<ApiResponse<IEnumerable<MasterListaResponseDto>>> ListarMastersListaPorVooId(int vooId)
    {
        return await _masterService.ListarMasterListaPorVooId(HttpContext.GetUserSession(), vooId);
    }

    [HttpPost]
    [Authorize]
    [Route("InserirMaster")]
    public async Task<ApiResponse<MasterResponseDto>> InserirMaster([FromBody]MasterInsertRequestDto input)
    {
        return await _masterService.InserirMaster(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarMaster")]
    public async Task<ApiResponse<MasterResponseDto>> AtualizarMaster([FromBody]MasterUpdateRequestDto input)
    {
        return await _masterService.AtualizarMaster(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarReeviarMaster")]
    public async Task<ApiResponse<IEnumerable<MasterResponseDto>>> AtualizarReeviarMaster([FromBody] AtualizarMasterReenviarRequest input)
    {
        return await _masterService.AtualizarReenviarMaster(HttpContext.GetUserSession(), input);
    }

    [HttpPost]
    [Authorize]
    [Route("ExcluirMaster")]
    public async Task<ApiResponse<string>> ExcluirMaster(ExcluirMastersByIdRequest input)
    {
        return await _masterService.ExcluirMaster(HttpContext.GetUserSession(), input);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarArquivosImportacao")]
    public ApiResponse<List<MasterFileResponseDto>> ListFileToImport()
    {
        return _masterService.GetFilesToImport(HttpContext.GetUserSession());
    }

    [HttpPost]
    [Authorize]
    [Route("UploadImportFile")]
    public async Task<ApiResponse<List<MasterResponseDto>>> UploadFileToImport([FromForm]MasterFileImportRequest input, IFormFile file)
    {
        if (file.Length > 0)
        {
            using (var ms = file.OpenReadStream())
            {
                return await _masterService.ImportFile(HttpContext.GetUserSession(), input, ms);
            }
        }

        return new ApiResponse<List<MasterResponseDto>>()
        {
            Dados = null,
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
