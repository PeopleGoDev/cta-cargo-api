using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class HouseListarRequest
    {
        public DateTime? DataProcessamento { get; set; }
        public int? AgenteDeCargaId { get; set; }
        public DateTime? DataCriacaoInicialUnica { get; set; }
        public DateTime? DataCriacaoFinal { get; set; }
        public string NumeroMaster { get; set; }
        public string Numero { get; set; }
        public string NomeLike { get; set; }
        public int? StatusReceita { get; set; }
        public DateTime? DataEnvioReceita { get; set; }
    }
}
