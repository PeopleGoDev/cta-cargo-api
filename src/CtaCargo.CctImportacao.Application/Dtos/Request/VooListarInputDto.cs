using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class VooListarInputDto
{
    public DateTime? DataVoo { get; set; }
    public DateTime? DataInicial { get; set; }
    public DateTime? DataFinal { get; set; }
}

public class CloneFlightForDeparturingRequest
{
    public int FlightId { get; set; }
    public int SegmentId { get; set; }
    public string FlightNumber { get; set;}
}