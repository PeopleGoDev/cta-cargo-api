namespace CtaCargo.CctImportacao.Application.Support.Contracts
{
    public interface ISendEmail
    {
        void Email(string emailTo, string subject, string htmlString);
    }
}