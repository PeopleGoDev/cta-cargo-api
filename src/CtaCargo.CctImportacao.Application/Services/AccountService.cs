using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Validator;
using CtaCargo.CctImportacao.Infrastructure.Data.Cache;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICacheService _cacheService;
    private readonly IConfiguraRepository _configuraRepository;
    private readonly ITokenService _tokenSerice;
    private readonly IMapper _mapper;
    public AccountService(
        IUsuarioRepository usuarioRepository,
        IConfiguraRepository configuraRepository,
        IMapper mapper,
        ITokenService tokenSerice,
        ICacheService cacheService)
    {
        _usuarioRepository = usuarioRepository;
        _configuraRepository = configuraRepository;
        _mapper = mapper;
        _tokenSerice = tokenSerice;
        _cacheService = cacheService;
    }

    public async Task<ApiResponse<UsuarioLoginResponse>> AutenticarUsuario(UsuarioLoginRequest usuarioLogin)
    {
        UsuarioLoginRequestValidator validator = new UsuarioLoginRequestValidator();
        var result = validator.Validate(usuarioLogin);

        if (!result.IsValid)
        {
            List<Notificacao> notificacoes = (from c in result.Errors
                                              select new Notificacao
                                              {
                                                  Codigo = "9999",
                                                  Mensagem = c.ErrorMessage
                                              }).ToList();

            return new ApiResponse<UsuarioLoginResponse>
            {
                Dados = null,
                Sucesso = false,
                Notificacoes = notificacoes
            };
        }

        Usuario user = await _usuarioRepository.GetUsuarioByAuthentication(usuarioLogin.Email, usuarioLogin.Senha);
        if (user == null)
            throw new BusinessException("Usuário/Senha Invalido!");

        string token = null;
        UsuarioInfoResponse userReaddto = null;

        bool alteraSenha = user.AlterarSenha;
        if (user.AlterarSenha == false)
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

        return
        new ApiResponse<UsuarioLoginResponse>
        {
            Dados = new UsuarioLoginResponse
            {
                AccessToken = token,
                UsuarioInfo = userReaddto,
                AlterarSenha = alteraSenha
            },
            Sucesso = true,
            Notificacoes = null
        };

    }

    public RegistryResponse GetRegistry(UserSession userSession)
    {
        var registry = _configuraRepository.GetCompanyById(userSession.CompanyId);

        if (registry == null)
            throw new BusinessException("Empresa não registrada");

        return new()
        {
            TaxId = registry.CNPJ,
            CompanyName = registry.RazaoSocial,
            Address = registry.Endereco,
            Address2 = registry.Complemento,
            PostalCode = registry.CEP,
            Province = registry.UF,
            City = registry.Cidade,
            CountryCode = registry.Pais,
            Contact = registry.Contato,
            Phone = registry.Telefone,
            Email = registry.EMail,
            LogoUrl = registry.LogoUrl
        };
    }

    public async Task<RegistryResponse> NewRegistryGet(string registrationToken)
    {
        var empresa = await _cacheService.GetData<Empresa>(registrationToken);

        if (empresa == null)
            return default;

        return new()
        {
            CompanyName = empresa.RazaoSocial,
            Address = empresa.Endereco,
            Address2 = empresa.Complemento,
            City = empresa.Cidade,
            CountryCode = empresa.Pais,
            Phone = empresa.Telefone,
            Email = empresa.EMail,
            Contact = empresa.Contato,
            LogoUrl = empresa.LogoUrl,
            PostalCode = empresa.CEP,
            Province = empresa.UF,
            TaxId = empresa.CNPJ
        };
    }

    public async Task<CiaAereaResponseDto> NewRegistryAirCompanyGet(string registrationToken)
    {
        registrationToken = $"{ registrationToken }-aircompany";
        var ciaAerea = await _cacheService.GetData<CiaAerea>(registrationToken);

        if (ciaAerea == null)
            return default;

        return new()
        {
            Cidade = ciaAerea.Cidade,
            CNPJ = ciaAerea.CNPJ,
            Endereco1 = ciaAerea.Endereco,
            Endereco2 = ciaAerea.Complemento,
            Estado = ciaAerea.UF,
            Nome = ciaAerea.Nome,
            Numero = ciaAerea.Numero,
            Pais = ciaAerea.Pais,
            CEP = ciaAerea.CEP,
            Contato = ciaAerea.Contato,
            Telefone = ciaAerea.Telefone,
            EMail = ciaAerea.EMail
        };
    }

    public async Task<RegistryInsertResponse> NewRegistryPost(string registrationToken, RegistryInsertRequest request)
    {
        var registry = _configuraRepository.GetCompanyByTaxId(request.TaxId);

        if (registry?.EMail?.ToLower().Trim() == request.Email.ToLower().Trim())
            throw new BusinessException("Empresa já registrada para esse E-mail. Siga com lembrar senha, caso necessário.");

        if (registry is not null)
            throw new BusinessException("Empresa já registrada!");

        registry = new()
        {
            CNPJ = request.TaxId,
            RazaoSocial = request.CompanyName,
            Endereco = request.Address,
            Complemento = request.Address2,
            Cidade = request.City,
            UF = request.Province,
            CEP = request.PostalCode,
            Pais = request.CountryCode,
            Contato = request.Contact,
            Telefone = request.Phone,
            EMail = request.Email,
            LogoUrl = request.LogoUrl
        };

        await _cacheService.SetData<Empresa>(registrationToken, registry, DateTime.Now);

        // _configuraRepository.UpdateCompany(registry);
        // _configuraRepository.SaveCompany();

        return new()
        {
            TaxId = registry.CNPJ,
            CompanyName = registry.RazaoSocial,
            Address = registry.Endereco,
            Address2 = registry.Complemento,
            PostalCode = registry.CEP,
            City = registry.Cidade,
            Province = registry.UF,
            CountryCode = registry.Pais,
            Contact = registry.Contato,
            Phone = registry.Telefone,
            Email = registry.EMail,
            LogoUrl = registry.LogoUrl
        };
    }

    public async Task NewRegistryFilePost(string registrationToken, string fileType, Stream stream)
    {
        registrationToken = $"{registrationToken}-{fileType}";

        await _cacheService.SetStreamData(registrationToken, stream);
    }

    public async Task DelRegistryFilePost(string registrationToken, string fileType)
    {
        registrationToken = $"{registrationToken}-{fileType}";

        await _cacheService.RemoveData(registrationToken);
    }
    public async Task<CiaAereaResponseDto> NewRegistryAirCompanyPost(string registrationToken, RegistryAirCompanyInsertRequest request)
    {
        registrationToken = $"{registrationToken}-aircompany";

        CiaAerea registry = new()
        {
            Nome = request.CompanyName,
            Endereco = request.Address,
            Complemento = request.Address2,
            UF = request.Province,
            Numero = request.Number,
            CNPJ = request.TaxId,
            Cidade = request.City,
            Pais = request.CountryCode,
            EMail = request.Email,
            Telefone = request.Phone,
            CEP = request.PostalCode,
            Contato = request.Contact,
            CertificadoDigital = new CertificadoDigital()
            {
                Senha = request.CertificatePassword
            }
        };

        await _cacheService.SetData<CiaAerea>(registrationToken, registry, DateTime.Now);

        return new()
        {
            Cidade = registry.Cidade,
            CNPJ = registry.CNPJ,
            Pais = registry.Pais,
            Endereco1 = registry.Endereco,
            Endereco2 = registry.Complemento,
            Estado = registry.UF,
            Nome = registry.Nome,
            Numero = registry.Numero,
            CEP = registry.CEP,
            Contato = registry.Contato,
            EMail = registry.EMail,
            Telefone = registry.Telefone
        };
    }

    public RegistryUpdateResponse SetRegistry(UserSession userSession, RegistryUpdateRequest request)
    {
        var registry = _configuraRepository.GetCompanyById(userSession.CompanyId);

        if (registry == null)
            throw new BusinessException("Empresa não registrada");

        registry.CNPJ = request.TaxId;
        registry.RazaoSocial = request.CompanyName;
        registry.Endereco = request.Address;
        registry.Complemento = request.Address2;
        registry.CEP = request.PostalCode;
        registry.UF = request.Province;
        registry.Cidade = request.City;
        registry.Pais = request.CountryCode;
        registry.Contato = request.Contact;
        registry.Telefone = request.Phone;
        registry.EMail = request.Email;
        registry.LogoUrl = request.LogoUrl;

        _configuraRepository.UpdateCompany(registry);
        _configuraRepository.SaveCompany();

        return new()
        {
            TaxId = registry.CNPJ,
            CompanyName = registry.RazaoSocial,
            Address = registry.Endereco,
            Address2 = registry.Complemento,
            PostalCode = registry.CEP,
            Province = registry.UF,
            City = registry.Cidade,
            CountryCode = registry.Pais,
            Contact = registry.Contato,
            Phone = registry.Telefone,
            Email = registry.EMail,
            LogoUrl = registry.LogoUrl
        };
    }

}
