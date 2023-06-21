using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
    public class SQLClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void DeleteCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            cliente.DataExclusao = DateTime.UtcNow;
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Cliente>> GetAllClientes(int empresaId)
        {
            return await _context.Clientes.Where(
                x => x.EmpresaId == empresaId &&
                x.DataExclusao == null).OrderByDescending(x => x.Nome).ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int clienteId)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(x => x.Id == clienteId && x.DataExclusao == null);
        }

        public async Task<string> GetCnpjClienteByKey1(string consignatarioNome,
            string consignatarioEndereco,
            string consignatarioCidade,
            string consignatarioPaisCodigo,
            string consignatarioPostal,
            string consignatarioSubDivisao)
        {
            return await _context.Clientes
                .Include("CnpjClientes")
                .Where(x => x.Nome == consignatarioNome &&
                x.Endereco == consignatarioEndereco &&
                x.Cidade == consignatarioCidade &&
                x.PaisCodigo == consignatarioPaisCodigo &&
                x.Postal == consignatarioPostal &&
                x.Subdivisao == consignatarioSubDivisao &&
                x.DataExclusao == null).Select(a => a.CnpjClienteInfo.Cnpj).FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByCnpj(string cnpj)
        {
            return await _context.Clientes
                .Include("CnpjClientes")
                .Where(x => x.CnpjClienteInfo.Cnpj == cnpj &&
                x.DataExclusao == null).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateCliente(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }
    }
}
