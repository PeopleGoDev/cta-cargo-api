using System;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Context
{
    public class ResumoVooUldView
    {
        public int? MasterId { get; set; }
        public int VooId { get; set; }
        public int?  ULDMasterId {get;set;} 
        public int Id { get; set; }
        public int VooId2 { get; set; }
        public string ULDCaracteristicaCodigo { get; set; }
        public string ULDId { get; set; }
        public string ULDIdPrimario { get; set; } 
        public decimal? Peso { get; set; }
        public int? QuantidadePecas { get; set; }
        public string TotalParcial { get; set; }
        public string MasterNumero { get; set; }
        public double? PesoTotalBruto { get; set; }
        public string PesoTotalBrutoUN { get; set; } 
        public int? TotalPecas { get; set; }
        public string VooNumero { get; set; }
        public DateTime? DataHoraChegadaEstimada { get; set; }
        public DateTime? DataHoraChegadaReal { get; set; }
    }
}
