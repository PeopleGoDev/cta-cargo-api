using CtaCargo.CctImportacao.Api.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtaCargo.CctImportacao.Api.Infrastructure.Installers
{
    public class RegisterCORS : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAll", 
                    builder => 
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });
        }
    }
}
