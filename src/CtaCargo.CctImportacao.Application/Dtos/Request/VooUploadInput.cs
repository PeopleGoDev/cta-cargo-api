using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class VooUploadInput
{
    public int VooId { get; set; }
}

public class ConfirmDepartureRequest
{
    public int FlightId { get; set; }
    public int ItineraryId { get; set; }
    public DateTime DepartureTime { get; set; }
}