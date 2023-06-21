using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface INaturezaCargaRepository
    {
        void CreateNaturezaCarga(NaturezaCarga narurezaCarga);
        void DeleteNaturezaCarga(NaturezaCarga narurezaCarga);
        Task<IEnumerable<NaturezaCarga>> GetAllNaturezaCarga(int empresaId);
        Task<NaturezaCarga> GetNaturezaCargaById(int id);
        Task<int?> GetNaturezaCargaIdByCodigo(string codigo);
        Task<bool> SaveChanges();
        void UpdateNaturezaCarga(NaturezaCarga naturezaCarga);
    }
}