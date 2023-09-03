using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos
{
    public class UldMasterBaseDto
    {
        public string MasterNumero { get; set; }
        public string UldId { get; set; }
        public string UldCaracteristicaCodigo { get; set; }
        public string UldIdPrimario { get; set; }
        public int QuantidadePecas { get; set; }
        public decimal Peso { get; set; }
        public string PesoUN { get; set; }
        public bool Transferencia { get; set; }
        public string TipoDivisao { get; set; }
    }
}
