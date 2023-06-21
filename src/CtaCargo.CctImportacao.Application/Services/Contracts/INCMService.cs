using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface INcmService
    {
        IEnumerable<NCM> GetNcmByDescriptionLike(string like);
        IEnumerable<NCM> GetNcmByCode(string[] codes);
    }
}