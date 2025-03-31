using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories;

public interface IAssociacaoRepository
{
    void DeleteMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
    void InsertMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
    Task<bool> SaveChangesAsync();
    Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoById(int ciaId, int id);
    Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociationByIdsAsync(int ciaId, int[] ids);
    Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoByMaster(string master);
    Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociacaoParam(QueryJunction<MasterHouseAssociacao> param);
    void UpdateMasterHouseAssociacao(MasterHouseAssociacao associacao);
    MasterHouseAssociationChild InsertMasterHouseAssociationChild(MasterHouseAssociationChild child);
    MasterHouseAssociationChild UpdateMasterHouseAssociationChild(MasterHouseAssociationChild child);
    IEnumerable<string> GetMasterNumbersByAssociationIds(int companyId, int freightFowarderId, int[] ids);
    IEnumerable<MasterHouseAssociacao> GetMasterHouseAssociationByMasterList(int ciaId, string[] masterList);
    IEnumerable<MasterHouseAssociacao> GetMasterHouseAssociationByMaster(int ciaId, int freightFowarderId, string master);
    string GetDocumentNumberByMaster(int ciaId, int freightFowarderId, string master);
}