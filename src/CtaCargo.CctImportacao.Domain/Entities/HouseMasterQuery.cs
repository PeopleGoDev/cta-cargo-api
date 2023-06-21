namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class HouseMasterQuery
    {
        public string Numero { get; set; }
        public double PesoTotalBruto { get; set; }
        public string PesoTotalBrutoUN { get; set; }
        public int TotalVolumes { get; set; }
        public decimal ValorFretePP { get; set; }
        public string ValorFretePPUN { get; set; }
        public decimal ValorFreteFC { get; set; }
        public string ValorFreteFCUN { get; set; }
        public string DescricaoMercadoria { get; set; }
        public string ConsignatarioNome { get; set; }
        public string ConsignatarioCidade { get; set; }
        public string ConsignatarioPaisCodigo { get; set; }
        public string ConsignatarioCNPJ { get; set; }
        public int? AeroportoOrigemId { get; set; }
        public int? AeroportoDestinoId { get; set; }
    }
}
