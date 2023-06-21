using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Support
{
    public class DownloadArquivoCertificadoAzureStorage : IDownloadArquivoCertificado
    {
        private readonly IConfiguration _configuration;
        private readonly string _storageConnectionString;
        private readonly string _storageSharedFolder;
        public DownloadArquivoCertificadoAzureStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _storageConnectionString = _configuration.GetConnectionString("AzureStorageConnectionString");
            _storageSharedFolder = _configuration.GetConnectionString("AzureStorageSharedFolder");
        }

        public byte[] GetCertificateStream(string fileName)
        {
            try
            {
                ShareClient share = new ShareClient(_storageConnectionString, _storageSharedFolder);
                if (share.Exists())
                {
                    ShareDirectoryClient directory = share.GetDirectoryClient("files");
                    ShareFileClient file = directory.GetFileClient(fileName);

                    if (file.Exists())
                    {
                        ShareFileDownloadInfo download = file.Download();
                        using (var memoryStream = new MemoryStream())
                        {
                            download.Content.CopyTo(memoryStream);
                            return ReadFile(memoryStream);
                        }
                    }
                    // Download the file
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        private byte[] ReadFile(MemoryStream stream) => stream.ToArray();

    }
}
