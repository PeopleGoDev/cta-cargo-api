using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLNcmRepository : INcmRepository
{
    private readonly ApplicationDbContext _context;

    public SQLNcmRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<NCM> GetTopNcm(string like, int top) => _context.NCMs.Where(x => x.Seleciona == true && x.Descricao.Contains(like))
            .Take(top);

    public IEnumerable<NCM> GetTopNcmByCode(string code, int top) => _context.NCMs.Where(x => x.Seleciona == true && x.CodigoNumero.StartsWith(code))
            .Take(top);

    public IEnumerable<NCM> GetNcmByCodeList(string[] codes) => _context.NCMs.Where(x => x.Seleciona == true && codes.Contains(x.Codigo));
}
