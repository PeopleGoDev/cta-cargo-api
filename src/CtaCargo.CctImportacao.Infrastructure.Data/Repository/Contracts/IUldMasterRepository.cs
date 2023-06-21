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
        Task<int> UpdateUldMaster(UldMaster uld);
        Task<int> DeleteUldMasterList(List<UldMaster> ulds);
        Task<List<UldMaster>> GetUldMasterByMasterId(int uldMasterId);
        Task<List<UldMaster>> GetUldMasterByLinha(int vooId, string linha);
        Task<List<UldMasterNumeroQuery>> GetUldMasterByVooId(int vooId);
        Task<List<UldMaster>> GetUldMasterByTag(UldMasterDeleteByTagInput input);
        Task<List<MasterNumeroUldSumario>> GetUldMasterSumarioByVooId(int vooId);
    }
}