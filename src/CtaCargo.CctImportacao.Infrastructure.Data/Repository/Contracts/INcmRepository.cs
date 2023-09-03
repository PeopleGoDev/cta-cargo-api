using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface INcmRepository
    {
        IEnumerable<NCM> GetTopNcm(string like, int top);
        IEnumerable<NCM> GetTopNcmByCode(string code, int top);
        IEnumerable<NCM> GetNcmByCodeList(string[] codes);
    }
}