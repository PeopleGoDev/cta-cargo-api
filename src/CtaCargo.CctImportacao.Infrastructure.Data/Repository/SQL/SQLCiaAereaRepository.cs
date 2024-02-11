using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLCiaAereaRepository : ICiaAereaRepository
{
    private readonly ApplicationDbContext _context;

    public SQLCiaAereaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateCiaAerea(CiaAerea cia)
    {
        _context.CiasAereas.Add(cia);
    }

    public void DeleteCiaAerea(CiaAerea cia)
    {
        _context.CiasAereas.Remove(cia);
    }

    public async Task<IEnumerable<CiaAerea>> GetAllCiaAereas(int empresaId)
    {
        return await _context.CiasAereas
            .Include("CertificadoDigital")
            .Where(x => x.EmpresaId == empresaId && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<CiaAerea> GetCiaAereaById(int id)
    {
        return await _context.CiasAereas
            .Include("CertificadoDigital")
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<CiaAerea> GetCiaAereaByIataCode(int empresaId, string iataCode)
    {
        return await _context.CiasAereas
            .FirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.Numero == iataCode && x.DataExclusao == null);
    }
    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void UpdateCiaAerea(CiaAerea cia)
    {
        _context.Update(cia);
    }
}
