using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface ISubmeterReceitaService
{
    Task<ApiResponse<string>> SubmeterVoo(UserSession userSession, FlightUploadRequest input);
    Task<ApiResponse<string>> SubmitFlightSchedule(UserSession userSession, FlightUploadRequest input);
    Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterVooMaster(UserSession userSession, FlightUploadRequest input);
    Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterMasterSelecionado(UserSession userSession, FlightUploadRequest input);
    Task<ApiResponse<IEnumerable<FileUploadResponse>>> SubmeterMasterAcao(UserSession userSession, MasterUploadInput input);
    Task<ApiResponse<string>> VerificarProtocoloVoo(UserSession userSession, FlightUploadRequest input);
    Task<MasterResponseDto> SubmeterMasterExclusion(UserSession userSession, MasterExclusaoRFBInput input);
}