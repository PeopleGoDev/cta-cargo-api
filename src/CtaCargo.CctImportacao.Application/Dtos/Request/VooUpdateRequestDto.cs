using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class VooUpdateRequestDto
    {
        public int VooId { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime? DataHoraSaidaEstimada { get; set; }
        public DateTime? DataHoraSaidaReal { get; set; }
        public DateTime? DataHoraChegadaEstimada { get; set; }
        public DateTime? DataHoraChegadaReal { get; set; }
        public int UsuarioModificadorId { get; set; }
        public string AeroportoOrigemCodigo { get; set; }
        public string AeroportoDestinoCodigo { get; set; }
    }
}
