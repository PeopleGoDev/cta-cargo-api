using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SqlMasterRepository : IMasterRepository
{
    private readonly ApplicationDbContext _context;

    public SqlMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateMaster(int companyId, Master master)
    {
        if (master == null)
        {
            throw new ArgumentNullException(nameof(master));
        }

        _context.Masters.Add(master);
    }

    public void DeleteMaster(int companyId, Master master)
    {
        if (master == null)
        {
            throw new ArgumentNullException(nameof(master));
        }
        master.DataExclusao = DateTime.UtcNow;
        _context.Masters.Update(master);
    }

    public async Task<IEnumerable<Master>> GetAllMasters(Expression<Func<Master, bool>> predicate)
    {
        return await _context.Masters
            .Include("ULDs")
            .Include(e => e.ErrosMaster.Where(e => e.DataExclusao == null))
            .Include("UsuarioCriacaoInfo")
            .Include("VooInfo")
            .Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<Master>> GetAllMastersByDataCriacao(int companyId, DateTime dataEmissao)
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
        return await _context.Masters
            .Include("ULDs")
            .Include("ErrosMaster")
            .Include("UsuarioCriacaoInfo")
            .Include("VooInfo")
            .Where( x => 
        x.EmpresaId == companyId &&
        x.DataExclusao == null &&
        x.CreatedDateTimeUtc >= dataInicial &&
        x.CreatedDateTimeUtc <= dataFinal).ToListAsync();
    }
    
    public async Task<Master> GetMasterById(int companyId, int masterId)
    {
        return await _context.Masters
            .Include("ULDs")
            .Include("ErrosMaster")
            .Include("UsuarioCriacaoInfo")
            .Include("VooInfo")
            .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == masterId && x.DataExclusao == null);
    }
    public async Task<IEnumerable<Master>> GetMasterByIds(int companyId, int[] masterIds)
    {
        return await _context.Masters
            .Include("ULDs")
            .Include("ErrosMaster")
            .Include("UsuarioCriacaoInfo")
            .Include("VooInfo")
            .Where(x => x.EmpresaId == companyId && masterIds.Contains(x.Id) && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<List<Master>> GetMastersForUploadById(int companyId, int[] masterArrayId)
    {
        return await _context.Masters
            .Include("CiaAereaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .Include("ErrosMaster")
            .Include("ULDs")
            .Include("VooInfo")
            .Include("VooInfo.CompanhiaAereaInfo")
            .Include("VooInfo.PortoIataOrigemInfo")
            .Include("VooInfo.PortoIataDestinoInfo")
            .Where(x => x.EmpresaId == companyId && masterArrayId.Contains(x.Id) && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<Master> GetMasterForUploadById(int companyId, int masterId)
    {
        return await _context.Masters
            .Include("CiaAereaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .Include(x => x.ErrosMaster.Where(y => y.DataExclusao == null))
            .Include("ULDs")
            .Include("VooInfo")
            .Include("VooInfo.CompanhiaAereaInfo")
            .Include("VooInfo.PortoIataOrigemInfo")
            .Include("VooInfo.PortoIataDestinoInfo")
            .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == masterId && x.DataExclusao == null);
    }

    public async Task<List<Master>> GetMastersForUploadByVooId(int companyId, int vooId)
    {
        return await _context.Masters
            .Include("CiaAereaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .Include("ErrosMaster")
            .Include("VooInfo")
            .Include("VooInfo.CompanhiaAereaInfo")
            .Include("VooInfo.PortoIataOrigemInfo")
            .Include("VooInfo.PortoIataDestinoInfo")
            .Include("VooInfo.Trechos")
            .Include("VooInfo.Trechos.PortoIataDestinoInfo")
            .Where(x => x.EmpresaId == companyId && x.VooId == vooId && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<List<Master>> GetMastersForUploadSelected(int companyId, int[] masterIdList)
    {
        return await _context.Masters
            .Include("CiaAereaInfo")
            .Include("AeroportoOrigemInfo")
            .Include("AeroportoDestinoInfo")
            .Include("VooInfo")
            .Include("VooInfo.CompanhiaAereaInfo")
            .Include("VooInfo.PortoIataOrigemInfo")
            .Include("VooInfo.PortoIataDestinoInfo")
            .Include("VooInfo.Trechos")
            .Include("VooInfo.Trechos.PortoIataDestinoInfo")
            .Where(x => x.EmpresaId == companyId && masterIdList.Contains(x.Id) && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<MasterListaQuery>> GetMastersListaByVooId(int companyId, int vooId)
    {
        return await (from m in _context.Masters
                      where m.EmpresaId == companyId && m.VooId == vooId &&
                      m.DataExclusao == null
                      select new MasterListaQuery()
                      {
                          MasterId = m.Id,
                          Numero = m.Numero
                      }).ToListAsync();
    }

    public async Task<IEnumerable<MasterVooQuery>> GetMastersVoo(int companyId, int vooId)
    {
        return await (from m in _context.Masters
                    where m.EmpresaId == companyId && m.VooId == vooId && m.DataExclusao == null
                    select new MasterVooQuery()
                    {
                        Numero = m.Numero,
                        Descricao = m.DescricaoMercadoria,
                        Peso = m.PesoTotalBruto,
                        PesoUnidade = m.PesoTotalBrutoUN,
                        TotalPecas = m.TotalPecas,
                        PortoOrigemId = m.AeroportoOrigemId,
                        PortoDestinoId = m.AeroportoDestinoId
                    }).ToListAsync();
    }

    public async Task<Master> GetMasterIdByNumber(int companyId, int? vooId, string masterNumber)
    {
        var result = await _context.Masters
            .Where(x => x.EmpresaId == companyId && x.VooId == vooId && x.Numero == masterNumber && x.DataExclusao == null)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<Master> GetMasterByNumber(int companyId, string masterNumber)
    {
        var oneYearDate = DateTime.UtcNow.AddYears(-1);

        var result = await _context.Masters
            .Where(x => x.EmpresaId == companyId && x.DataExclusao == null
                    && x.Numero == masterNumber && x.CreatedDateTimeUtc > oneYearDate)
            .Include(x => x.ErrosMaster)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();
        return result;
    }

    public async Task<SituacaoRFBQuery> GetMasterRFBStatus(int masterId)
    {
        return await _context.Masters
            .Where(x => x.Id == masterId && x.DataExclusao == null)
            .Select(x => new SituacaoRFBQuery
            {
                Id = x.Id,
                SituacaoRFB = x.SituacaoRFBId,
                ProtocoloRFB = x.ProtocoloRFB
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int?> GetMasterIdByNumberValidate(int ciaId, string numero, DateTime dataLimite)
    {
        var result = await _context.Masters
            .Where(x => x.CiaAereaId == ciaId && x.Numero == numero && x.CreatedDateTimeUtc >= dataLimite && x.DataExclusao == null)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return result > 0 ? result : null;
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void ClearChangeTracker()
    {
        _context.ChangeTracker.Clear();
    }

    public void UpdateMaster(Master master)
    {
        _context.Update(master);
    }

    public FileImport GetMasterFileImportById(int ciaId, int fileId)
    {
        return _context.FileImport
            .Include(x => x.Details.Where(x => x.DataExclusao == null))
            .FirstOrDefault(x => x.EmpresaId == ciaId && x.Destination == "MASTER" && x.Id == fileId && x.DataExclusao == null);
    }

    public List<FileImport> GetMasterFileImportList(int ciaId)
    {
        return _context.FileImport
            .Include(x => x.Details.Where(x => x.DataExclusao == null))
            .Where(x => x.EmpresaId == ciaId && x.Destination == "MASTER" && x.DataExclusao == null)
            .ToList();
    }
}
