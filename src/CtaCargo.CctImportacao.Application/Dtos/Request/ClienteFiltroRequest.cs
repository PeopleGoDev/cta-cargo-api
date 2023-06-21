using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class ClienteFiltroRequest
    {
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public string Potal { get; set; }
    }
}
