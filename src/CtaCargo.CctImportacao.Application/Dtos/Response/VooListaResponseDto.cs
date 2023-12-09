using CtaCargo.CctImportacao.Application.Dtos.Enum;
using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class VooListaResponseDto
{
    public int VooId { get; set; }
    public string Numero { get; set; }
    public VooType FlightType { get; set; }
    public RecordStatus SituacaoVoo { get; set; }
    public string CiaAereaNome { get; set; }
    public DateTime? CertificadoValidade { get; set; }
    public bool GhostFlight { get; set; }
    public IEnumerable<VooTrechoResponse> Trechos { get; set; }
}
