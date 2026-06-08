using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao.Request;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.ReceitaFederal.Associacao;

public class RemoveAssociationService : IRemoveAssociationService
{
    private readonly ICertitificadoDigitalSupport _certificadoDigitalSupport;
    private readonly IAssociacaoRepository _masterHouseAssociacaoRepository;
    private readonly IAutenticaReceitaFederal _autenticaReceitaFederal;
    private readonly IUploadReceitaFederal _uploadReceitaFederal;
    private readonly IMotorIataHouse _motorIataHouse;

    public RemoveAssociationService(
        ICertitificadoDigitalSupport certificadoDigitalSupport,
        IAssociacaoRepository masterHouseAssociacaoRepository,
        IAutenticaReceitaFederal autenticaReceitaFederal,
        IUploadReceitaFederal uploadReceitaFederal,
        IMotorIataHouse motorIataHouse)
    {
        _certificadoDigitalSupport = certificadoDigitalSupport;
        _masterHouseAssociacaoRepository = masterHouseAssociacaoRepository;
        _autenticaReceitaFederal = autenticaReceitaFederal;
        _uploadReceitaFederal = uploadReceitaFederal;
        _motorIataHouse = motorIataHouse;
    }

    #region Public Methods
    public async Task<List<MasterHouseAssociationUploadResponse>> RemoveAssociationAsync(UserSession userSession, SubmitAssociatonRequest request)
    {
        int[] associationIds = new int[1];
        associationIds[0] = request.associationId;

        var masterList =
            _masterHouseAssociacaoRepository.GetMasterNumbersByAssociationIds(userSession.CompanyId, request.freightFowarderId, associationIds);

        if(masterList.Count() == 0)
            throw new BusinessException("Não é possivel cancelar associação, associação não encontrada!");

        var associationList =
            _masterHouseAssociacaoRepository.GetMasterHouseAssociationByMasterList(userSession.CompanyId, masterList.ToArray());

        if (associationList.Any(x => x.SituacaoAssociacaoRFBId == 1))
            throw new BusinessException("Não é possivel cancelar associação com document pendente de status");

        var freightFowarderId = associationList.SelectMany(x => x.MasterHouseAssociationChildren).FirstOrDefault()?.House.AgenteDeCargaId;

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, freightFowarderId.Value);

        if (certificate.HasError)
            throw new BusinessException(certificate.Error);

        var token = await
            _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        var updateAssociationList = associationList.ToList();

        await CancelarHouseMasterAssociacao(updateAssociationList, token, certificate.Certificate);

        var responseList = new List<MasterHouseAssociationUploadResponse>();

        foreach (var item in updateAssociationList)
        {
            responseList.Add(MasterHouseAssociationUploadResponse.GetResonse(item));
        }

        return responseList;
    }

    public async Task<List<MasterHouseAssociationUploadResponse>> CheckRemoveAssociationAsync(UserSession userSession, SubmitAssociatonRequest request)
    {
        int[] associationIds = new int[1];
        associationIds[0] = request.associationId;

        var masterList =
            _masterHouseAssociacaoRepository.GetMasterNumbersByAssociationIds(userSession.CompanyId, request.freightFowarderId, associationIds);

        var associationList =
            _masterHouseAssociacaoRepository.GetMasterHouseAssociationByMasterList(userSession.CompanyId, masterList.ToArray());

        if (!CheckRemoveAssociationAvailable(associationList))
            throw new BusinessException("Não existe Exclusão pendente para ser processado!");

        var protocolExclusion = associationList.FirstOrDefault(x => x.Id == request.associationId).ProtocoloDeletionAssociacaoRFB;

        var certificate = await
            _certificadoDigitalSupport.GetCertificateForFreightFowarder(userSession, request.freightFowarderId);

        var token = await
            _autenticaReceitaFederal.GetTokenAuthetication(certificate.Certificate, "AGECARGA");

        var res = await _uploadReceitaFederal.CheckFileProtocol(protocolExclusion, token);

        var associationListUpdate = associationList.ToList();

        await ProcessCheckRemoveAssociationAsync(res, associationListUpdate);

        var responseList = new List<MasterHouseAssociationUploadResponse>();

        foreach (var item in associationListUpdate)
        {
            responseList.Add(MasterHouseAssociationUploadResponse.GetResonse(item));
        }

        return responseList;
    }

    #endregion

    #region Cancelation Submit Private
    private async Task CancelarHouseMasterAssociacao(
        List<MasterHouseAssociacao> associationList,
        TokenResponse token,
        X509Certificate2 certificado)
    {
        var associationBase = associationList.SelectMany(x => x.MasterHouseAssociationChildren);

        string freightFowarderCnpj = associationBase.FirstOrDefault().House.AgenteDeCargaInfo.CNPJ;

        var operation = IataXmlPurposeCode.Deletion;

        string xmlAssociacao = _motorIataHouse
            .GenMasterHouseManifest(associationList, operation);

        var responseAssociacao = await _uploadReceitaFederal
            .SubmitHouseMaster(freightFowarderCnpj, xmlAssociacao, token, certificado);

        await ProcessarRetornoExclusaoAssociacao(responseAssociacao, associationList);

        return;
    }

    private async Task ProcessarRetornoExclusaoAssociacao(
        ReceitaRetornoProtocol response,
        List<MasterHouseAssociacao> association)
    {
        switch (response.StatusCode)
        {
            case "Received":
                await ReceivedMasterHouseAssociationExclusionUpload(association, response);
                break;
            case "Rejected":
                await RejectMasterHouseAssociationExclusionUpload(association, response);
                break;
            case "Processed":
                await ProcessMasterHouseAssociationExclusionUpload(association, response);
                break;
        }
    }

    private async Task ReceivedMasterHouseAssociationExclusionUpload(
        IEnumerable<MasterHouseAssociacao> associationList,
        ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoDeletionAssociacaoRFBId = 1;
            association.CodigoErroDeletionAssociacaoRFB = null;
            association.DescricaoErroDeletionAssociacaoRFB = null;
            association.ProtocoloDeletionAssociacaoRFB = response.Reason;
            association.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task RejectMasterHouseAssociationExclusionUpload(
        IEnumerable<MasterHouseAssociacao> associationList,
        ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoDeletionAssociacaoRFBId = 3;
            association.CodigoErroDeletionAssociacaoRFB = response.StatusCode;
            association.DescricaoErroDeletionAssociacaoRFB = response.Reason;
            association.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task ProcessMasterHouseAssociationExclusionUpload(
        IEnumerable<MasterHouseAssociacao> associationList,
        ReceitaRetornoProtocol response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoDeletionAssociacaoRFBId = 2;
            association.CodigoErroDeletionAssociacaoRFB = null;
            association.DescricaoErroDeletionAssociacaoRFB = null;
            association.DataProtocoloDeletionAssociacaoRFB = response.IssueDateTime;
            association.DataChecagemDeletionAssociacaoRFB = null;
            association.SituacaoAssociacaoRFBId = 0;
            association.ProtocoloAssociacaoRFB = null;
            association.CodigoErroAssociacaoRFB = null;
            association.DescricaoErroAssociacaoRFB = null;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }
    #endregion

    #region Check Cancelattion Association
    private bool CheckRemoveAssociationAvailable(IEnumerable<MasterHouseAssociacao> associationList)
    {
        return associationList.Any(x => x.SituacaoDeletionAssociacaoRFBId == 1);
    }

    private async Task ProcessCheckRemoveAssociationAsync(
        ProtocoloReceitaCheckFile response,
        List<MasterHouseAssociacao> associationList)
    {

        switch (response.status)
        {
            case "Rejected":
                await RejectCheckRemoveAssociationRejectAsync(associationList, response);
                break;
            case "Processed":
                await ProcessCheckRemoveAssociationRejectAsync(associationList, response);
                break;
        }
    }

    private async Task RejectCheckRemoveAssociationRejectAsync(
        IEnumerable<MasterHouseAssociacao> associationList,
        ProtocoloReceitaCheckFile response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoDeletionAssociacaoRFBId = 3;
            association.CodigoErroDeletionAssociacaoRFB = response.errorList[0].code;
            association.DescricaoErroDeletionAssociacaoRFB = string.Join("\n", response.errorList.Select(x => x.description));
            association.DataProtocoloDeletionAssociacaoRFB = response.dateTime;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }

    private async Task ProcessCheckRemoveAssociationRejectAsync(
        IEnumerable<MasterHouseAssociacao> associationList,
        ProtocoloReceitaCheckFile response)
    {
        var associations = associationList.ToList();

        foreach (var association in associations)
        {
            association.SituacaoDeletionAssociacaoRFBId = 2;
            association.CodigoErroDeletionAssociacaoRFB = null;
            association.DescricaoErroDeletionAssociacaoRFB = null;
            association.DataProtocoloDeletionAssociacaoRFB = response.dateTime;
            association.SituacaoAssociacaoRFBId = 0;
            _masterHouseAssociacaoRepository.UpdateMasterHouseAssociacao(association);
        }

        await _masterHouseAssociacaoRepository.SaveChangesAsync();
    }
    #endregion
}