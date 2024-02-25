using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Cache;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICacheService _cacheService;
    private readonly ICertificadoDigitalRepository _certificadoDigitalRepository;
    private readonly IConfiguraRepository _configuraRepository;
    private readonly ITokenService _tokenSerice;
    private readonly IMapper _mapper;
    public AccountService(
        IUsuarioRepository usuarioRepository,
        IConfiguraRepository configuraRepository,
        IMapper mapper,
        ITokenService tokenSerice,
        ICacheService cacheService,
        ICertificadoDigitalRepository certificadoDigitalRepository = null)
    {
        _usuarioRepository = usuarioRepository;
        _configuraRepository = configuraRepository;
        _mapper = mapper;
        _tokenSerice = tokenSerice;
        _cacheService = cacheService;
        _certificadoDigitalRepository = certificadoDigitalRepository;
    }

    public async Task<UsuarioLoginResponse> AutenticarUsuario(UsuarioLoginRequest usuarioLogin)
    {
        UsuarioLoginRequestValidator validator = new UsuarioLoginRequestValidator();
        var result = validator.Validate(usuarioLogin);

        if (!result.IsValid)
            throw new BusinessException(result.Errors[0].ErrorMessage);

        Usuario user = await _usuarioRepository.GetUsuarioByAuthentication(usuarioLogin.Email, usuarioLogin.Senha);
        if (user == null)
            throw new BusinessException("Usuário/Senha Invalido!");

        string token = null;
        UsuarioInfoResponse userReaddto = null;

        bool alteraSenha = user.AlterarSenha;
        if (!user.AlterarSenha)
        {
            token = _tokenSerice.GenerateToken(user);
            userReaddto = _mapper.Map<UsuarioInfoResponse>(user);
            userReaddto.UserProfile = user.Perfil.ToString();
        }

        if (usuarioLogin.AlterarSenhar)
        {
            user.Senha = usuarioLogin.NovaSenha;
            user.AlterarSenha = false;
            _usuarioRepository.UpdateUsuario(user);
            await _usuarioRepository.SaveChanges();
            token = _tokenSerice.GenerateToken(user);
            userReaddto = _mapper.Map<UsuarioInfoResponse>(user);
            alteraSenha = false;
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
