using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts
{
    public interface IPortoIATARepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<PortoIata>> GetAllPortosIATA(int empresaId);
        Task<PortoIata> GetPortoIATAById(int portoIATAId);
        Task<int?> GetPortoIATAIdByCodigo(string codigo);
        void CreatePortoIATA(PortoIata portoIATA);
        void UpdatePortoIATA(PortoIata portoIATA);
        void DeletePortoIATA(PortoIata portoIATA);
    }
}
