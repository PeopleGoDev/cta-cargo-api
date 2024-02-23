using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public interface ICertitificadoDigitalSupport
{
    Task<X509Certificate2> GetCertificateCiaAereaAsync(int ciaAereaId);
    //Task<X509Certificate2> GetCertificateAgenteDeCargaAsync(int agenteDeCargaId);
    // Task<X509Certificate2> GetCertificateUsuarioAsync(int usuarioId, bool optionalError = true);
    Task<CctCertificate> GetCertificateForAirCompany(int usuarioId, int airCompanyId);
    Task<CctCertificate> GetCertificateForFreightFowarder(int usuarioId, int freightForwarderId);
}