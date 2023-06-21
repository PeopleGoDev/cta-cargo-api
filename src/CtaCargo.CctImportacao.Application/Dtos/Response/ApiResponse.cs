using CtaCargo.CctImportacao.Domain.Enums;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class ApiResponse<T> where T : class
    {
        public bool Sucesso { get; set; }
        public T Dados { get; set; }
        public IList<Notificacao> Notificacoes { get; set; }
    }

    public class Notificacao
    {
        public Notificacao() { }

        public Notificacao(CodigoNotificacao codigoErro, string mensagem)
        {
            Codigo = codigoErro.GetHashCode().ToString();
            Mensagem = mensagem;
        }

        public string Codigo { get; set; }
        public string Mensagem { get; set; }
    }
}
