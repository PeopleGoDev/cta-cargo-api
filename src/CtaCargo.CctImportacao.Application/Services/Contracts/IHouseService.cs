using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IHouseService
    {
        Task<ApiResponse<HouseResponseDto>> AtualizarHouse(UserSession userSession, HouseUpdateRequestDto houseRequest);
        Task<ApiResponse<HouseResponseDto>> AtualizarReenviarHouse(UserSession userSession, int houseId);
        Task<ApiResponse<HouseResponseDto>> AtualizarReenviarAssociacaoHouse(UserSession userSession, int houseId);
        Task<ApiResponse<HouseResponseDto>> ExcluirHouse(UserSession userSession, int houseId);
        Task<ApiResponse<HouseResponseDto>> HousePorId(UserSession userSession, int houseId);
        Task<ApiResponse<HouseResponseDto>> InserirHouse(UserSession userSession, HouseInsertRequestDto houseRequest);
        Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(UserSession userSession, HouseListarRequest input);
        Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHousesPorDataCriacao(UserSession userSession, MasterHousePorDataCriacaoRequest input);
        Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUpload(UserSession userSession, HouseListarRequest input);
    }
}