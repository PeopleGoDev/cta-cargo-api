using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SqlHouseRepository : IHouseRepository
{
    private readonly ApplicationDbContext _context;

    public SqlHouseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateHouse(House house)
    {
        if (house == null)
        {
            throw new ArgumentNullException(nameof(house));
        }

        _context.Houses.Add(house);
    }

    public void DeleteHouse(House house)
    {
        if (house == null)
        {
            throw new ArgumentNullException(nameof(house));
        }
        house.DataExclusao = DateTime.UtcNow;
        _context.Houses.Update(house);
    }

    public async Task<IEnumerable<House>> GetAllHouses(Expression<Func<House, bool>> predicate)
    {
        return await _context.Houses.Where(predicate).ToListAsync();
    }

    public List<House> GetHouseForUploading(QueryJunction<House> param)
    {
        return _context.Houses
            .Include("AgenteDeCargaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .Where(param.ToPredicate())
            .OrderBy(x => x.MasterNumeroXML)
            .ToList();
    }

    public async Task<IEnumerable<House>> GetAllHousesByDataCriacao(int companyId, DateTime dataEmissao)
    {
        DateTime dataInicial = new DateTime(
            dataEmissao.Year,
            dataEmissao.Month,
            dataEmissao.Day,
            0, 0, 0, 0);
        DateTime dataFinal = new DateTime(
            dataEmissao.Year,
            dataEmissao.Month,
            dataEmissao.Day,
            23, 59, 59, 997);
        return await _context.Houses.Where(x =>
       x.DataExclusao == null &&
       x.EmpresaId == companyId &&
       x.CreatedDateTimeUtc >= dataInicial &&
       x.CreatedDateTimeUtc <= dataFinal).ToListAsync();
    }

    public async Task<House> GetHouseById(int ciaId, int houseId)
    {
        return await _context.Houses.FirstOrDefaultAsync(x => x.EmpresaId == ciaId && x.Id == houseId &&
        x.DataExclusao == null);
    }

    public async Task<House> GetHouseByIdForExclusionUpload(int ciaId, int houseId)
    {
        return await _context.Houses
            .Include("AgenteDeCargaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .FirstOrDefaultAsync(x => x.EmpresaId == ciaId && x.Id == houseId && x.DataExclusao == null);
    }

    public string[] GetMastersByParam(QueryJunction<House> param)
    {
        return _context.Houses
            .Where(param.ToPredicate())
            .GroupBy(x => x.MasterNumeroXML)
            .Select(x => x.Key)
            .ToArray();
    }

    public IEnumerable<House> GetHouseByMasterList(string[] masters)
    {
        return _context.Houses
            .Where(x => masters.Contains(x.MasterNumeroXML) &&  x.DataExclusao == null);
    }

    public async Task<int?> GetHouseIdByNumberValidate(int ciaId, string numero, DateTime dataLimite)
    {
        return await _context.Houses
            .Where(x => x.EmpresaId == ciaId && x.Numero == numero && x.CreatedDateTimeUtc >= dataLimite && x.DataExclusao == null)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void UpdateHouse(House house)
    {
        _context.Update(house);
    }
}
