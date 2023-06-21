namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class MasterUpdateTotalParcialRequestDto
    {
        public int MasterId { get; set; }
        public int UsuarioAlteradorId { get; set; }
        public string TotalParcial { get; set; }
    }
}
