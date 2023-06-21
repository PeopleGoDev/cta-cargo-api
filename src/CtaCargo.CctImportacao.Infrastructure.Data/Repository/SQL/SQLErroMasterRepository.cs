using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
    public class SQLErroMasterRepository : IErroMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLErroMasterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteErroMaster(ICollection<ErroMaster> erroLista)
        {
            _context.ErrosMaster.RemoveRange(erroLista);
            _context.SaveChanges();
        }
    }
}
