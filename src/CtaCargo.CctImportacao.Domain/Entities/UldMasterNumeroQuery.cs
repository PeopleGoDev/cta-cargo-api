using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class UldMasterNumeroQuery
    {
        public string ULDCaracteristicaCodigo { get; set; }
        public string ULDId { get; set; }
        public string ULDIdPrimario { get; set; }
        public string ULDLinha
        {
            get
            {
                return ULDCaracteristicaCodigo + ULDId + ULDIdPrimario;
            }
        }
        public IEnumerable<UldMasterNumeroQueryChildren> ULDs { get; set; }
    }

    public class UldMasterNumeroQueryChildren
    {
        public int MasterId { get; set; }
        public string MasterNumero { get; set; }
        public string UldId { get; set; }
        public string UldCaracteristicaCodigo { get; set; }
        public string UldIdPrimario { get; set; }
        public int? QuantidadePecas { get; set; }
        public decimal? Peso { get; set; }
        public string PesoUnidade { get; set; }
        public int Id { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime DataCricao { get; set; }
        public string TotalParcial { get; set; }
    }

    public class MasterNumeroUldSumario
    {
        public string MasterNumero { get; set; }
        public int? MasterPecas { get; set; }
        public double? MasterPeso { get; set; }
        public string MasterPesoUnidade { get; set; }
        public List<MasterNumeroUldSumarioChildren> Ulds { get; set; }
    }

    public class MasterNumeroUldSumarioChildren
    {
        public string UldNumero { get; set; }
        public int QuantidadePecas { get; set; }
        public double Peso { get; set; }
        public string PesoUnidade { get; set; }
        public string TotalParcial { get; set; }
        public bool MesmoVoo { get; set; }
        public string VooNumero { get; set; }
        public DateTime? DataHoraChegadaEstimada { get; set; }
        public DateTime? DataHoraChegadaReal { get; set; }
    }
}
