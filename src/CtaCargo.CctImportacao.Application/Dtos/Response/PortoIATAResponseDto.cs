﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class PortoIataResponseDto
{
    public int PortoId { get; set; }
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string CountryCode { get; set; }
}
