using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Domain.Dtos
{
    public class UldMasterDeleteByTagInput
    {
        public int VooId { get; set; }
        public string ULDId { get; set; }
        public string ULDCaracteristicaCodigo { get; set; }
        public string ULDIdPrimario { get; set; }
    }

    public class UldMasterDeleteByIdInput
    {
        public int VooId { get; set; }
        public List<int> ListaIds { get; set; }
    }
}
