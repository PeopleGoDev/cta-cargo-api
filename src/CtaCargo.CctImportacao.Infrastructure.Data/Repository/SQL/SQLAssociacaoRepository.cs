using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLAssociacaoRepository : IAssociacaoRepository
{
    private readonly ApplicationDbContext _context;

    public SQLAssociacaoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void InsertMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao)
    {
        _context.MasterHouseAssociacoes.Add(masterHouseAssociacao);
    }

    public void DeleteMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao)
    {
        if (masterHouseAssociacao == null)
        {
            throw new ArgumentNullException(nameof(masterHouseAssociacao));
        }
        masterHouseAssociacao.DataExclusao = DateTime.UtcNow;
        _context.MasterHouseAssociacoes.Update(masterHouseAssociacao);
    }

    public async Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociacaoParam(QueryJunction<MasterHouseAssociacao> param)
    {
        return await _context.MasterHouseAssociacoes
            .Include(x => x.MasterHouseAssociationChildren.Where(s => s.DataExclusao == null))
            .Include("MasterHouseAssociationChildren")
            .Include("MasterHouseAssociationChildren.House")
            .Where(param.ToPredicate())
            .ToListAsync();
    }

    public async Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoById(int ciaId, int id)
    {
        return await _context.MasterHouseAssociacoes
            .FirstOrDefaultAsync(x => x.EmpresaId == ciaId && x.Id == id && x.DataExclusao == null);
    }

    public async Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociationByIdsAsync(int ciaId, int[] ids)
    {
        return await _context.MasterHouseAssociacoes
            .Include(x => x.MasterHouseAssociationChildren.Select(s => s.House))
            .Include(x => x.AgenteDeCargaInfo)
            .Where(x => x.EmpresaId == ciaId && ids.Contains(x.Id) && x.DataExclusao == null)
            .ToListAsync();
    }

    public IEnumerable<string> GetMasterNumbersByAssociationIds(int companyId, int freightFowarderId, int[] ids)
    {
        return _context.MasterHouseAssociacoes
            .Where(x => x.EmpresaId == companyId && x.AgenteDeCargaId == freightFowarderId && ids.Contains(x.Id) && x.DataExclusao == null)
            .Select(x => x.MasterNumber)
            .ToList();
    }

    public IEnumerable<MasterHouseAssociacao> GetMasterHouseAssociationByMasterList(int ciaId, string[] masterList)
    {
        return _context.MasterHouseAssociacoes
            .Include(x => x.MasterHouseAssociationChildren.Where(s => s.DataExclusao == null))
            .Include(x => x.MasterHouseAssociationChildren)
            .Include("MasterHouseAssociationChildren.House")
            .Include(x => x.AgenteDeCargaInfo)
            .Where(x => x.EmpresaId == ciaId && masterList.Contains(x.MasterNumber) && x.DataExclusao == null)
            .ToList();
    }

    public IEnumerable<MasterHouseAssociacao> GetMasterHouseAssociationByMaster(int ciaId, int freightFowarderId, string master)
    {
        return _context.MasterHouseAssociacoes
            .Include(x => x.MasterHouseAssociationChildren.Where(s => s.DataExclusao == null))
            .Include(x => x.MasterHouseAssociationChildren)
            .Include("MasterHouseAssociationChildren.House")
            .Include(x => x.AgenteDeCargaInfo)
            .Where(x => x.EmpresaId == ciaId && x.AgenteDeCargaId == freightFowarderId && x.MasterNumber == master && x.DataExclusao == null)
            .ToList();
    }

    public string GetDocumentNumberByMaster(int ciaId, int freightFowarderId, string master)
    {
        return _context.MasterHouseAssociacoes
            .Where(x => x.EmpresaId == ciaId && x.AgenteDeCargaId == freightFowarderId && x.MasterNumber == master && x.DataExclusao == null)
            .Select(x => x.MessageHeaderDocumentId)
            .FirstOrDefault();
    }

    public async Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoByMaster(string master)
    {
        return await _context.MasterHouseAssociacoes
            .FirstOrDefaultAsync(x => x.MasterNumber == master && x.DataExclusao == null);
    }

    public void UpdateMasterHouseAssociacao(MasterHouseAssociacao associacao)
    {
        _context.MasterHouseAssociacoes.Update(associacao);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public MasterHouseAssociationChild InsertMasterHouseAssociationChild(MasterHouseAssociationChild child)
    {
        _context.MasterHouseAssociationChildren.AddAsync(child);
        return child;
    }

    public MasterHouseAssociationChild UpdateMasterHouseAssociationChild(MasterHouseAssociationChild child)
    {
        _context.MasterHouseAssociationChildren.Update(child);
        return child;
    }
}
