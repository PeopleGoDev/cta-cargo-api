namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class NaturezaCargaUpdateRequestDto
    {
        public int NaturezaCargaId { get; set; }
        public string Descricao { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
