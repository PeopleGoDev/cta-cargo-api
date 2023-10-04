using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IHouseRepository
    {
        void CreateHouse(House house);
        void DeleteHouse(House house);
        Task<IEnumerable<House>> GetAllHouses(Expression<Func<House, bool>> predicate);
        Task<IEnumerable<House>> GetAllHousesByDataCriacao(int companyId, DateTime dataEmissao);
        List<House> GetHouseForUploading(QueryJunction<House> param);
        Task<House> GetHouseById(int ciaId, int houseId);
        Task<House> GetHouseByIdForExclusionUpload(int ciaId, int houseId);
        string[] GetMastersByParam(QueryJunction<House> param);
        IEnumerable<House> GetHouseByMasterList(string[] masters);
        Task<bool> SaveChanges();
        void UpdateHouse(House house);
    }
}