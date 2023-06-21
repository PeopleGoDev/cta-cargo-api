using System.IO;

namespace CtaCargo.CctImportacao.Application.Support.Contracts
{
    public interface IDownloadArquivoCertificado
    {
        byte[] GetCertificateStream(string fileName);
    }
}