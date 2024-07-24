using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface INcmRepository
    {
        IEnumerable<NCM> GetTopNcm(string like, int top);
        IEnumerable<NCM> GetTopNcmByCode(string code, int top);
        IEnumerable<NCM> GetNcmByCodeList(string[] codes);
    }
}