using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface IMasterHouseAssociacaoRepository
    {
        void DeleteMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
        void InsertMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
        Task<bool> SaveChanges();
        Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoById(int ciaId, int id);
        Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoByMaster(string master);
        Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociacaoParam(QueryJunction<MasterHouseAssociacao> param);
        void UpdateMasterHouseAssociacao(MasterHouseAssociacao associacao);
    }
}