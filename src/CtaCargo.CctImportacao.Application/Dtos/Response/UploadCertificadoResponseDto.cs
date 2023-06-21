using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class UploadCertificadoResponseDto
    {
        public string NomeArquivo { get; set; }
        public DateTime DataVencimento { get; set; }
    }
}
