namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class UsuarioRegistroRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaConfirmacao { get; set; }
    }
}
