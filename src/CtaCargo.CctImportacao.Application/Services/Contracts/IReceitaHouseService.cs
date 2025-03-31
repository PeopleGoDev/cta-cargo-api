using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IReceitaHouseService
{
    Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(UserSession userSession, SubmeterRFBMasterHouseRequest request);
    Task<ApiResponse<string>> SubmitHouseMasterAssociationAsync(UserSession userSession, SubmitRFBMasterHouseRequest request);
    Task<ApiResponse<HouseResponseDto>> SubmeterHouseExclusion(UserSession userSession, int houseId);
    Task<ApiResponse<string>> SubmeterHousesAgentesDeCarga(UserSession userSession, SubmeterRFBHouseRequest request);
    Task<ApiResponse<string>> SubmeterHousesAgentesDeCargaAndIds(UserSession userSession, SubmeterRFBHouseByIdsRequest request);
}