using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            string search = q.Replace(".", "");

            Regex regex = new Regex(@"^\d{2,8}$");
            if(regex.Match(search).Success)
            {
                return _NCMService.GetNcmByCodeStart(search);
            }

            if (q == null || q.Length < 3)
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
