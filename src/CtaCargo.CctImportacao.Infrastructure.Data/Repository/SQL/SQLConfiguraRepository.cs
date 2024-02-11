using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;

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

    public Empresa GetCompanyById(int id)
    {
        return _context.Empresas
            .FirstOrDefault(x => x.Id == id);
    }

    public Empresa GetCompanyByTaxId(string taxId)
    {
        return _context.Empresas
            .FirstOrDefault(x => x.CNPJ == taxId && x.DataExclusao == null);
    }

    public void AddCompany(Empresa empresa)
    {
        _context.Empresas.Add(empresa);
    }

    public void UpdateCompany(Empresa empresa)
    {
        _context.Empresas.Update(empresa);
    }

    public void SaveCompany()
    {
        _context.SaveChanges();
    }
}
