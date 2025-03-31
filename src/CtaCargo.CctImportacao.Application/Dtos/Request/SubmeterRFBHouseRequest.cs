using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class SubmeterRFBHouseRequest
{
    public DateTime DataProcessamento { get; set; }
    public int AgenteDeCargaId { get; set; }
}

public class SubmeterRFBHouseByIdsRequest
{
    public DateTime DataProcessamento { get; set; }
    public int FreightFowarderId { get; set; }
    public List<int> HouseIds { get; set; }
}

public class AddMasterHouseAssociationRequest
{
    public int FreightFowarderId { get; set; }
    public List<AddRFBMasterHouseItemRequest> Masters { get; set; }

}

public class UpdateMasterHouseAssociationRequest
{
    public int FreightFowarderId { get; set; }

    public List<UpdateRFBMasterHouseItemRequest> Associations { get; set; }
}

public class RemoveMasterHouseAssociationRequest
{
    public int FreightFowarderId { get; set; }

    public List<RemoveRFBMasterHouseItemRequest> Associations { get; set; }
}

public class SubmeterRFBMasterHouseRequest
{
    public int FreightFowarderId { get; set; }
    public List<SubmeterRFBMasterHouseItemRequest> Masters { get; set; }

}

public class SubmeterRFBMasterHouseItemRequest
{
    public string MasterNumber { get; set; }
    public DateTime? CarrierDeclarationDate { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPiece { get; set; }
    public int[] HouseIds { get; set; }
}

public class AddRFBMasterHouseItemRequest
{
    public string MasterNumber { get; set; }
    public DateTime? CarrierDeclarationDate { get; set; }
    public int[] HouseIds { get; set; }
}

public class UpdateRFBMasterHouseItemRequest
{
    public string MessageHeaderDocumentoId { get; set; }
    public DateTime? CarrierDeclarationDate { get; set; }
    public int[] HouseIds { get; set; }
}

public class RemoveRFBMasterHouseItemRequest
{
    public string MessageHeaderDocumentoId { get; set; }
}

public class MasterHouseAssociationResponse : MasterHouseAssociationUploadResponse
{
    public static MasterHouseAssociationResponse FromMasterHouseAssociacao(MasterHouseAssociacao masterHouseAssociacao)
    {
        return new MasterHouseAssociationResponse
        {
            Number = masterHouseAssociacao.MasterNumber,
            DocumentId = masterHouseAssociacao.MessageHeaderDocumentId,
            Houses = masterHouseAssociacao.MasterHouseAssociationChildren.Select(x => new MasterHouseAssociationHouseItemResponse
            {
                DestinationLocation = x.House.AeroportoDestinoCodigo,
                AssociationDate = x.House.DataProtocoloAssociacaoRFB,
                AssociationCheckDate = x.House.DataChecagemAssociacaoRFB,
                AssociationErrorCode = x.House.CodigoErroAssociacaoRFB,
                AssociationErrorDescription = x.House.DescricaoErroAssociacaoRFB,
                AssociationProtocol = x.House.ProtocoloAssociacaoRFB,
                AssociationStatusId = x.House.SituacaoAssociacaoRFBId,
                Id = x.House.Id,
                Number = x.House.Numero,
                OriginLocation = x.House.AeroportoOrigemCodigo,
                PackageQuantity = x.House.TotalVolumes,
                ProcessDate = x.House.DataProcessamento,
                Resend = x.House.Reenviar,
                ResendAssociation = x.House.ReenviarAssociacao,
                RFBStatus = x.House.SituacaoRFBId,
                TotalPieceQuantity = x.House.TotalVolumes,
                TotalWeight = x.House.PesoTotalBruto,
                TotalWeightUnit = x.House.PesoTotalBrutoUN
            }).ToList(),
            Summary = new MasterHouseAssociationSummaryUploadResponse
            {
                Id = masterHouseAssociacao.Id,
                DestinationLocation = masterHouseAssociacao.FinalDestinationLocation,
                ConsignmentItemQuantity = masterHouseAssociacao.ConsigmentItemQuantity,
                IssueDate = masterHouseAssociacao.XmlIssueDate,
                OriginLocation = masterHouseAssociacao.OriginLocation,
                PackageQuantity = masterHouseAssociacao.PackageQuantity,
                TotalPieceQuantity = masterHouseAssociacao.TotalPieceQuantity,
                TotalWeight = masterHouseAssociacao.GrossWeight,
                TotalWeightUnit = masterHouseAssociacao.GrossWeightUnit,
                RFBCreationStatus = masterHouseAssociacao.SituacaoAssociacaoRFBId,
                RFBCreationProtocol = masterHouseAssociacao.ProtocoloAssociacaoRFB,
                RFBCancelationStatus = masterHouseAssociacao.SituacaoDeletionAssociacaoRFBId,
                RFBCancelationProtocol = masterHouseAssociacao.ProtocoloDeletionAssociacaoRFB

            }
        };
    }
}

public class MasterHouseAssoationForUploadResponse
{
    public List<MasterHouseAssociationUploadResponse> MasterAssociationItems { get; set; }
    public List<MasterHouseAssociationOpenMasterItem> OpenMasters { get; set; }

}

public class MasterHouseAssociationUploadResponse
{
    public string Number { get; set; }
    public string DocumentId { get; set; }
    public string CreateRFBProtocol { get; set; }
    public RFStatusEnvioType CreateRFPStatus { get; set; }
    public DateTime? CreateRFBSubmitDateTime { get; set; }
    public string CreateRFBErrorCode { get; set; }
    public string CreateRFBErrorDescription { get; set; }
    public string CancelationRFBProtocol { get; set; }
    public RFStatusEnvioType CancelationRFPStatus { get; set; }
    public DateTime? CancelationRFBSubmitDateTime { get; set; }
    public string CancelationRFBErrorCode { get; set; }
    public string CancelationRFBErrorDescription { get; set; }
    public DateTime? ProcessDate { get; set; }
    public MasterHouseAssociationSummaryUploadResponse? Summary { get; set; }
    public List<MasterHouseAssociationHouseItemResponse> Houses { get; set; }

    public static MasterHouseAssociationUploadResponse GetResonse(MasterHouseAssociacao masterHouseAssociacao)
    {
        return new MasterHouseAssociationUploadResponse
        {
            Number = masterHouseAssociacao.MasterNumber,
            DocumentId = masterHouseAssociacao.MessageHeaderDocumentId,
            ProcessDate = masterHouseAssociacao.ProcessDate,
            CreateRFPStatus = (RFStatusEnvioType)masterHouseAssociacao.SituacaoAssociacaoRFBId,
            CreateRFBProtocol = masterHouseAssociacao.ProtocoloAssociacaoRFB,
            CreateRFBErrorCode = masterHouseAssociacao.CodigoErroAssociacaoRFB,
            CreateRFBErrorDescription = masterHouseAssociacao.DescricaoErroAssociacaoRFB,
            CreateRFBSubmitDateTime = masterHouseAssociacao.DataProtocoloAssociacaoRFB,
            CancelationRFPStatus = (RFStatusEnvioType)masterHouseAssociacao.SituacaoDeletionAssociacaoRFBId,
            CancelationRFBProtocol = masterHouseAssociacao.ProtocoloDeletionAssociacaoRFB,
            CancelationRFBErrorCode = masterHouseAssociacao.CodigoErroDeletionAssociacaoRFB,
            CancelationRFBErrorDescription = masterHouseAssociacao.DescricaoErroDeletionAssociacaoRFB,
            CancelationRFBSubmitDateTime = masterHouseAssociacao.DataProtocoloDeletionAssociacaoRFB,
            Houses = masterHouseAssociacao.MasterHouseAssociationChildren.Select(x => new MasterHouseAssociationHouseItemResponse
            {
                AssociationCheckDate = x.House.DataChecagemAssociacaoRFB,
                AssociationDate = x.House.DataProtocoloAssociacaoRFB,
                AssociationErrorCode = x.House.CodigoErroAssociacaoRFB,
                AssociationErrorDescription = x.House.DescricaoErroAssociacaoRFB,
                AssociationProtocol = x.House.ProtocoloAssociacaoRFB,
                AssociationStatusId = x.House.SituacaoAssociacaoRFBId,
                ResendAssociation = x.House.ReenviarAssociacao,
                DestinationLocation = x.House.AeroportoDestinoCodigo,
                Id = x.House.Id,
                Number = x.House.Numero,
                OriginLocation = x.House.AeroportoOrigemCodigo,
                PackageQuantity = x.House.TotalVolumes,
                TotalPieceQuantity = x.House.TotalVolumes,
                ProcessDate = x.House.DataProcessamento,
                Resend = x.House.Reenviar,
                RFBStatus = x.House.SituacaoRFBId,
                TotalWeight = x.House.PesoTotalBruto,
                TotalWeightUnit = x.House.PesoTotalBrutoUN
            }).ToList(),
             Summary = new MasterHouseAssociationSummaryUploadResponse
             {
                 Id = masterHouseAssociacao.Id,
                 ConsignmentItemQuantity = masterHouseAssociacao.ConsigmentItemQuantity,
                 DestinationLocation = masterHouseAssociacao.FinalDestinationLocation,
                 OriginLocation = masterHouseAssociacao.OriginLocation,
                 PackageQuantity = masterHouseAssociacao.PackageQuantity,
                 IssueDate = masterHouseAssociacao.ProcessDate,
                 RFBCancelationProtocol = masterHouseAssociacao.ProtocoloDeletionAssociacaoRFB,
                 RFBCancelationStatus = masterHouseAssociacao.SituacaoDeletionAssociacaoRFBId,
                 RFBCreationProtocol = masterHouseAssociacao.ProtocoloAssociacaoRFB,
                 RFBCreationStatus = masterHouseAssociacao.SituacaoAssociacaoRFBId,
                 TotalPieceQuantity = masterHouseAssociacao.TotalPieceQuantity,
                 TotalWeight = masterHouseAssociacao.GrossWeight,
             }
        };
    }
}

public class MasterHouseAssociationSummaryUploadResponse
{
    public int Id { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int ConsignmentItemQuantity { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPieceQuantity { get; set; }
    public DateTime? IssueDate { get; set; }
    public int RFBCreationStatus { get; set; }
    public string RFBCreationProtocol { get; set; }
    public int RFBCancelationStatus { get; set; }
    public string RFBCancelationProtocol { get; set; }
}

public class MasterHouseAssociationHouseItemResponse
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPieceQuantity { get; set; }
    public string AssociationProtocol { get; set; }
    public string AssociationErrorCode { get; set; }
    public string AssociationErrorDescription { get; set; }
    public int AssociationStatusId { get; set; }
    public DateTime? AssociationDate { get; set; }
    public DateTime? AssociationCheckDate { get; set; }
    public bool ResendAssociation { get; set; }
    public DateTime? ProcessDate { get; set; }
    public int RFBStatus { get; set; }
    public bool Resend { get; set; }
}

public class MasterHouseAssociationOpenMasterItem
{
    public string MasterNumber { get; set; }
    public List<MasterHouseAssociationOpenHouseItem> Houses { get; set; }
}

public class MasterHouseAssociationOpenHouseItem
{
    public int Id { get; set; }
    public string Number { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPieceQuantity { get; set; }
    public string MasterNumber { get; set; }
    public DateTime? ProcessDate { set; get; }
}

public class SubmitRFBMasterHouseRequest
{
    public int FreightFowarderId { get; set; }
    public int[] AssociationIds { get; set; }
}

public class MasterHouseCancelarAssociacaoRequest
{
    public int AssociationId { get; set; }
}