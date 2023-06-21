using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class VooInsertRequestDto
    {
        public string Numero { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime? DataHoraSaidaEstimada { get; set; }
        public DateTime? DataHoraSaidaReal { get; set; }
        public DateTime? DataHoraChegadaEstimada { get; set; }
        public DateTime? DataHoraChegadaReal { get; set; }
        public int PortoOrigemId { get; set; }
        public int PortoDestinoId { get; set; }
        public double? PesoBruto { get; set; }
        public string PesoBrutoUnidade { get; set; }
        public double? Volume { get; set; }
        public string VolumeUnidade { get; set; }
        public int? TotalPacotes { get; set; }
        public int? TotalPecas { get; set; }
        public int UsuarioInsercaoId { get; set; }
        public string AeroportoOrigemCodigo { get; set; }
        public string AeroportoDestinoCodigo { get; set; }
    }
}
