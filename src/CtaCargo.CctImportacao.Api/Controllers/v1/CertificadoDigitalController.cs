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
    public class CertificadoDigitalController : Controller
    {
        private readonly ICertificadoDigitalService _certificadoDigitalService;

        public CertificadoDigitalController(ICertificadoDigitalService certificadoDigitalService)
        {
            _certificadoDigitalService = certificadoDigitalService;
        }

        [HttpGet]
        [Authorize]
        [Route("ListarCertificadosDigitais")]
        public async Task<ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>> ListarCertificadosDigitais(int empresaId)
        {
            return await _certificadoDigitalService.ListarCertificadosDigitais(empresaId);
        }
    }
}
