using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class CertificadoDigitalService : ICertificadoDigitalService
    {
        private readonly ICertificadoDigitalRepository _certificadoRepository;
        private readonly IMapper _mapper;

        public CertificadoDigitalService(ICertificadoDigitalRepository certificadoRepository, IMapper mapper)
        {
            _certificadoRepository = certificadoRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>> ListarCertificadosDigitais(int empresaId)
        {
            try
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
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<CertificadoDigitalResponseDto>>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Erro na aplicação! {ex.Message} !"
                                }
                            }
                        };
            }

        }
    }
}
