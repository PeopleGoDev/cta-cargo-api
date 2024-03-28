using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLNaturezaCargaRepository : INaturezaCargaRepository
{
    private readonly ApplicationDbContext _context;

    public SQLNaturezaCargaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateNaturezaCarga(NaturezaCarga narurezaCarga)
    {
        _context.NaturezasCarga.Add(narurezaCarga);
    }

    public void DeleteNaturezaCarga(NaturezaCarga narurezaCarga)
    {
        narurezaCarga.DataExclusao = DateTime.UtcNow;

        _context.NaturezasCarga.Update(narurezaCarga);
    }

    public async Task<List<NaturezaCarga>> GetAllNaturezaCarga(int empresaId) => await _context.NaturezasCarga
            .Where(x => x.EmpresaId == empresaId && x.DataExclusao == null)
            .OrderByDescending(x => x.Codigo)
            .ToListAsync();
    public async Task<NaturezaCarga> GetNaturezaCargaById(int id) => await _context.NaturezasCarga
        .FirstOrDefaultAsync(x => x.Id == id && x.DataExclusao == null);

    public async Task<int?> GetNaturezaCargaIdByCodigo(string codigo) => await _context.NaturezasCarga
        .Where(x => x.Codigo == codigo && x.DataExclusao == null)
        .Select(x => x.Id)
        .FirstOrDefaultAsync();

    public IEnumerable<NaturezaCarga> GetTopSpecialInstruction(string like, int top) =>
        _context.NaturezasCarga.Where(x => x.DataExclusao == null && x.Descricao.Contains(like))
        .Take(top);


    public IEnumerable<NaturezaCarga> GetTopSpecialInstructionByCode(string code, int top) =>
        _context.NaturezasCarga.Where(x => x.DataExclusao == null && x.Codigo == code)
        .Take(top);

    public IEnumerable<NaturezaCarga> GetSpecialInstructionByCodeList(string[] codes) =>
        _context.NaturezasCarga.Where(x => x.DataExclusao == null && codes.Contains(x.Codigo));

    public async Task<bool> SaveChanges() => (await _context.SaveChangesAsync() >= 0);

    public void UpdateNaturezaCarga(NaturezaCarga naturezaCarga)
    {
        _context.Update(naturezaCarga);
    }

}
