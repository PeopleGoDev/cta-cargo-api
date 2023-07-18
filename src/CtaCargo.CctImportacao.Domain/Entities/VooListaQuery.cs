using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class VooListaQuery
{
    public int VooId { get; set; }
    public string Numero { get; set; }
    public int SituacaoVoo { get; set; }
    public string CiaAereaNome { get; set; }
    public DateTime? CertificadoValidade { get; set; }
    public IEnumerable<VooTrechoQuery> Trechos { get; set; }
}

public record VooTrechoQuery(int id, string portoDestino);