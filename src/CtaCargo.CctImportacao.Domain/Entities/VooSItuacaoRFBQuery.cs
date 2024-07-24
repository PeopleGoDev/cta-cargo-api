using CtaCargo.CctImportacao.Domain.Enums;

namespace CtaCargo.CctImportacao.Domain.Entities;

public class SituacaoRFBQuery
{
    public int Id { get; set; }
    public RFStatusEnvioType SituacaoRFB { get; set; }
    public string ProtocoloRFB { get; set; }
    public bool Reenviar { get; set; }
    public RFStatusEnvioType ScheduleSituationRFB { get; set; }
    public string ScheduleProtocolRFB { get; set; }
    public bool GhostFlight { get; set; }
}
