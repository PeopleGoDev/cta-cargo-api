using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IVooRepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Voo>> GetAllVoos(QueryJunction<Voo> param);
        Task<Voo> GetVooById(int vooId);
        Task<Voo> GetVooIdByDataVooNumero(DateTime dataVoo, string numeroVoo);
        Task<SituacaoRFBQuery> GetVooRFBStatus(int vooId);
        Task<Voo> GetVooWithULDById(int companyId, int vooId);
        Task<IEnumerable<VooListaQuery>> GetVoosByDate(QueryJunction<Voo> param);
        void CreateVoo(Voo voo);
        void UpdateVoo(Voo voo);
        void DeleteVoo(Voo voo);
    }
}
