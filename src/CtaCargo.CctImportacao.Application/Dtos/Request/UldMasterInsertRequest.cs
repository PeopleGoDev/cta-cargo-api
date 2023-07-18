using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class UldMasterInsertRequest: UldMasterBaseDto
    {
        public int TrechoId { get; set; }
    }
}
