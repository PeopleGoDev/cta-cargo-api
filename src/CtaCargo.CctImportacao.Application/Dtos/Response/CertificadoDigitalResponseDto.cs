using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public enum CertificateOwnerType
{
    User,
    Company
}
public class CertificadoDigitalResponseDto
{
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public int? UsuarioModificadorId { get; set; }
    public string UsuarioModificacao { get; set; }
    public DateTime? DataModificacao { get; set; }
    public int Id { get; set; }
    public string Arquivo { get; set; }
    public DateTime DataVencimento { get; set; }
    public string NomeDono { get; set; }
    public string SerialNumber { get; set; }
}

public class DigitalCertificateUserRelatedResponse
{
    public List<DigitalCertificateUserRelatedItemResponse> Certificates { get; set; } = new();
}

public class DigitalCertificateUserRelatedItemResponse
{
    public int Id { get; set; }
    public string Arquivo { get; set; }
    public string? CompanyName { get; set; }
    public DateTime DataVencimento { get; set; }
    public string NomeDono { get; set; }
    public string SerialNumber { get; set; }
    public CertificateOwnerType OwnerType { get; set; }
}