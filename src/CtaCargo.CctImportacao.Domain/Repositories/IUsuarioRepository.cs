﻿using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<bool> SaveChanges();
        Task<IEnumerable<Usuario>> GetAllUsuarios(int empresaId, bool usuarioSistema = false);
        Task<Usuario> GetUsuarioById(int usuarioId);
        Task<Usuario> GetUserCertificateById(int userId);
        Task<Usuario> GetUsuarioByAuthentication(string email, string pwd);
        void CreateUsuario(Usuario usr);
        void UpdateUsuario(Usuario usr);
        void DeleteUsuario(Usuario usr);
    }
}
