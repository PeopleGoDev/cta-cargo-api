using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

public class VooResponseDto
{
    public int VooId { get; set; }
    public string Numero { get; set; }
    public VooType FlightType { get; set; }
    public DateTime DataVoo { get; set; }
    public DateTime? DataHoraSaidaReal { get; set; }
    public DateTime? DataHoraSaidaPrevista { get; set; }
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
    public string ParentFlightNumber { get; set; }
    public string CountryOrigin { get; set; }
    public string PrefixoAeronave { get; set; }
    public List<VooTrechoResponse> Trechos { get; set; }
    public int? ScheduleSituationRFB { get; set; }
    public string? ProtocoloScheduleRFB { get; set; }
    public string? ScheduleErrorCodeRFB { get; set; }
    public string? ScheduleErrorDescriptionRFB { get; set; }
    public bool GhostFlight { get; set; }

    public static implicit operator VooResponseDto(Voo voo)
    {
        return new VooResponseDto
        {
            AeroportoDestinoCodigo = voo.AeroportoDestinoCodigo,
            AeroportoOrigemCodigo = voo.AeroportoOrigemCodigo,
            DataCriacao = voo.CreatedDateTimeUtc,
            DataHoraChegadaEstimada = voo.DataHoraChegadaEstimada,
            DataHoraSaidaReal = voo.DataHoraSaidaReal,
            DataHoraSaidaPrevista = voo.DataHoraSaidaEstimada,
            DataProtocoloRFB = voo.DataProtocoloRFB,
            DataVoo = voo.DataVoo,
            ErroCodigoRFB = voo.CodigoErroRFB,
            ErroDescricaoRFB = voo.DescricaoErroRFB,
            Numero = voo.Numero,
            FlightType = voo.FlightType,
            ProtocoloRFB = voo.ProtocoloRFB,
            Reenviar = voo.Reenviar,
            SituacaoRFBId = (int)voo.SituacaoRFBId,
            StatusId = voo.StatusId,
            UsuarioCriacao = voo.UsuarioCriacaoInfo?.Nome,
            VooId = voo.Id,
            ParentFlightNumber = voo.ParentFlightInfo?.Numero,
            CountryOrigin = voo.CountryOrigin,
            PrefixoAeronave = voo.PrefixoAeronave,
            ScheduleSituationRFB = (int)voo.ScheduleSituationRFB,
            ProtocoloScheduleRFB = voo.ProtocoloScheduleRFB,
            ScheduleErrorCodeRFB = voo.ScheduleErrorCodeRFB,
            ScheduleErrorDescriptionRFB = voo.ScheduleErrorDescriptionRFB,
            GhostFlight = voo.GhostFlight,
            Trechos = (from c in voo.Trechos
                       select new VooTrechoResponse
                       {
                           Id = c.Id,
                           AeroportoDestinoCodigo = c.AeroportoDestinoCodigo,
                           DataHoraChegadaEstimada = c.DataHoraChegadaEstimada,
                           DataHoraSaidaEstimada = c.DataHoraSaidaEstimada,
                           PaisDestinoCodigo = c.PortoIataDestinoInfo?.SiglaPais
                       })
                       .ToList()
        };
    }
}

public class VooUploadResponse : VooResponseDto
{
    public static implicit operator VooUploadResponse(Voo voo)
    {
        return new VooUploadResponse
        {
            AeroportoDestinoCodigo = voo.AeroportoDestinoCodigo,
            AeroportoOrigemCodigo = voo.AeroportoOrigemCodigo,
            DataCriacao = voo.CreatedDateTimeUtc,
            DataHoraChegadaEstimada = voo.DataHoraChegadaEstimada,
            DataHoraSaidaReal = voo.DataHoraSaidaReal,
            DataHoraSaidaPrevista = voo.DataHoraSaidaEstimada,
            DataProtocoloRFB = voo.DataProtocoloRFB,
            DataVoo = voo.DataVoo,
            ErroCodigoRFB = voo.CodigoErroRFB,
            ErroDescricaoRFB = voo.DescricaoErroRFB,
            Numero = voo.Numero,
            FlightType = voo.FlightType,
            ProtocoloRFB = voo.ProtocoloRFB,
            Reenviar = voo.Reenviar,
            SituacaoRFBId = (int)voo.SituacaoRFBId,
            StatusId = voo.StatusId,
            UsuarioCriacao = voo.UsuarioCriacaoInfo?.Nome,
            VooId = voo.Id,
            ParentFlightNumber = voo.ParentFlightInfo?.Numero,
            CountryOrigin = voo.CountryOrigin,
            PrefixoAeronave = voo.PrefixoAeronave,
            ScheduleSituationRFB = (int)voo.ScheduleSituationRFB,
            ProtocoloScheduleRFB = voo.ProtocoloScheduleRFB,
            ScheduleErrorCodeRFB = voo.ScheduleErrorCodeRFB,
            ScheduleErrorDescriptionRFB = voo.ScheduleErrorDescriptionRFB,
            GhostFlight = voo.GhostFlight,
            Trechos = (from c in voo.Trechos
                       select new VooTrechoResponse
                       {
                           Id = c.Id,
                           AeroportoDestinoCodigo = c.AeroportoDestinoCodigo,
                           DataHoraChegadaEstimada = c.DataHoraChegadaEstimada,
                           DataHoraSaidaEstimada = c.DataHoraSaidaEstimada,
                           PaisDestinoCodigo = c.PortoIataDestinoInfo?.SiglaPais
                       })
                       .ToList()
        };
    }
}

public class VooTrechoResponse
{
    public int Id { get; set; }
    public string AeroportoDestinoCodigo { get; set; }
    public DateTime? DataHoraChegadaEstimada { get; set; }
    public DateTime? DataHoraSaidaEstimada { get; set; }
    public string PaisDestinoCodigo { get; set; }
    public List<UldMasterNumeroQuery> ULDs { get; set; }
}