using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using System.IO;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface IUploadService
    {
        Task<ApiResponse<UploadCertificadoResponseDto>> UploadArquivo(UploadFileRequest input, Stream fileStream);
    }
}