using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class FlightUploadRequest
{
    public int? FlightId { get; set; }
    public int? ItineraryId { get; set; }
    public DateTime? DepartureTime { get; set; }
    public int[] idList { get; set; }
}

public class FileUploadResponse
{
    public int Id { get; set; }
    public string Protocol { get; set; }
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
}