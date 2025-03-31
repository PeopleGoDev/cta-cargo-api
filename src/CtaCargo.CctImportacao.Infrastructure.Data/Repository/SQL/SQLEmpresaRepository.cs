using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLEmpresaRepository : IEmpresaRepository
{
    private readonly ApplicationDbContext _context;

    public SQLEmpresaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Empresa>> GetAllEmpresasAsync()
    {
        return await _context.Empresas
            .Where(x => x.DataExclusao == null).ToListAsync();
    }

    public async Task<Empresa> GetEmpresasByIdAsync(int id)
    {
        return await _context.Empresas
            .Where(x => x.DataExclusao == null && x.Id == id)
            .FirstOrDefaultAsync();
    }
}