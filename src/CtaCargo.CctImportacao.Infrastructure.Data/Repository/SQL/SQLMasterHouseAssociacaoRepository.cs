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
    public class SQLMasterHouseAssociacaoRepository : IMasterHouseAssociacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLMasterHouseAssociacaoRepository(ApplicationDbContext context)
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

        public async Task<List<MasterHouseAssociacao>> SelectMasterHouseAssociacaoParam(Expression<Func<MasterHouseAssociacao, bool>> predicate)
        {
            return await _context.MasterHouseAssociacoes.Where(predicate).ToListAsync();
        }

        public async Task<MasterHouseAssociacao> SelectMasterHouseAssociacaoById(int ciaId, int id)
        {
            return await _context.MasterHouseAssociacoes
                .FirstOrDefaultAsync(x => x.EmpresaId == ciaId && x.Id == id && x.DataExclusao == null);
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

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
