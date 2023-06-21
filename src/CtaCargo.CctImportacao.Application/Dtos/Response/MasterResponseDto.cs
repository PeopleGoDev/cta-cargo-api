using CtaCargo.CctImportacao.Application.Dtos.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class MasterResponseDto: MasterBaseDto
    {
        public int MasterId { get; set; }
        public int StatusId { get; set; }
        public int SituacaoRFB { get; set; }
        public string ProtocoloRFB { get; set; }
        public string CodigoErroRFB { get; set; }
        public string DescricoErroRFB { get; set; }
        public DateTime? DataProtocoloRFB { get; set; }
        public ICollection<MasterErroDto> Erros { get; set; }
        public bool Reenviar { get; set; }
        public RecordStatus StatusVoo { get; set; } 
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class MasterErroDto
    {
        public string Erro { get; set; }
    }
}
