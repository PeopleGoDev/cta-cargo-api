using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface ICiaAereaRepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<CiaAerea>> GetAllCiaAereas(int empresaId);
        Task<CiaAerea> GetCiaAereaById(int ciaId, int id);
        Task<CiaAerea> GetCiaAereaByIataCode(int empresaId, string iataCode);
        void CreateCiaAerea(CiaAerea cia);
        void UpdateCiaAerea(CiaAerea cia);
        void DeleteCiaAerea(CiaAerea cia);
    }
}
