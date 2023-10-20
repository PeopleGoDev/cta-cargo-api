using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IMasterRepository
    {
        void CreateMaster(int companyId, Master master);
        void DeleteMaster(int companyId, Master master);
        Task<IEnumerable<Master>> GetAllMasters(Expression<Func<Master, bool>> predicate);
        Task<IEnumerable<Master>> GetAllMastersByDataCriacao(int companyId, DateTime dataEmissao);
        Task<IEnumerable<MasterVooQuery>> GetMastersVoo(int companyId, int vooId);
        Task<Master> GetMasterById(int companyId, int masterId);
        Task<IEnumerable<Master>> GetMasterByIds(int companyId, int[] masterIds);
        Task<Master> GetMasterForUploadById(int companyId, int masterId);
        Task<List<Master>> GetMastersForUploadById(int companyId, int[] masterArrayId);
        Task<List<Master>> GetMastersForUploadByVooId(int companyId, int vooId);
        Task<List<Master>> GetMastersForUploadSelected(int companyId, int[] masterIdList);
        Task<IEnumerable<MasterListaQuery>> GetMastersListaByVooId(int companyId, int vooId);
        Task<Master> GetMasterIdByNumber(int companyId, int? vooId, string masterNumber);
        Task<Master> GetMasterByNumber(int companyId, string masterNumber);
        Task<SituacaoRFBQuery> GetMasterRFBStatus(int masterId);
        Task<int?> GetMasterIdByNumberValidate(int ciaId, string numero, DateTime dataLimite);
        Task<bool> SaveChanges();
        void ClearChangeTracker();
        void UpdateMaster(Master master);
        FileImport GetMasterFileImportById(int ciaId, int fileId);
        List<FileImport> GetMasterFileImportList(int ciaId);
    }
}