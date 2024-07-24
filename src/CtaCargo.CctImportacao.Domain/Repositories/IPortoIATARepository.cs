using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface IPortoIATARepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<PortoIata>> GetAllPortosIATA(int empresaId);
        Task<PortoIata> GetPortoIATAById(int ciaId, int portoIATAId);
        Task<int?> GetPortoIATAIdByCodigo(string codigo);
        PortoIata GetPortoIATAByCode(int empresaId, string codigo);
        void CreatePortoIATA(PortoIata portoIATA);
        void UpdatePortoIATA(PortoIata portoIATA);
        void DeletePortoIATA(PortoIata portoIATA);
    }
}
