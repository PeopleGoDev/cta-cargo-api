using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface ICertificadoDigitalService
    {
        Task<ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>> ListarCertificadosDigitais(int empresaId);
    }
}