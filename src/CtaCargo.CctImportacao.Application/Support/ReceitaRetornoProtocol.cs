using System;

namespace CtaCargo.CctImportacao.Application.Support;

public class ReceitaRetornoProtocol
{
    public string StatusCode { get; set; }
    public string Reason { get; set; }
    public DateTime? IssueDateTime { get; set; }
}
