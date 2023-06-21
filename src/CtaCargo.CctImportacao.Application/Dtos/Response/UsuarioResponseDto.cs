using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class UsuarioResponseDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public int CompanhiaId { get; set; }
        public string CompanhiaNome { get; set; }
        public bool AlteraCompanhia { get; set; }
        public bool AcessoUsuarios { get; set; }
        public bool AcessoClientes { get; set; }
        public bool AcessoCompanhias { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Bloqueado { get; set; }
        public int? CertificadoDigitalId {get;set;}
    }
}
