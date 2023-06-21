namespace CtaCargo.CctImportacao.Domain.Entities
{
    public class MasterVooQuery
    {
        public string Numero { get; set; }
        public string CodigoTipo { get; set; } // ULD ou BLK
        public double Peso { get; set; }
        public string PesoUnidade { get; set; }
        public int TotalPecas { get; set; }
        public string Descricao { get; set; }
        public int? PortoOrigemId { get; set; }
        public int? PortoDestinoId { get; set; }
    }
}
