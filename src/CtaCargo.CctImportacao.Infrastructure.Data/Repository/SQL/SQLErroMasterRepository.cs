using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLErroMasterRepository : IErroMasterRepository
{
    private readonly ApplicationDbContext _context;

    public SQLErroMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void UpdateErroMaster(ErroMaster erroMaster)
    {
        _context.ErrosMaster.Update(erroMaster);
    }

    public void DeleteErroMaster(List<ErroMaster> erroLista)
    {
        if (erroLista.Count > 0)
        {
            erroLista.ForEach(item =>
            {
                item.DataExclusao = DateTime.UtcNow;
                _context.ErrosMaster.Update(item);
            });
        }
    }
}
