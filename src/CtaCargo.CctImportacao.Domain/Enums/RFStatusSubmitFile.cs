namespace CtaCargo.CctImportacao.Domain.Enums;

public enum RFStatusSubmitFile
{
    Received,
    Processed,
    Rejected
}

public enum CargoHangling: int
{
    SpecialTreament = 0,
    SpecialService = 1,
    OtherServiceInformation = 2,
}