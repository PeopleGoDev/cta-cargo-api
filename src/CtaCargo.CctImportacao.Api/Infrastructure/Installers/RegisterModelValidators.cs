using CtaCargo.CctImportacao.Api.Configurations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtaCargo.CctImportacao.Api.Infrastructure.Installers
{
    public class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration = null)
        {
            //services.AddTransient<IValidator<CriarHouseRequest>, CriarHouseRequestValidator>();
            //services.AddTransient<IValidator<EditarHouseRequest>, EditarHouseRequestValidator>();

            // Desabilitando a validação automática de ModelState do ASP.NET Core
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        }
    }
}
