using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories;
public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetAllEmpresasAsync();
    Task<Empresa> GetEmpresasByIdAsync(int id);
}
