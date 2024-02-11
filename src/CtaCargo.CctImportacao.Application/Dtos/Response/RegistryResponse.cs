using CtaCargo.CctImportacao.Application.Dtos.Request;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class RegistryResponse
{
    public string TaxId { get; set; }
    public string CompanyName { get; set; }
    public string Contact { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string CountryCode { get; set; }
    public string LogoUrl { get; set; }
}

public class RegistryInsertRequest : RegistryResponse { }

public class RegistryUpdateRequest : RegistryResponse { }

public class RegistryInsertResponse : RegistryResponse
{
    public CiaAereaInsertRequest AirCompany { get; set; }
}
public class RegistryUpdateResponse : RegistryResponse { }

public class RegistryAirCompanyInsertRequest : RegistryResponse
{
    public string Number { get; set; }
    public string CertificatePassword { get; set; }
}
