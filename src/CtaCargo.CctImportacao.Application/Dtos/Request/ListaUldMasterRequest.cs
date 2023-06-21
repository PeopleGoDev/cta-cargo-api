using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class ListaUldMasterRequest
    {
        public int vooId { get; set; }
        public string uldLinha { get; set; }
    }
}
