using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class NcmService : INcmService
    {
        private INcmRepository _ncmRepository;

        public NcmService(INcmRepository ncmRepository)
        {
            _ncmRepository = ncmRepository;
        }

        public IEnumerable<NCM> GetNcmByDescriptionLike(string like)
        {
            return _ncmRepository.GetTopNcm(like, 5);
        }

        public IEnumerable<NCM> GetNcmByCode(string[] codes)
        {
            return _ncmRepository.GetNcmByCodeList(codes);
        }
    }
}
