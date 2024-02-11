using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class CertificadoDigitalService : ICertificadoDigitalService
{
    private readonly ICertificadoDigitalRepository _certificadoRepository;
    private readonly ICiaAereaRepository _ciareaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public CertificadoDigitalService(
        ICertificadoDigitalRepository certificadoRepository,
        IMapper mapper,
        IUsuarioRepository usuarioRepository,
        ICiaAereaRepository ciareaRepository)
    {
        _certificadoRepository = certificadoRepository;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _ciareaRepository = ciareaRepository;
    }

    public async Task<ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>> ListarCertificadosDigitais(int empresaId)
    {
        var lista = await _certificadoRepository.GetAllCertificadosDigital(empresaId);

        var dto = _mapper.Map<IEnumerable<CertificadoDigitalResponseDto>>(lista);

        return
                new ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<DigitalCertificateUserRelatedResponse>> GetCertificateOwner(UserSession userSession)
    {
        var user = await _usuarioRepository.GetUserCertificateById(userSession.UserId);

        var airs = await _ciareaRepository.GetAllCiaAereas(userSession.CompanyId);

        var response = new DigitalCertificateUserRelatedResponse();

        if (user?.CertificadoId is not null)
        {
            var certificateUser = await _certificadoRepository.GetCertificadoDigitalById((int)user?.CertificadoId);

            if (certificateUser != null)
            {
                response.Certificates.Add(new DigitalCertificateUserRelatedItemResponse
                {
                    Arquivo = certificateUser.Arquivo,
                    DataVencimento = certificateUser.DataVencimento,
                    Id = certificateUser.Id,
                    NomeDono = certificateUser.NomeDono,
                    SerialNumber = certificateUser.SerialNumber,
                    OwnerType = CertificateOwnerType.User
                });
            };
        }

        foreach(var air in airs)
        {
            if(air.CertificadoDigital is not null)
            {
                response.Certificates.Add(new DigitalCertificateUserRelatedItemResponse
                {
                    Arquivo = air.CertificadoDigital.Arquivo,
                    DataVencimento = air.CertificadoDigital.DataVencimento,
                    Id = air.CertificadoDigital.Id,
                    NomeDono = air.CertificadoDigital.NomeDono,
                    SerialNumber = air.CertificadoDigital.SerialNumber,
                    CompanyName = air.Nome,
                    OwnerType = CertificateOwnerType.Company
                });
            }
        }

        return
                new ApiResponse<DigitalCertificateUserRelatedResponse>
                {
                    Dados = response,
                    Sucesso = true,
                    Notificacoes = null
                };
    }
}
