using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class VooUpdateRequestDto
{
    public int VooId { get; set; }
    public string? Numero { get; set; }
    public DateTime? DataVoo { get; set; }
    public DateTime? DataHoraSaidaReal { get; set; }
    public DateTime? DataHoraSaidaPrevista { get; set; }
    public string? AeroportoOrigemCodigo { get; set; }
    public List<VooUpdateTrechoRequest> Trechos { get; set; }
}

public class VooUpdateTrechoRequest
{
    public int? Id { get; set; }
    public string AeroportoDestinoCodigo { get; set; }
    public DateTime? DataHoraChegadaEstimada { get; set; }
    public DateTime? DataHoraSaidaEstimada { get; set; }
}