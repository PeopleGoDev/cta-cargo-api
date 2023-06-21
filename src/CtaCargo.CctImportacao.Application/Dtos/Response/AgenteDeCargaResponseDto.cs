using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class AgenteDeCargaResponseDto
    {
        public int AgenteDeCargaId { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Endereco1 { get; set; }
        public string Endereco2 { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Numero { get; set; }
        public string ArquivoCertificado { get; set; }
        public DateTime? DataExpiracaoCertificado { get; set; }
    }
}
