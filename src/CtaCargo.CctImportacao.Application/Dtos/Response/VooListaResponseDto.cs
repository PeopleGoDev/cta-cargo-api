using CtaCargo.CctImportacao.Application.Dtos.Enum;
using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class VooListaResponseDto
    {
        public int VooId { get; set; }
        public string Numero { get; set; }
        public RecordStatus SituacaoVoo { get; set; }
        public string CiaAereaNome { get; set; }
        public DateTime? CertificadoValidade { get; set; }
    }
}
