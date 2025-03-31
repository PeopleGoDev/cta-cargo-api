using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao;
public interface IRemoveAssociationService
{
    Task<List<MasterHouseAssociationUploadResponse>> CheckRemoveAssociationAsync(UserSession userSession, SubmitAssociatonRequest request);
    Task<List<MasterHouseAssociationUploadResponse>> RemoveAssociationAsync(UserSession userSession, SubmitAssociatonRequest request);
}