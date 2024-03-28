using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Context;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.SQL
{
    public class SQLUsuarioRepository: IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLUsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateUsuario(Usuario usr)
        {
            _context.Usuarios.Add(usr);
        }

        public void DeleteUsuario(Usuario usr)
        {
            _context.Usuarios.Remove(usr);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarios(int empresaId, bool usuarioSistema = false)
        {
            return await _context.Usuarios.Where(x => 
            x.EmpresaId == empresaId && 
            x.DataExclusao == null && 
            x.UsuarioSistema == usuarioSistema).ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByAuthentication(string email, string pwd)
        {
            var usuario = await _context.Usuarios
                .Include(x => x.Empresa)
                .Where(x => x.Account == email && x.DataExclusao == null)
                .FirstOrDefaultAsync();

            if (usuario == null) return null;
            if (string.CompareOrdinal(usuario.Senha, pwd) == 0) return usuario;
            return null;
        }

        public async Task<Usuario> GetUsuarioById(int usuarioId)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == usuarioId);
        }

        public async Task<Usuario> GetUserCertificateById(int userId)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == userId && x.DataExclusao == null && !x.Bloqueado);
        }

        public async Task<bool> SaveChanges() =>
            (await _context.SaveChangesAsync() >= 0);

        public void UpdateUsuario(Usuario usr)
        {
            _context.Update(usr);
        }
    }
}
