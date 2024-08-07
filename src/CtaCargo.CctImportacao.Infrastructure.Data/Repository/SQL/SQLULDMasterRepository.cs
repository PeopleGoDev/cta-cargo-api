﻿using CtaCargo.CctImportacao.Domain.Dtos;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLUldMasterRepository : IUldMasterRepository
{
    private readonly ApplicationDbContext _context;
    public SQLUldMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<UldMaster> GetUldMasterById(int id)
    {
        return await _context.ULDMasters.Where(x => x.Id == id && x.DataExclusao == null)
            .FirstOrDefaultAsync();
    }
    public async Task<List<UldMaster>> GetUldMasterByIdList(List<int> ids)
    {
        IQueryable<UldMaster> query = null;

        foreach (var id in ids)
        {
            var queryForID =
                from o in _context.ULDMasters
                where o.Id == id
                select o;
            if (query == null)
            {
                query = queryForID;
            }
            else
            {
                query = query.Union(queryForID);
            }
        }

        return await query.ToListAsync();
    }
    public async Task<int> CreateUldMasterList(List<UldMaster> ulds)
    {
        _context.ULDMasters.AddRange(ulds);
        return await _context.SaveChangesAsync();
    }
    public void UpdateUldMaster(UldMaster uld)
    {
        _context.ULDMasters.Update(uld);
    }
    public void DeleteUldMasterList(List<UldMaster> ulds, int userId)
    {
        DateTime nowUtc = DateTime.UtcNow;
        
        foreach(var uld in ulds)
        {
            nowUtc = nowUtc.AddSeconds(1);
            uld.DataExclusao = nowUtc;
            uld.ModificadoPeloId = userId;
            _context.ULDMasters.UpdateRange(ulds);
        };

    }
    public async Task<List<UldMaster>> GetUldMasterByMasterId(int masterId)
    {
        return await _context.ULDMasters.Where(x => x.MasterId == masterId).ToListAsync();
    }

    public async Task<UldMaster> GetUldMasterByMasterNumber(int ciaId, string masterNumber)
    {
        return await _context.ULDMasters.Where(x => x.EmpresaId == ciaId && 
            x.MasterNumero == masterNumber && 
            x.DataExclusao == null)
            .Include(x => x.VooTrecho)
            .Include("VooTrecho.VooInfo")
            .OrderByDescending(x => x.CreatedDateTimeUtc)
            .FirstOrDefaultAsync();
    }

    public async Task<List<UldMaster>> GetUldListByMasterNumberVooId(int ciaId, string masterNumber, int vooId)
    {
        return await _context.ULDMasters.Where(x => x.EmpresaId == ciaId &&
            x.VooId == vooId &&
            x.MasterNumero == masterNumber &&
            x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<List<UldMaster>> GetUldByMasterNumberForUpload(int ciaId, string masterNumber)
    {
        return await _context.ULDMasters.Where(x => x.EmpresaId == ciaId &&
            x.MasterNumero == masterNumber &&
            x.DataExclusao == null &&
            x.MasterId == null)
            .ToListAsync();
    }

    public async Task<List<UldMaster>> GetUldMasterByLinha(int vooId, string linha)
    {
        string uldchar = linha.Substring(0, 3);
        string uldid = linha.Substring(3, 5);
        string uldpri = linha.Substring(8,2);

        return await _context.ULDMasters
            .Where(x => x.VooId == vooId && x.ULDCaracteristicaCodigo == uldchar && x.ULDId == uldid && x.ULDIdPrimario == uldpri && x.DataExclusao == null).ToListAsync();
    }
    public async Task<List<UldMasterNumeroQuery>> GetUldMasterByVooId(int vooId)
    {
        var result = await _context.ULDMasters
            .Include("UsuarioCriacaoInfo")
            .Include("MasterInfo")
            .Where(x => x.VooId == vooId && x.DataExclusao == null)
            .Select(c => new UldMasterNumeroQueryChildren
            {
                Id = c.Id,
                DataCricao = c.CreatedDateTimeUtc,
                MasterNumero = c.MasterNumero,
                Peso = c.Peso,
                PesoUnidade = c.MasterInfo.PesoTotalBrutoUN,
                QuantidadePecas = c.QuantidadePecas,
                UldCaracteristicaCodigo = c.ULDCaracteristicaCodigo,
                UldId = c.ULDId,
                UldIdPrimario = c.ULDIdPrimario,
                UsuarioCriacao = c.UsuarioCriacaoInfo.Nome, 
                TotalParcial = c.TotalParcial
            }).ToListAsync();

        var result1 = result
            .GroupBy(g => new { g.UldCaracteristicaCodigo, g.UldId, g.UldIdPrimario })
            .Select(s => new UldMasterNumeroQuery
            {
                ULDCaracteristicaCodigo = s.Key.UldCaracteristicaCodigo,
                ULDId = s.Key.UldId,
                ULDIdPrimario = s.Key.UldIdPrimario,
                ULDs = s.ToList()
            }).ToList();

        return result1;
    }

    public async Task<List<UldMasterNumeroQuery>> GetUldMasterByTrechoId(int trechoId)
    {
        var result = await _context.ULDMasters
            .Include("UsuarioCriacaoInfo")
            .Where(x => x.VooTrechoId == trechoId && x.DataExclusao == null)
            .Select(c => new UldMasterNumeroQueryChildren
            {
                Id = c.Id,
                DataCricao = c.CreatedDateTimeUtc,
                MasterNumero = c.MasterNumero,
                Peso = c.Peso,
                PesoUnidade = c.PesoUN,
                QuantidadePecas = c.QuantidadePecas,
                UldCaracteristicaCodigo = c.ULDCaracteristicaCodigo,
                UldId = c.ULDId,
                UldIdPrimario = c.ULDIdPrimario,
                UsuarioCriacao = c.UsuarioCriacaoInfo.Nome,
                TotalParcial = c.TotalParcial,
                Transferencia = c.Tranferencia,
                MasterId = c.MasterId,
                AeroportoOrigem = c.PortOfOrign,
                AeroportoDestino = c.PortOfDestiny,
                DescricaoMercadoria = c.SummaryDescription
            }).ToListAsync();

        var result1 = result
            .GroupBy(g => new { g.UldCaracteristicaCodigo, g.UldId, g.UldIdPrimario })
            .Select(s => new UldMasterNumeroQuery
            {
                ULDCaracteristicaCodigo = s.Key.UldCaracteristicaCodigo,
                ULDId = s.Key.UldId,
                ULDIdPrimario = s.Key.UldIdPrimario,
                ULDs = s.ToList()
            }).ToList();

        return result1;
    }
    public async Task<List<UldMaster>> GetUldMasterByTag(UldMasterDeleteByTagInput input)
    {
        var result = await _context.ULDMasters
            .Where(x => x.VooId == input.VooId && x.ULDId == input.ULDId && 
            x.ULDCaracteristicaCodigo == input.ULDCaracteristicaCodigo && 
            x.ULDIdPrimario == input.ULDIdPrimario && x.DataExclusao == null)
            .ToListAsync();

        return result;
    }
    public async Task<List<MasterNumeroUldSumario>> GetUldMasterSumarioByVooId(int vooId)
    {
        var result = await _context.ResumoVooUldsView
            .Where(x => x.VooId == vooId)
            .ToListAsync();

        var result1 = result
            .GroupBy(g => new { g.MasterNumero,  g.TotalPecas, g.PesoTotalBruto, g.PesoTotalBrutoUN })
            .Select(s => new MasterNumeroUldSumario
            {
                MasterNumero = s.Key.MasterNumero,
                MasterPecas = s.Key.TotalPecas,
                MasterPeso = s.Key.PesoTotalBruto, 
                MasterPesoUnidade = s.Key.PesoTotalBrutoUN,
                Ulds = s.ToList()
                    .Select(i => new MasterNumeroUldSumarioChildren
                    {
                        Peso = Convert.ToDouble(i.Peso.Value),
                        QuantidadePecas = Convert.ToInt32(i.QuantidadePecas),
                        UldNumero = i.ULDCaracteristicaCodigo + i.ULDId + i.ULDIdPrimario,
                        PesoUnidade = s.Key.PesoTotalBrutoUN,
                        TotalParcial = i.TotalParcial,
                        MesmoVoo = i.VooId == i.VooId2,
                        VooNumero = i.VooNumero,
                        DataHoraChegadaEstimada = i.DataHoraChegadaEstimada,
                        DataHoraChegadaReal = i.DataHoraChegadaReal
                    }).ToList()
            }).ToList();

        return result1;
    }
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}
