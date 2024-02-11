using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.IO;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IAccountService
{
    Task<ApiResponse<UsuarioLoginResponse>> AutenticarUsuario(UsuarioLoginRequest usuarioLogin);
    RegistryResponse GetRegistry(UserSession userSession);
    Task<RegistryInsertResponse> NewRegistryPost(string registrationToken, RegistryInsertRequest request);
    Task NewRegistryFilePost(string registrationToken, string fileType, Stream stream);
    Task DelRegistryFilePost(string registrationToken, string fileType);
    Task<CiaAereaResponseDto> NewRegistryAirCompanyPost(string registrationToken, RegistryAirCompanyInsertRequest request);
    Task<RegistryResponse> NewRegistryGet(string registrationToken);
    Task<CiaAereaResponseDto> NewRegistryAirCompanyGet(string registrationToken);
    RegistryUpdateResponse SetRegistry(UserSession userSession, RegistryUpdateRequest request);
}