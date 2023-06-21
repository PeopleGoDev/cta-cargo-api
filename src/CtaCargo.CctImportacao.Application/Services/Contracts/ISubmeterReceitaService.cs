using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface ISubmeterReceitaService
    {
        Task<ApiResponse<string>> SubmeterVoo(UserSession userSession, VooUploadInput input);
        Task<ApiResponse<string>> SubmeterVooMaster(UserSession userSession, VooUploadInput input);
        Task<ApiResponse<string>> SubmeterMasterAcao(UserSession userSession, MasterUploadInput input);
        Task<ApiResponse<string>> VerificarProtocoloVoo(VooUploadInput input);
        Task<ApiResponse<string>> SubmeterMasterExclusion(UserSession userSession, MasterExclusaoRFBInput input);
        Task<ApiResponse<string>> SubmeterHousesAgentesDeCarga(SubmeterRFBHouseRequest input);
        Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(UserSession userSession, SubmeterRFBMasterHouseRequest input);
    }
}