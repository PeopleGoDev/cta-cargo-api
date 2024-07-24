using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface IClienteRepository
    {
        void CreateCliente(Cliente cliente);
        void DeleteCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> GetAllClientes(int empresaId);
        Task<Cliente> GetClienteById(int clienteId);
        Task<string> GetCnpjClienteByKey1(string consignatarioNome, string consignatarioEndereco, string consignatarioCidade, string consignatarioPaisCodigo, string consignatarioPostal, string consignatarioSubDivisao);
        Task<bool> SaveChanges();
        void UpdateCliente(Cliente cliente);
    }
}