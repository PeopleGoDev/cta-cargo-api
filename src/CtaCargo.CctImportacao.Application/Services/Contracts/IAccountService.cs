using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IAccountService
    {
        Task<ApiResponse<UsuarioLoginResponse>> AutenticarUsuario(UsuarioLoginRequest usuarioLogin);
    }
}