using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Domain.Validator;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICertificadoDigitalRepository _certificadoDigitalRepository;
    private readonly ITokenService _tokenSerice;
    private readonly IMapper _mapper;
    public AccountService(
        IUsuarioRepository usuarioRepository,
        IMapper mapper,
        ITokenService tokenSerice,
        ICertificadoDigitalRepository certificadoDigitalRepository = null)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _tokenSerice = tokenSerice;
        _certificadoDigitalRepository = certificadoDigitalRepository;
    }

    public async Task<UsuarioLoginResponse> AutenticarUsuario(UsuarioLoginRequest usuarioLogin)
    {
        UsuarioLoginRequestValidator validator = new UsuarioLoginRequestValidator();
        var result = validator.Validate(usuarioLogin);

        if (!result.IsValid)
            throw new BusinessException(result.Errors[0].ErrorMessage);

        Usuario user = 
            await _usuarioRepository.GetUsuarioByAuthentication(usuarioLogin.Email, usuarioLogin.Senha) ?? 
            throw new BusinessException("Usuário/Senha Invalido!");

        UsuarioInfoResponse userReaddto = _mapper.Map<UsuarioInfoResponse>(user);

        string token = null;
        bool alteraSenha = user.AlterarSenha;
        if (!user.AlterarSenha)
        {
            userReaddto.UserProfile = user.Perfil.ToString();
            token = _tokenSerice.GenerateToken(user);
        }

        if (usuarioLogin.AlterarSenhar)
        {
            user.Senha = usuarioLogin.NovaSenha;
            user.AlterarSenha = false;
            _usuarioRepository.UpdateUsuario(user);
            await _usuarioRepository.SaveChanges();
            alteraSenha = false;
            token = _tokenSerice.GenerateToken(user);
        }

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
            AlterarSenha = alteraSenha
        };
    }
}
