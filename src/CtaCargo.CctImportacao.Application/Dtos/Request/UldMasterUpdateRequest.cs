using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class UldMasterUpdateRequest: UldMasterBaseDto
{
    public int Id { get; set; }
    public int TrechoId { get; set; }
}

public class UldMasterPatchRequest
{
    public int TrechoId { get; set; }
    public string UldId { get; set; }
    public string UldCaracteristicaCodigo { get; set; }
    public string UldIdPrimario { get; set; }
    public string OriginalUldId { get; set; }
    public string OriginalUldCaracteristicaCodigo { get; set; }
    public string OriginalUldIdPrimario { get; set; }
    public List<UldMasterPatchItemRequest> Masters { get; set; }
}
public class UldMasterPatchItemRequest
{
    public int? Id { get; set; }
    public string MasterNumero { get; set; }
    public int QuantidadePecas { get; set; }
    public decimal Peso { get; set; }
    public string PesoUN { get; set; }
    public bool Transferencia { get; set; }
    public string TipoDivisao { get; set; }
    public string AeroportoOrigem { get; set; }
    public string AeroportoDestino { get; set; }
    public string DescricaoMercadoria { get; set; }
}