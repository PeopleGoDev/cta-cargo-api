using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Model.Iata.FlightManifest;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLVooRepository : IVooRepository
{
    private readonly ApplicationDbContext _context;

    public SQLVooRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateVoo(Voo voo)
    {
        if (voo == null)
        {
            throw new ArgumentNullException(nameof(voo));
        }

        _context.Voos.Add(voo);
    }

    public void DeleteVoo(Voo voo)
    {
        if (voo == null)
        {
            throw new ArgumentNullException(nameof(voo));
        }
        voo.DataExclusao = DateTime.UtcNow;
        _context.Voos.Update(voo);
    }

    public async Task<IEnumerable<Voo>> GetAllVoos(QueryJunction<Voo> param)
    {
        return await _context.Voos
            .Include("UsuarioCriacaoInfo")
            .Include(x => x.Trechos.Where(t => t.DataExclusao == null))
            .Include("Trechos.PortoIataDestinoInfo")
            .Include(x => x.ParentFlightInfo)
            .Where(param.ToPredicate())
            .OrderByDescending( x => x.DataVoo)
            .ToListAsync();
    }

    public async Task<Voo> GetVooById(int vooId)
    {
        return await _context.Voos
            .Include("UsuarioCriacaoInfo")
            .Include("PortoIataOrigemInfo")
            .Include("PortoIataDestinoInfo")
            .Include("CompanhiaAereaInfo")
            .Include(x => x.Trechos.Where(t => t.DataExclusao == null))
            .Include("Trechos.ULDs")
            .Include("Trechos.ULDs.UsuarioCriacaoInfo")
            .FirstOrDefaultAsync(x => x.Id == vooId && x.DataExclusao == null);
    }

    public async Task<Voo> GetVooForExclusionById(int ciaId, int vooId)
    {
        return await _context.Voos
            .Include(x => x.Masters.Where(y => y.DataExclusao == null))
            .FirstOrDefaultAsync(x => x.EmpresaId == ciaId &&  x.Id == vooId && x.DataExclusao == null);
    }

    public async Task<Voo> GetVooByIdSimple(int companyId, int vooId)
    {
        return await _context.Voos
            .Include("PortoIataOrigemInfo")
            .Include("CompanhiaAereaInfo")
            .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == vooId && x.DataExclusao == null);
    }

    public IEnumerable<VooTrecho> GetTrechoByVooId(int vooId)
    {
        return _context.VooTrechos
            .Where(x => x.VooId == vooId && x.DataExclusao == null);
    }

    public Voo GetVooIdByDataVooNumero(int companyId, DateTime dataVoo, string numeroVoo)
    {
        return _context.Voos
            .Include("UsuarioCriacaoInfo")
            .Include("PortoIataOrigemInfo")
            .Include("PortoIataDestinoInfo")
            .Include("CompanhiaAereaInfo")
            .Include(x => x.Trechos.Where(t => t.DataExclusao == null))
            .Include("Trechos.ULDs")
            .Where(x => x.EmpresaId == companyId && 
                        x.DataVoo == dataVoo && 
                        x.Numero == numeroVoo && 
                        x.DataExclusao == null)
            .FirstOrDefault();
    }

    public async Task<SituacaoRFBQuery> GetVooRFBStatus(int vooId)
    {
        return await _context.Voos
            .Where(x => x.Id == vooId && x.DataExclusao == null)
            .Select( x => new SituacaoRFBQuery
            {
                Id = x.Id,
                SituacaoRFB = x.SituacaoRFBId,
                ProtocoloRFB = x.ProtocoloRFB,
                Reenviar = x.Reenviar,
                ScheduleProtocolRFB = x.ProtocoloScheduleRFB,
                ScheduleSituationRFB = x.ScheduleSituationRFB,
                GhostFlight = x.GhostFlight
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Voo> GetVooWithULDById(int companyId, int vooId)
    {
        return await _context.Voos
            .Include(x => x.Trechos.Where(y => y.DataExclusao == null))
            .Include("Trechos.ULDs")
            .Include("Trechos.ULDs.MasterInfo")
            .Include("Trechos.ULDs.MasterInfo.AeroportoOrigemInfo")
            .Include("Trechos.ULDs.MasterInfo.AeroportoDestinoInfo")
            .Include("PortoIataOrigemInfo")
            .Include("Trechos.PortoIataDestinoInfo")
            .Include("CompanhiaAereaInfo")
            .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == vooId && x.DataExclusao == null);
    }

    public async Task<IEnumerable<VooListaQuery>> GetVoosByDate(QueryJunction<Voo> param)
    {
        var dados = await _context.Voos
            .Include(x => x.CompanhiaAereaInfo)
            .Include(x => x.CompanhiaAereaInfo.CertificadoDigital)
            .Include(x => x.Trechos.Where(t => t.DataExclusao == null))
            .Where(param.ToPredicate())
            .ToListAsync();

        return dados
            .Select(x => new VooListaQuery
            {
                VooId = x.Id,
                Numero = x.Numero,
                FlightType = x.FlightType,
                SituacaoVoo = x.StatusId,
                CiaAereaNome = x.CompanhiaAereaInfo.Nome,
                CertificadoValidade = x.CompanhiaAereaInfo.CertificadoDigital?.DataVencimento,
                GhostFlight = x.GhostFlight,
                Trechos = (from t in x.Trechos select new VooTrechoQuery(t.Id, t.AeroportoDestinoCodigo))
            });
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public VooTrecho SelectTrecho(int id)
    {
        return _context.VooTrechos
            .Include(x => x.VooInfo)
            .Include(x => x.ULDs)
            .FirstOrDefault(x => x.DataExclusao == null && x.Id == id);
    }

    public VooTrecho? GetVooBySegment(int ciaId, string airportOfDestiny, DateTime estimateArrival)
    {
        return _context.VooTrechos
            .Include(x => x.VooInfo)
            .Where(x => x.EmpresaId == ciaId && x.AeroportoDestinoCodigo == airportOfDestiny &&
                          x.DataHoraChegadaEstimada.Value == estimateArrival && x.DataExclusao == null)
            .FirstOrDefault();
    }

    public void AddTrecho(VooTrecho trecho)
    {
        _context.VooTrechos.Add(trecho);
    }

    public void UpdateTrecho(VooTrecho trecho)
    {
        _context.VooTrechos.Update(trecho);
    }

    public void RemoveTrecho(VooTrecho trecho)
    {
        trecho.DataExclusao = DateTime.UtcNow;
    }

    public void UpdateVoo(Voo voo)
    {
        _context.Update(voo);
    }
}
