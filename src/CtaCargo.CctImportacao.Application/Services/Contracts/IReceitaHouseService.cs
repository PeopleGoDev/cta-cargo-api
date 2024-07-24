using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;
public interface IReceitaHouseService
{
    Task<ApiResponse<string>> SubmeterAssociacaoHousesMaster(UserSession userSession, SubmeterRFBMasterHouseRequest input);
    Task<ApiResponse<string>> SubmeterAssociation(UserSession userSession, int associationId);
    Task<ApiResponse<HouseResponseDto>> SubmeterHouseExclusion(UserSession userSession, int houseId);
    Task<ApiResponse<string>> SubmeterHousesAgentesDeCarga(UserSession userSession, SubmeterRFBHouseRequest input);
    Task<ApiResponse<string>> SubmeterHousesAgentesDeCargaAndIds(UserSession userSession, SubmeterRFBHouseByIdsRequest input);
}