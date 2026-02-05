using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CtaCargo.CctImportacao.Api.Contracts
{
    public interface IServiceRegistration
    {
        void RegisterAppServices(IServiceCollection services, IConfiguration configuration = null);
    }
}
