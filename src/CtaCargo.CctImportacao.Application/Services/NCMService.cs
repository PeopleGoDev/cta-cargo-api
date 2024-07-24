using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Repositories;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Services;

public class NcmService : INcmService
{
    private INcmRepository _ncmRepository;

    public NcmService(INcmRepository ncmRepository) =>
        _ncmRepository = ncmRepository;

    public IEnumerable<NCM> GetNcmByDescriptionLike(string like) =>
        _ncmRepository.GetTopNcm(like, 5);
    

    public IEnumerable<NCM> GetNcmByCodeStart(string code) =>
        _ncmRepository.GetTopNcmByCode(code, 5);

    public IEnumerable<NCM> GetNcmByCode(string[] codes) =>
        _ncmRepository.GetNcmByCodeList(codes);

}
