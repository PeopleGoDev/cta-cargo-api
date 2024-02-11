using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
    public class SQLCertificadoDigitalRepository : ICertificadoDigitalRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLCertificadoDigitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCertificadoDigital(CertificadoDigital certificado)
        {
            _context.Certificados.Add(certificado);
        }

        public void DeleteCertificadoDigital(CertificadoDigital certificado)
        {
            _context.Certificados.Remove(certificado);
        }

        public async Task<IEnumerable<CertificadoDigital>> GetAllCertificadosDigital(int empresaId)
        {
            return await _context.Certificados
                .Include("UsuarioCriacaoInfo")
                .Include("UsuarioModificacaoInfo")
                .Where(x => x.EmpresaId == empresaId && x.DataVencimento > DateTime.UtcNow &&  x.DataExclusao == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<CertificadoDigital>> GetAllCertificadosDigitalWithExpiration(int empresaId)
        {
            return await _context.Certificados
                .Where(x => x.EmpresaId == empresaId && x.DataExclusao == null)
                .ToListAsync();
        }

        public async Task<CertificadoDigital> GetCertificadoDigitalById(int id)
        {
            return await _context.Certificados.FindAsync(id);
        }

        public async Task<CertificadoDigital> GetCertificadoDigitalBySerialNumber(int empresaId, string serialNumber)
        {
            return await _context.Certificados
                .FirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.SerialNumber == serialNumber && x.DataExclusao == null);
        }

        public void UpdateCertificadoDigital(CertificadoDigital certificado)
        {
            _context.Update(certificado);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
