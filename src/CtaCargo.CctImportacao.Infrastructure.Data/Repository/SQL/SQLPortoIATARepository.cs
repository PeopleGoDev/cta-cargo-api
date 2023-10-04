using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLPortoIATARepository : IPortoIATARepository
{
    private readonly ApplicationDbContext _context;

    public SQLPortoIATARepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreatePortoIATA(PortoIata portoIATA)
    {
        if (portoIATA == null)
        {
            throw new ArgumentNullException(nameof(portoIATA));
        }

        _context.PortosIATA.Add(portoIATA);
    }

    public void DeletePortoIATA(PortoIata portoIATA)
    {
        if (portoIATA == null)
        {
            throw new ArgumentNullException(nameof(portoIATA));
        }
        portoIATA.DataExclusao = DateTime.UtcNow;
        _context.PortosIATA.Update(portoIATA);
    }

    public async Task<IEnumerable<PortoIata>> GetAllPortosIATA(int empresaId)
    {
        return await _context.PortosIATA.Where(
            x => x.EmpresaId == empresaId && 
            x.DataExclusao == null).OrderByDescending(x => x.Codigo).ToListAsync();
    }

    public async Task<PortoIata> GetPortoIATAById(int ciaId, int portoIATAId)
    {
        return await _context.PortosIATA
            .FirstOrDefaultAsync(x => x.EmpresaId==ciaId && x.Id == portoIATAId && x.DataExclusao == null);
    }

    public async Task<int?> GetPortoIATAIdByCodigo(string codigo)
    {
        return await _context.PortosIATA
            .Where(x => x.Codigo == codigo && x.DataExclusao == null)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public PortoIata GetPortoIATAByCode(int empresaId, string codigo)
    {
        return _context.PortosIATA.FirstOrDefault(x =>
            x.EmpresaId == empresaId &&
            x.Codigo == codigo &&
            x.DataExclusao == null);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void UpdatePortoIATA(PortoIata portoIATA)
    {
        _context.Update(portoIATA);
    }
}
