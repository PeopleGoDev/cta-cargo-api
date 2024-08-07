﻿using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IUsuarioService
{
    Task<ApiResponse<UsuarioResponseDto>> AtualizarUsuario(UsuarioUpdateRequest usuarioRequest);
    Task<ApiResponse<UsuarioResponseDto>> ExcluirUsuario(int usuarioId);
    Task<ApiResponse<UsuarioResponseDto>> InserirUsuario(UserSession userSession, UsuarioInsertRequest usuarioRequest);
    Task<ApiResponse<IEnumerable<UsuarioResponseDto>>> ListarUsuarios(int empresaId);
    Task<ApiResponse<string>> ResetarUsuario(UserResetRequest usuarioRequest);
    Task<ApiResponse<UsuarioResponseDto>> UsuarioPorId(int usuarioId);
}