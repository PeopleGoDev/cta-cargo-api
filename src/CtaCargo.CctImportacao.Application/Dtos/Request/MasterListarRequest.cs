using System;
using System.Collections.Generic;
using System.Text;
using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class MasterListarRequest
    {
        public int? CiaAereaId { get; set; }
        public int? VooId { get; set; }
        public DateTime? DataCriacaoInicialUnica { get; set; }
        public DateTime? DataCriacaoFinal { get; set; }
        public string Numero { get; set; }
        public string NomeLike { get; set; }
        public RFStatusEnvioType? StatusReceita { get; set; }
        public DateTime? DataEnvioReceita { get; set; }
    }
}
