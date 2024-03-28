using CtaCargo.CctImportacao.Application.Dtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;

namespace CtaCargo.CctImportacao.Api.Controllers.Session;

public static class ControllerExtensions
{
    public static UserSession GetUserSession(this HttpContext context)
    {
        var identity = context.User?.Identity as ClaimsIdentity;
        if (identity != null)
        {
            return new UserSession()
            {
                CompanyId = int.Parse(identity.FindFirst("CompanyId").Value),
                UserId = int.Parse(identity.FindFirst("UserId").Value),
                UserName = identity.FindFirst(ClaimTypes.Name).Value,
                Environment = identity.FindFirst("Environment").Value
            };
        }
        return null;
    }
}
