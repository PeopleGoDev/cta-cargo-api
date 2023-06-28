using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class SubmeterRFBHouseRequest
{
    public DateTime DataProcessamento { get; set; }
    public int AgenteDeCargaId { get; set; }
}

public class SubmeterRFBMasterHouseRequest
{
    public int FreightFowarderId { get; set; }
    public List<SubmeterRFBMasterHouseItemRequest> Masters { get; set; }

}

public class SubmeterRFBMasterHouseItemRequest
{
    public string MasterNumber { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPiece { get; set; }
}

public class MasterHouseAssociationUploadResponse
{
    public string Number { get; set; }
    public MasterHouseAssociationSummaryUploadResponse? Summary { get; set; }
    public List<MasterHouseAssociationHouseItemResponse> Houses { get; set; }
}

public class MasterHouseAssociationSummaryUploadResponse
{
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int ConsignmentItemQuantity { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPieceQuantity { get; set; }
    public DateTime IssueDate { get; set; }
    public string DocumentId { get; set; }
}

public class MasterHouseAssociationHouseItemResponse
{
    public string Number { get; set; }
    public string OriginLocation { get; set; }
    public string DestinationLocation { get; set; }
    public double TotalWeight { get; set; }
    public string TotalWeightUnit { get; set; }
    public int PackageQuantity { get; set; }
    public int TotalPieceQuantity { get; set; }
    public string DocumentId { get; set; }
    public string AssociationProtocol { get; set; }
    public string AssociationErrorCode { get; set; }
    public string AssociationErrorDescription { get; set; }
    public int AssociationStatusId { get; set; }
    public DateTime? AssociationDate { get; set; }
    public DateTime? AssociationCheckDate { get; set; }
    public bool ResendAssociation { get; set; }
    public DateTime? ProcessDate { get; set; }
}