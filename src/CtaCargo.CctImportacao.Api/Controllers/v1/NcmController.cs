using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class NcmController : Controller
    {
        private INcmService _NCMService;

        public NcmController(INcmService nCMService)
        {
            _NCMService = nCMService;
        }

        [HttpGet]
        [Authorize]
        [Route("search")]
        public IEnumerable<NCM> GetNcmByDescriptionLike(string q)
        {
            
            if(q == null || q.Length < 3 )
                return new List<NCM>();

            return _NCMService.GetNcmByDescriptionLike(q);
        }

        [HttpPost]
        [Authorize]
        [Route("searchcodes")]
        public IEnumerable<NCM> GetNcmByDescriptionCodes(string[] codes)
        {
            return _NCMService.GetNcmByCode(codes);
        }
    }
}
