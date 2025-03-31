using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts;

public interface IHouseService
{
    Task<ApiResponse<HouseResponseDto>> AtualizarHouse(UserSession userSession, HouseUpdateRequestDto houseRequest);
    Task<ApiResponse<HouseResponseDto>> AtualizarReenviarHouse(UserSession userSession, int houseId);
    Task<ApiResponse<HouseResponseDto>> AtualizarReenviarAssociacaoHouse(UserSession userSession, int houseId);
    Task<ApiResponse<HouseResponseDto>> ExcluirHouse(UserSession userSession, int houseId);
    Task<ApiResponse<HouseResponseDto>> HousePorId(UserSession userSession, int houseId);
    Task<ApiResponse<HouseResponseDto>> InserirHouse(UserSession userSession, HouseInsertRequestDto houseRequest, string inputMode = "Manual");
    Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHouses(UserSession userSession, HouseListarRequest input);
    Task<ApiResponse<IEnumerable<HouseResponseDto>>> ListarHousesPorDataCriacao(UserSession userSession, MasterHousePorDataCriacaoRequest input);
    Task<List<MasterHouseAssociationUploadResponse>> SelectHouseAssociationForUploadAsync(UserSession userSession, HouseListarRequest input);
    Task<MasterHouseAssoationForUploadResponse> SelectHouseAssociationAsync(UserSession userSession, HouseListarRequest input);
    Task<List<MasterHouseAssociationResponse>> IncluirAssociacaoMasterHouse(UserSession userSession, AddMasterHouseAssociationRequest request);
    Task<List<MasterHouseAssociationResponse>> AtualizarAssociacaoMasterHouse(UserSession userSession, UpdateMasterHouseAssociationRequest request);
    Task<List<MasterHouseAssociationResponse>> DesfazerAssociacaoMasterHouse(UserSession userSession, RemoveMasterHouseAssociationRequest request);
}