using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.SharedKernel.Erros;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Api.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (DomainException domainException)
            {
                await HandleDomainExceptionAsync(httpContext, domainException);
            }
            catch (BusinessException exception)
            {
                await HandleBusinessExceptionAsync(httpContext, exception);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleDomainExceptionAsync(HttpContext httpContext, DomainException domainException)
        {
            var apiResponse = new ApiResponse<object>
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = new List<Notificacao>
                {
                    new Notificacao(Domain.Enums.CodigoNotificacao.ErroInesperado, MensagemErro.ErroInesperado)
                }
            };

            var response = JsonConvert.SerializeObject(apiResponse);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleBusinessExceptionAsync(HttpContext httpContext, BusinessException domainException)
        {
            var apiResponse = new ApiResponse<object>
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = new List<Notificacao>
                {
                    new Notificacao( Domain.Enums.CodigoNotificacao.Business, domainException.Message)
                }
            };

            var response = JsonConvert.SerializeObject(apiResponse);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var apiResponse = new ApiResponse<object>
            {
                Sucesso = false,
                Dados = null,
                Notificacoes = new List<Notificacao>
                {
                    new Notificacao(Domain.Enums.CodigoNotificacao.ErroInesperado, MensagemErro.ErroInesperado)
                }
            };

            var response = JsonConvert.SerializeObject(apiResponse);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(response);
        }

    }
}
