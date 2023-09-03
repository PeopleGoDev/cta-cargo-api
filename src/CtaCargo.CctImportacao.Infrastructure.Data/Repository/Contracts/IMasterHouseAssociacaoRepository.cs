using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IMasterHouseAssociacaoRepository
    {
        void DeleteMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
        void InsertMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao);
        Task<bool> SaveChanges();
        Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoById(int ciaId, int id);
        Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoByMaster(string master);
        Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociacaoParam(Expression<Func<MasterHouseAssociacao, bool>> predicate);
        void UpdateMasterHouseAssociacao(MasterHouseAssociacao associacao);
    }
}