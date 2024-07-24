using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class PortoIataInsertRequestDto
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string CountryCode { get; set; }
}
