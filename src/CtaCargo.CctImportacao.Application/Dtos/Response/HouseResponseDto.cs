using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class HouseResponseDto: HouseBaseDto
    {
        public int HouseId { get; set; }
        public int StatusId { get; set; }
        public int SituacaoRFB { get; set; }
        public string ProtocoloRFB { get; set; }
        public string DescricaoErroRFB { get; set; }
        public DateTime DataProcessamento { get; set; }
        public bool Reenviar { get; set; }


        public string ProtocoloAssociacaoRFB { get; set; }
        public string CodigoErroAssociacaoRFB { get; set; }
        public string DescricaoErroAssociacaoRFB { get; set; }
        public int SituacaoAssociacaoRFBId { get; set; } // “Received” , “Rejected”, “Processed”
        public DateTime? DataProtocoloAssociacaoRFB { get; set; }
        public DateTime? DataChecagemAssociacaoRFB { get; set; }
        public bool ReenviarAssociacao { get; set; }
    }
}
