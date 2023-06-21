using static CtaCargo.CctImportacao.Domain.Entities.Master;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class SituacaoRFBQuery
    {
        public int Id { get; set; }
        public RFStatusEnvioType SituacaoRFB { get; set; }
        public string ProtocoloRFB { get; set; }
        public bool Reenviar { get; set; }
    }
}
