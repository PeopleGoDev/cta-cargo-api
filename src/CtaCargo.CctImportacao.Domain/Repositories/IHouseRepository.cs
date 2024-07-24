using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model;

namespace CtaCargo.CctImportacao.Domain.Repositories
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
        Task<int?> GetHouseIdByNumberValidate(int ciaId, string numero, DateTime dataLimite);
        Task<bool> SaveChanges();
        void UpdateHouse(House house);
    }
}