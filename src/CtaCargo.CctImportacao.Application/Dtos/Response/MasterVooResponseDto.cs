using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class MasterVooResponseDto
    {
        public string Numero { get; set; }
        public string CodigoTipo { get; set; } // ULD ou BLK
        public double Peso { get; set; }
        public string PesoUnidade { get; set; }
        public int TotalVolumes { get; set; }
        public string Descricao { get; set; }
        public int PortoOrigemId { get; set; }
        public int PortoDestinoId { get; set; }
    }
}
