using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Domain.Repositories;

public interface IConfiguraRepository
{
    List<Configura> GetAllAvailableConfiguration();
    Empresa GetCompanyById(int id);
    Empresa GetCompanyByTaxId(string taxId);
    void AddCompany(Empresa empresa);
    void UpdateCompany(Empresa empresa);
    void SaveCompany();
}