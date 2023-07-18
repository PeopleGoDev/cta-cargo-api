using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class UldMasterUpdateRequest: UldMasterBaseDto
    {
        public int Id { get; set; }
        public int TrechoId { get; set; }
    }
}
