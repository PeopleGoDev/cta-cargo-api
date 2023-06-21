using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class PortoIATAController: Controller
    {
        private readonly IPortoIATAService _portoIATAService;

        public PortoIATAController(IPortoIATAService portoIATAService)
        {
            _portoIATAService = portoIATAService;
        }

        [HttpGet]
        [Authorize]
        [Route("ObterPortoIATAPorId")]
        public async Task<ApiResponse<PortoIATAResponseDto>> ObterPortoIATAPorId(int portoIATAId)
        {
            return await _portoIATAService.PortoIATAPorId(portoIATAId);
        }

        [HttpGet]
        [Authorize]
        [Route("ListarPortosIATA")]
        public async Task<ApiResponse<IEnumerable<PortoIATAResponseDto>>> ListarPortosIATA(int empresaId)
        {
            return await _portoIATAService.ListarPortosIATA(empresaId);
        }

        [HttpPost]
        [Authorize]
        [Route("InserirPortoIATA")]
        public async Task<ApiResponse<PortoIATAResponseDto>> InserirPortoIATA([FromBody]PortoIATAInsertRequestDto input)
        {
            return await _portoIATAService.InserirPortoIATA(input);
        }

        [HttpPost]
        [Authorize]
        [Route("AtualizarPortoIATA")]
        public async Task<ApiResponse<PortoIATAResponseDto>> AtualizarPortoIATA([FromBody]PortoIATAUpdateRequestDto input)
        {
            return await _portoIATAService.AtualizarPortoIATA(input);
        }

        [HttpDelete]
        [Authorize]
        [Route("ExcluirPortoIATA")]
        public async Task<ApiResponse<PortoIATAResponseDto>> ExcluirPortoIATA(int portoIATAId)
        {
            return await _portoIATAService.ExcluirPortoIATA(portoIATAId);
        }
    }
}
