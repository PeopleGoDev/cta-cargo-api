using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
public class NaturezaCargaController: Controller
{
    private readonly INaturezaCargaService _naturezaCargaService;

    public NaturezaCargaController(INaturezaCargaService naturezaCargaService)
    {
        _naturezaCargaService = naturezaCargaService;
    }

    [HttpGet]
    [Authorize]
    [Route("ObterNaturezaCargaPorId")]
    public async Task<ApiResponse<NaturezaCargaResponseDto>> ObterNaturezaCargaPorId(int naturezaCargaId)
    {
        return await _naturezaCargaService.NaturezaCargaPorId(naturezaCargaId);
    }

    [HttpGet]
    [Authorize]
    [Route("ListarNaturezaCarga")]
    public async Task<ApiResponse<IEnumerable<NaturezaCargaResponseDto>>> ListarNaturezaCarga(int empresaId)
    {
        return await _naturezaCargaService.ListarNaturezaCarga(empresaId);
    }

    [HttpPost]
    [Authorize]
    [Route("InserirNaturezaCarga")]
    public async Task<ApiResponse<NaturezaCargaResponseDto>> InserirNaturezaCarga([FromBody] NaturezaCargaInsertRequestDto input)
    {
        return await _naturezaCargaService.InserirNaturezaCarga(input);
    }

    [HttpPost]
    [Authorize]
    [Route("AtualizarNaturezaCarga")]
    public async Task<ApiResponse<NaturezaCargaResponseDto>> AtualizarNaturezaCarga([FromBody] NaturezaCargaUpdateRequestDto input)
    {
        return await _naturezaCargaService.AtualizarNaturezaCarga(input);
    }

    [HttpDelete]
    [Authorize]
    [Route("ExcluirNaturezaCarga")]
    public async Task<ApiResponse<NaturezaCargaResponseDto>> ExcluirPortoIATA(int naturezaCargaId)
    {
        return await _naturezaCargaService.ExcluirNaturezaCarga(naturezaCargaId);
    }

    [HttpGet]
    [Authorize]
    [Route("search")]
    public IEnumerable<NaturezaCarga> GetNcmByDescriptionLike(string q)
    {
        if (q.Trim().Length < 3)
            return default;

        if(q.Trim().Length == 3)
        {
            var result = _naturezaCargaService.GetSpecialInstructionByCode(q);
            if (result != null && result.Count() > 0)
                return result;
        }

        return _naturezaCargaService.GetSpecialInstructionByDescriptionLike(q.Trim());
    }

    [HttpPost]
    [Authorize]
    [Route("searchcodes")]
    public IEnumerable<NaturezaCarga> GetSpecialInstructionByCodes(string[] codes) =>
        _naturezaCargaService.GetSpecialInstructionByCode(codes);

}
