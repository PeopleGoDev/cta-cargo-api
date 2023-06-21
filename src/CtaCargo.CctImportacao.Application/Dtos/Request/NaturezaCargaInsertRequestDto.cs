namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class NaturezaCargaInsertRequestDto
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int UsuarioInsercaoId { get; set; }
        public int EmpresaId { get; set; }
    }
}
