namespace CtaCargo.CctImportacao.Api.Infrastructure.Extensions
{
    public class TokenJwtSettings
    {
        public string Secret { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
        public int ExpiracaoEmHoras { get; set; }
    }
}
