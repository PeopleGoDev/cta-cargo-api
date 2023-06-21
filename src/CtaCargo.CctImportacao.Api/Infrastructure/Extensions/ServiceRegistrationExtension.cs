using CtaCargo.CctImportacao.Api.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CtaCargo.CctImportacao.Api.Infrastructure.Extensions
{
    public static class ServiceRegistrationExtension
    {
        public static void AddServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var appServices = typeof(Startup).Assembly.DefinedTypes
                                .Where(svc => typeof(IServiceRegistration).IsAssignableFrom(svc) && !svc.IsInterface && !svc.IsAbstract)
                                .Select(Activator.CreateInstance)
                                .Cast<IServiceRegistration>()
                                .ToList();

            appServices.ForEach(svc => svc.RegisterAppServices(services, configuration));
        }
    }
}
