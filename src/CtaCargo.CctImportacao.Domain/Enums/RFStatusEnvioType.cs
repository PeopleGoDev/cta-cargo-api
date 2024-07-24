namespace CtaCargo.CctImportacao.Domain.Enums;

public enum RFStatusEnvioType
{
    NoSubmitted = 0,
    Received = 1,
    Processed = 2,
    Rejected = 3,
    ReceivedDeletion = 4,
    ProcessedDeletion = 5
}