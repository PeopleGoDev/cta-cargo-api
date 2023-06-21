using System;

namespace CtaCargo.CctImportacao.Application.Dtos
{
    public class HouseBaseDto
    {
        public string Numero { get; set; }
        public double PesoTotalBruto { get; set; }
        public string PesoTotalBrutoUN { get; set; }
        public int TotalVolumes { get; set; }
        public double? Volume { get; set; }
        public string VolumeUN { get; set; }
        public decimal ValorFretePP { get; set; }
        public string ValorFretePPUN { get; set; }
        public decimal ValorFreteFC { get; set; }
        public string ValorFreteFCUN { get; set; }
        public bool IndicadorMadeiraMacica { get; set; }
        public string DescricaoMercadoria { get; set; }
        public string CodigoRecintoAduaneiro { get; set; }
        public string RUC { get; set; }
        public string ConsignatarioNome { get; set; }
        public string ConsignatarioEndereco { get; set; }
        public string ConsignatarioPostal { get; set; }
        public string ConsignatarioCidade { get; set; }
        public string ConsignatarioPaisCodigo { get; set; }
        public string ConsignatarioSubdivisao { get; set; }
        public string ConsignatarioCNPJ { get; set; }
        public string AeroportoOrigem { get; set; }
        public string AeroportoDestino { get; set; }
        public DateTime? DataEmissaoXML { get; set; }
        public string MasterNumeroXML { get; set; }
        public string[] NCMLista { get; set; }
        public string GetNCMListaString()
        {
            if (NCMLista == null)
                return null;

            if (NCMLista.Length == 0)
                return null;

            return String.Join(",", NCMLista);
        }
        public string RemetenteNome { get; set; }
        public string RemetenteEndereco { get; set; }
        public string RemetentePostal { get; set; }
        public string RemetenteCidade { get; set; }
        public string RemetentePaisCodigo { get; set; }
        public string AgenteDeCargaNumero { get; set; }
    }
}
