using AutoMapper.Mappers;
using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class VooResponseDto
{
    public int VooId { get; set; }
    public string Numero { get; set; }
    public DateTime DataVoo { get; set; }
    public DateTime? DataHoraSaidaReal { get; set; }
    public DateTime? DataHoraChegadaEstimada { get; set; }
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
    public List<VooTrechoResponse> Trechos { get; set; }

    public static implicit operator VooResponseDto(Voo voo)
    {
        return new VooResponseDto
        {
            AeroportoDestinoCodigo = voo.AeroportoDestinoCodigo,
            AeroportoOrigemCodigo = voo.AeroportoOrigemCodigo,
            DataCriacao = voo.CreatedDateTimeUtc,
            DataHoraChegadaEstimada = voo.DataHoraChegadaEstimada,
            DataHoraSaidaReal = voo.DataHoraSaidaReal,
            DataProtocoloRFB = voo.DataProtocoloRFB,
            DataVoo = voo.DataVoo,
            ErroCodigoRFB = voo.CodigoErroRFB,
            ErroDescricaoRFB = voo.DescricaoErroRFB,
            Numero = voo.Numero,
            ProtocoloRFB = voo.ProtocoloRFB,
            Reenviar = voo.Reenviar,
            SituacaoRFBId = (int)voo.SituacaoRFBId,
            StatusId = voo.StatusId,
            UsuarioCriacao = voo.UsuarioCriacaoInfo?.Nome,
            VooId = voo.Id,
            Trechos = (from c in voo.Trechos
                       select new VooTrechoResponse(c.Id, c.AeroportoDestinoCodigo, c.DataHoraChegadaEstimada, c.DataHoraSaidaEstimada))
                           .ToList()
        };
    }
}

public class VooUploadResponse: VooResponseDto
{
    public List<UldMasterNumeroQuery> ULDs { get; set; }
}

public record VooTrechoResponse (int Id, string AeroportoDestinoCodigo, DateTime? DataHoraChegadaEstimada = null, DateTime? DataHoraSaidaEstimada = null);