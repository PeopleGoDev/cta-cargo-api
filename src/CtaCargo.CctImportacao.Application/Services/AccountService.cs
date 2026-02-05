using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Domain.Validator;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICertificadoDigitalRepository _certificadoDigitalRepository;
    private readonly IEmpresaRepository _empresaRepository;
    private readonly ITokenService _tokenSerice;
    private readonly IMapper _mapper;
    public AccountService(
        IUsuarioRepository usuarioRepository,
        IMapper mapper,
        ITokenService tokenSerice,
        ICertificadoDigitalRepository certificadoDigitalRepository,
        IEmpresaRepository empresaRepository)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _tokenSerice = tokenSerice;
        _certificadoDigitalRepository = certificadoDigitalRepository;
        _empresaRepository = empresaRepository;
    }

    public async Task<UsuarioLoginResponse> AutenticarUsuario(UsuarioLoginRequest request)
    {
        UsuarioLoginRequestValidator validator = new UsuarioLoginRequestValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
            throw new BusinessException(result.Errors[0].ErrorMessage);

        Usuario user = await _usuarioRepository.GetUsuarioByAuthentication(request.Email, request.Senha) ??
            throw new BusinessException("Usuário/Senha Invalido!");

        // A requisição vem com a Nova Senha
        if (request.AlterarSenha ?? false)
        {
            user.Senha = request.NovaSenha;
            user.AlterarSenha = false;
            _usuarioRepository.UpdateUsuario(user);
            await _usuarioRepository.SaveChanges();
        }

        if (user.AlterarSenha)
        {
            return new UsuarioLoginResponse
            {
                AlterarSenha = true
            };
        }

        if (user.MultiCompany)
        {
            return new UsuarioLoginResponse
            {
                SelectCompany = true,
                SelectCompanies = await GetAllCompaniesAsync(),
                AccessToken = _tokenSerice.GenerateMultiCompanyToken(user)
            };
        }

        UsuarioInfoResponse userReaddto = _mapper.Map<UsuarioInfoResponse>(user);

        userReaddto.UserProfile = user.Perfil.ToString();

        if (user.CertificadoId is not null)
        {
            var certificate = await _certificadoDigitalRepository.GetCertificadoDigitalById(user.CertificadoId.Value);
            if (certificate is not null)
            {
                userReaddto.CertificateExpiration = certificate.DataVencimento;
                userReaddto.CertificateOwner = certificate.Owner;
                userReaddto.CertificateOwnerId = certificate.OwnerId;
            }
        }

        return new()
        {
            AccessToken = _tokenSerice.GenerateToken(user),
            UsuarioInfo = userReaddto,
            AlterarSenha = false
        };
    }

    public async Task<UsuarioLoginResponse> SwitchCompany(UserSession userSession, int companyId)
    {
        Usuario user = await _usuarioRepository.GetUsuarioById(userSession.UserId) ??
            throw new BusinessException("Usuário Inválido!");

        Empresa empresa = await _empresaRepository.GetEmpresasByIdAsync(companyId) ??
            throw new BusinessException("Empresa Invalida!");

        user.Empresa = empresa;
        user.EmpresaId = companyId;

        UsuarioInfoResponse userReaddto = _mapper.Map<UsuarioInfoResponse>(user);

        userReaddto.UserProfile = user.Perfil.ToString();

        var token = _tokenSerice.GenerateToken(user);

        if (user.CertificadoId is not null)
        {
            var certificate = await _certificadoDigitalRepository.GetCertificadoDigitalById(user.CertificadoId.Value);
            if (certificate is not null)
            {
                userReaddto.CertificateExpiration = certificate.DataVencimento;
                userReaddto.CertificateOwner = certificate.Owner;
                userReaddto.CertificateOwnerId = certificate.OwnerId;
            }
        }

        return new()
        {
            AccessToken = token,
            UsuarioInfo = userReaddto,
            AlterarSenha = false
        };
    }

    private async Task<IEnumerable<UserSelectCompany>> GetAllCompaniesAsync()
    {
        var response = await _empresaRepository.GetAllEmpresasAsync();

        return response.Select(c => new UserSelectCompany
        {
            Id = c.Id,
            Name = c.RazaoSocial
        });
    }
}
