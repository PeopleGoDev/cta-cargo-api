using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLConfiguraRepository : IConfiguraRepository
{
    private readonly ApplicationDbContext _context;

    public SQLConfiguraRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Configura> GetAllAvailableConfiguration()
    {
        return _context.Configuracoes.Where(
            x => x.ConfiguracaoNome == "IMPORTAXML" &&
            x.DataExclusao == null).ToList();
    }
}
