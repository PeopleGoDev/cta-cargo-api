using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class PortoIataUpdateRequestDto
{
    public int PortoIataId { get; set; }
    public string Nome { get; set; }
    public string CountryCode { get; set; }
    
}
