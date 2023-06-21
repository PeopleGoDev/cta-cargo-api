using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support.Contracts
{
    public interface ICertitificadoDigitalSupport
    {
        Task<X509Certificate2> GetCertificateCiaAereaAsync(int ciaAereaId);
        Task<X509Certificate2> GetCertificateAgenteDeCargaAsync(int agenteDeCargaId);
        Task<X509Certificate2> GetCertificateUsuarioAsync(int usuarioId);

    }
}