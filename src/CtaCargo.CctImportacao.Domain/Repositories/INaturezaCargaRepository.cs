using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories;

public interface INaturezaCargaRepository
{
    void CreateNaturezaCarga(NaturezaCarga narurezaCarga);
    void DeleteNaturezaCarga(NaturezaCarga narurezaCarga);
    Task<List<NaturezaCarga>> GetAllNaturezaCarga(int empresaId);
    Task<NaturezaCarga> GetNaturezaCargaById(int id);
    Task<int?> GetNaturezaCargaIdByCodigo(string codigo);
    IEnumerable<NaturezaCarga> GetTopSpecialInstruction(string like, int top);
    IEnumerable<NaturezaCarga> GetTopSpecialInstructionByCode(string code, int top);
    IEnumerable<NaturezaCarga> GetSpecialInstructionByCodeList(string[] codes);
    Task<bool> SaveChanges();
    void UpdateNaturezaCarga(NaturezaCarga naturezaCarga);
}