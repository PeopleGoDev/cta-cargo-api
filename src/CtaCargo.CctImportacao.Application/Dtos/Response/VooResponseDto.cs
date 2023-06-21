using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class VooResponseDto
    {
        public int VooId { get; set; }
        public string Numero { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime? DataHoraSaidaEstimada { get; set; }
        public DateTime? DataHoraSaidaReal { get; set; }
        public DateTime? DataHoraChegadaEstimada { get; set; }
        public DateTime? DataHoraChegadaReal { get; set; }
        public double? PesoBruto { get; set; }
        public string PesoBrutoUnidade { get; set; }
        public double? Volume { get; set; }
        public string VolumeUnidade { get; set; }
        public int? TotalPacotes { get; set; }
        public int? TotalPecas { get; set; }
        public int StatusId { get; set; }
        public int SituacaoRFBId { get; set; }
        public string ProtocoloRFB { get; set; }
        public string ErroCodigoRFB { get; set; }
        public string ErroDescricaoRFB { get; set; }
        public DateTime? DataProtocoloRFB { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string AeroportoOrigemCodigo { get; set; }
        public string AeroportoDestinoCodigo { get; set; }
        public bool Reenviar { get; set; }
    }

    public class VooUploadResponse: VooResponseDto
    {
        public List<UldMasterNumeroQuery> ULDs { get; set; }
    }
}
