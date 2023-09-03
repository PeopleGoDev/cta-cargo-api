using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class VooInsertRequestDto
{
    public string Numero { get; set; }
    public DateTime DataVoo { get; set; }
    public DateTime? DataHoraSaidaReal { get; set; }
    public DateTime? DataHoraSaidaPrevista { get; set; }
    public string AeroportoOrigemCodigo { get; set; }
    public List<VooInsertTrechoRequest> Trechos { get; set; } 
}

public class VooInsertTrechoRequest
{
    public string AeroportoDestinoCodigo { get; set; }
    public DateTime? DataHoraChegadaEstimada { get; set; }
    public DateTime? DataHoraSaidaEstimada { get; set; }
}
