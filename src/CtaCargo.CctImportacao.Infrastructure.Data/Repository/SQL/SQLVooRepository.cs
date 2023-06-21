using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
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
                .FirstOrDefaultAsync(x => x.Id == vooId && x.DataExclusao == null);
        }

        public async Task<Voo> GetVooIdByDataVooNumero(DateTime dataVoo, string numeroVoo)
        {
            return await _context.Voos
                .Where(x => x.DataVoo == dataVoo && x.Numero == numeroVoo && x.DataExclusao == null)
                .FirstOrDefaultAsync();
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
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Voo> GetVooWithULDById(int companyId, int vooId)
        {
            return await _context.Voos
                .Include("ULDs")
                .Include("ULDs.MasterInfo")
                .Include("ULDs.MasterInfo.AeroportoOrigemInfo")
                .Include("ULDs.MasterInfo.AeroportoDestinoInfo")
                .Include("PortoIataOrigemInfo")
                .Include("PortoIataDestinoInfo")
                .Include("CompanhiaAereaInfo")
                .FirstOrDefaultAsync(x => x.EmpresaId == companyId && x.Id == vooId && x.DataExclusao == null);
        }

        public async Task<IEnumerable<VooListaQuery>> GetVoosByDate(QueryJunction<Voo> param)
        {
            return await _context.Voos
                .Where(param.ToPredicate())
                .Select(x => new VooListaQuery
                {
                    VooId = x.Id,
                    Numero = x.Numero,
                    SituacaoVoo = x.StatusId
                }).ToListAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateVoo(Voo voo)
        {
            _context.Update(voo);
        }
    }
}
