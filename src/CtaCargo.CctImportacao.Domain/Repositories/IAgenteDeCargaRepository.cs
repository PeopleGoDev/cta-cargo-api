using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface IAgenteDeCargaRepository
    {
        void CreateAgenteDeCarga(AgenteDeCarga agenteDeCarga);
        void DeleteAgenteDeCarga(AgenteDeCarga agenteDeCarga);
        Task<AgenteDeCarga> GetAgenteDeCargaByIataCode(int empresaId, string iataCode);
        Task<AgenteDeCarga> GetAgenteDeCargaById(int ciaId, int id);
        Task<IEnumerable<AgenteDeCarga>> GetAllAgenteDeCarga(int empresaId);
        Task<bool> SaveChanges();
        void UpdateAgenteDeCarga(AgenteDeCarga agenteDeCarga);
    }
}