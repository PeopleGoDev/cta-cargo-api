using CtaCargo.CctImportacao.Api.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CtaCargo.CctImportacao.Api.Infrastructure.Installers
{
    public class RegisterDependencies : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration = null)
        {
            
        }
    }
}
