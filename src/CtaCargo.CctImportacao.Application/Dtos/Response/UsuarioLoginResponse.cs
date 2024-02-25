using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class UsuarioLoginResponse
{
    public string AccessToken { get; set; }
    public UsuarioInfoResponse UsuarioInfo { get; set; }
    public bool AlterarSenha { get; set; }
}

public class UsuarioInfoResponse
{
    public int UsuarioId { get; set; }
    public int EmpresaId { get; set; }
    public string EmpresaNome { get; set; }
    public string EmpresaLogoUrl { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Email { get; set; }
    public string CompanhiaId { get; set; }
    public string CompanhiaNome { get; set; }
    public bool AlteraCompanhia { get; set; }
    public bool AcessoUsuarios { get; set; }
    public bool AcessoClientes { get; set; }
    public bool AcessoCompanhias { get; set; }
    public DateTime DataAlteracao { get; set; }
    public string UrlFoto { get; set; }
    public string UserProfile { get; set; }
    public DateTime? CertificateExpiration { get; set; }
    public string CertificateOwner { get; set; }
    public string CertificateOwnerId { get; set; }
}

public class UsuarioToken
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
}

public class UsuarioClaim
{
    public string Value { get; set; }
    public string Type { get; set; }
}
