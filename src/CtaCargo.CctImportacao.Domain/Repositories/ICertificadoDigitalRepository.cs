using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface ICertificadoDigitalRepository
    {
        void CreateCertificadoDigital(CertificadoDigital certificado);
        void DeleteCertificadoDigital(CertificadoDigital certificado);
        Task<IEnumerable<CertificadoDigital>> GetAllCertificadosDigital(int empresaId);
        Task<IEnumerable<CertificadoDigital>> GetAllCertificadosDigitalWithExpiration(int empresaId);
        Task<CertificadoDigital> GetCertificadoDigitalById(int id);
        Task<CertificadoDigital> GetCertificadoDigitalBySerialNumber(int empresaId, string serialNumber);
        Task<bool> SaveChanges();
        void UpdateCertificadoDigital(CertificadoDigital certificado);
    }
}