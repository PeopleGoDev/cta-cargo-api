using CtaCargo.CctImportacao.Application.Dtos;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public interface ICertitificadoDigitalSupport
{
    Task<X509Certificate2> GetCertificateCiaAereaAsync(UserSession userSession, int ciaAereaId);
    Task<CctCertificate> GetCertificateForAirCompany(UserSession userSession, int airCompanyId);
    Task<CctCertificate> GetCertificateForFreightFowarder(UserSession userSession, int freightForwarderId);
}