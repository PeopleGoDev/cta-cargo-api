using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IUldMasterRepository
    {
        Task<UldMaster> GetUldMasterById(int id);
        Task<List<UldMaster>> GetUldMasterByIdList(List<int> ids);
        Task<int> CreateUldMasterList(List<UldMaster> ulds);
        void UpdateUldMaster(UldMaster uld);
        void DeleteUldMasterList(List<UldMaster> ulds, int userId);
        Task<List<UldMaster>> GetUldMasterByMasterId(int uldMasterId);
        Task<UldMaster> GetUldMasterByMasterNumber(int ciaId, string masterNumber);
        Task<List<UldMaster>> GetUldMasterByLinha(int vooId, string linha);
        Task<List<UldMasterNumeroQuery>> GetUldMasterByVooId(int vooId);
        Task<List<UldMasterNumeroQuery>> GetUldMasterByTrechoId(int trechoId);
        Task<List<UldMaster>> GetUldMasterByTag(UldMasterDeleteByTagInput input);
        Task<List<MasterNumeroUldSumario>> GetUldMasterSumarioByVooId(int vooId);
        int SaveChanges();
    }
}