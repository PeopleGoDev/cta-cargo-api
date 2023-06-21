using CtaCargo.CctImportacao.Application.Dtos.Enum;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class VooListaResponseDto
    {
        public int VooId { get; set; }
        public string Numero { get; set; }
        public RecordStatus SituacaoVoo { get; set; }
    }
}
