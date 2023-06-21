using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
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
}
