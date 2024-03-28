using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL;

public class SQLAgenteDeCargaRepository : IAgenteDeCargaRepository
{
    private readonly ApplicationDbContext _context;

    public SQLAgenteDeCargaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CreateAgenteDeCarga(AgenteDeCarga agenteDeCarga)
    {
        if (agenteDeCarga == null)
        {
            throw new ArgumentNullException(nameof(agenteDeCarga));
        }

        _context.AgentesDeCarga.Add(agenteDeCarga);
    }

    public void DeleteAgenteDeCarga(AgenteDeCarga agenteDeCarga)
    {
        agenteDeCarga.DataExclusao = DateTime.UtcNow;
        SaveChanges();
    }

    public async Task<IEnumerable<AgenteDeCarga>> GetAllAgenteDeCarga(int empresaId)
    {
        return await _context.AgentesDeCarga
            .Include("CertificadoDigital")
            .Where(x => x.EmpresaId == empresaId && x.DataExclusao == null)
            .ToListAsync();
    }

    public async Task<AgenteDeCarga> GetAgenteDeCargaById(int ciaId, int id)
    {
        return await _context.AgentesDeCarga
            .Include("CertificadoDigital")
            .FirstOrDefaultAsync(x => x.EmpresaId == ciaId && x.Id == id);
    }

    public async Task<AgenteDeCarga> GetAgenteDeCargaByIataCode(int empresaId, string iataCode)
    {
        return await _context.AgentesDeCarga
            .FirstOrDefaultAsync(x => x.EmpresaId == empresaId && x.Numero == iataCode && x.DataExclusao == null);
    }

    public async Task<bool> SaveChanges()
    {
        return (await _context.SaveChangesAsync() >= 0);
    }

    public void UpdateAgenteDeCarga(AgenteDeCarga agenteDeCarga)
    {
        _context.Update(agenteDeCarga);
    }

}
