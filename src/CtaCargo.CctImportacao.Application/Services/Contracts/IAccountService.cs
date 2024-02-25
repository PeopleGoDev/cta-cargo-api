using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.IO;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IAccountService
{
    Task<UsuarioLoginResponse> AutenticarUsuario(UsuarioLoginRequest usuarioLogin);
}