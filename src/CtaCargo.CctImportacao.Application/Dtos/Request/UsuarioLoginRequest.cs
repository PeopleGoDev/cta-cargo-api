namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class UsuarioLoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool AlterarSenhar { get; set; }
        public string NovaSenha { get; set; }
        public string NovaSenhaConfirmacao { get; set; }
    }
}
