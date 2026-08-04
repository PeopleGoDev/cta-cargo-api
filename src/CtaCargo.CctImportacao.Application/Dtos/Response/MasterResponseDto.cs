using CtaCargo.CctImportacao.Application.Dtos.Enum;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CtaCargo.CctImportacao.Application.Dtos.Response;

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
    public ICollection<MasterInstrucaoManuseioDto> InstrucaoManuseios { get; set; } = [];
    public bool Reenviar { get; set; }
    public RecordStatus StatusVoo { get; set; } 
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class MasterErroDto
{
    public string Erro { get; set; }
}


public class MasterInstrucaoManuseioDto
{
    public int Id { get; set; }

    public SpecialHanglingType Tipo { get; set; }

    public string Codigo { get; set; }

    public string Descricao { get; set; }
}