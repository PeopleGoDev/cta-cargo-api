using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services;

public class UsuarioService : IUsuarioService
{
    public const int SqlServerViolationOfUniqueIndex = 2601;
    public const int SqlServerViolationOfUniqueConstraint = 2627;

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;
    private readonly ISendEmail _sendEmail;
    public UsuarioService(IUsuarioRepository usuarioRepository,
        IMapper mapper,
        ISendEmail sendEmail)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _sendEmail = sendEmail;
    }

    public async Task<ApiResponse<UsuarioResponseDto>> UsuarioPorId(int usuarioId)
    {
        var lista = await _usuarioRepository.GetUsuarioById(usuarioId) ??
            throw new BusinessException("Usuário não encontroado");

        var dto = _mapper.Map<UsuarioResponseDto>(lista);

        return
                new ApiResponse<UsuarioResponseDto>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<IEnumerable<UsuarioResponseDto>>> ListarUsuarios(int empresaId)
    {
        var lista = await _usuarioRepository.GetAllUsuarios(empresaId);
        var dto = _mapper.Map<IEnumerable<UsuarioResponseDto>>(lista);

        return
                new ApiResponse<IEnumerable<UsuarioResponseDto>>
                {
                    Dados = dto,
                    Sucesso = true,
                    Notificacoes = null
                };
    }

    public async Task<ApiResponse<UsuarioResponseDto>> InserirUsuario(UserSession userSession, UsuarioInsertRequest usuarioRequest)
    {
        var password = GeneratePassword(true, true, true, true, 8);
        var usuarioModel = _mapper.Map<Usuario>(usuarioRequest);
        usuarioModel.Account = usuarioRequest.Account;
        usuarioModel.CreatedDateTimeUtc = DateTime.UtcNow;
        usuarioModel.Senha = password;
        usuarioModel.AlterarSenha = true;
        usuarioModel.EmpresaId = userSession.CompanyId;
        usuarioModel.CriadoPeloId = userSession.UserId;

        _usuarioRepository.CreateUsuario(usuarioModel);

        if (!await _usuarioRepository.SaveChanges())
            throw new BusinessException("Não Foi possível adicionar o usuário: Erro Desconhecido!");

        string emailBody = GenBody("Bem-vindo ao CCT Importação", usuarioModel.Account, password);
        _sendEmail.Email(usuarioModel.EMail, "CCT Importação - Bem-vindo", emailBody);

        var usuarioResponseDTO = _mapper.Map<UsuarioResponseDto>(usuarioModel);
        return
            new ApiResponse<UsuarioResponseDto>
            {
                Dados = usuarioResponseDTO,
                Sucesso = true,
                Notificacoes = null
            };
    }

    public async Task<ApiResponse<UsuarioResponseDto>> AtualizarUsuario(UsuarioUpdateRequest usuarioRequest)
    {
        var user = await _usuarioRepository.GetUsuarioById(usuarioRequest.UsuarioId) ??
            throw new BusinessException("Não foi possível atualizar o usuário: Usuário não encontrado !");

        _mapper.Map(usuarioRequest, user);
        user.EMail = usuarioRequest.Email;
        user.ModifiedDateTimeUtc = DateTime.UtcNow;

        _usuarioRepository.UpdateUsuario(user);

        if (!await _usuarioRepository.SaveChanges())
            throw new BusinessException("Não foi possível atualizar o usuário: Erro Desconhecido!");

        var usuarioResponseDTO = _mapper.Map<UsuarioResponseDto>(user);

        return
            new ApiResponse<UsuarioResponseDto>
            {
                Dados = usuarioResponseDTO,
                Sucesso = true,
                Notificacoes = null
            };
    }

    public async Task<ApiResponse<string>> ResetarUsuario(UserResetRequest usuarioRequest)
    {
        var user = await _usuarioRepository.GetUsuarioById(usuarioRequest.UserId) ??
            throw new BusinessException("Não foi possível resetar a senha do usuário !");

        var password = GeneratePassword(true, true, true, true, 8);
        user.Senha = password;
        user.AlterarSenha = true;
        user.DataReset = DateTime.UtcNow;

        string emailBody = GenBody("Reset de senha", user.Account, password);
        _sendEmail.Email(user.EMail, "CCT Importação - Reset de Senha", emailBody);

        _usuarioRepository.UpdateUsuario(user);

        if (!await _usuarioRepository.SaveChanges())
            throw new BusinessException("Não foi possível resetar senha do usuário!");

        return
            new ApiResponse<string>
            {
                Dados = "Senha do usuário resetada com sucesso!",
                Sucesso = true,
                Notificacoes = null
            };
    }

    public async Task<ApiResponse<UsuarioResponseDto>> ExcluirUsuario(int usuarioId)
    {
        var user = await _usuarioRepository.GetUsuarioById(usuarioId) ??
            throw new BusinessException("Não foi possível excluir usuário: Usuário não encontrado !");

        user.DataExclusao = DateTime.UtcNow;

        _usuarioRepository.UpdateUsuario(user);

        if (!await _usuarioRepository.SaveChanges())
            throw new BusinessException("Não foi possível excluir usuário: Erro Desconhecido!");

        return
            new ApiResponse<UsuarioResponseDto>
            {
                Dados = null,
                Sucesso = true,
                Notificacoes = null
            };
    }

    private static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
    {
        const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string NUMBERS = "123456789";
        const string SPECIALS = @"!@$%&*#";

        char[] _password = new char[passwordSize];
        string charSet = ""; // Initialise to blank
        Random _random = new();
        int counter;

        // Build up the character set to choose from
        if (useLowercase) charSet += LOWER_CASE;

        if (useUppercase) charSet += UPPER_CAES;

        if (useNumbers) charSet += NUMBERS;

        if (useSpecial) charSet += SPECIALS;

        for (counter = 0; counter < passwordSize; counter++)
        {
            _password[counter] = charSet[_random.Next(charSet.Length - 1)];
        }

        return string.Join(null, _password);
    }

    private string GenBody(string title, string user, string password)
    {
        return $"<h3>{title}</h3>"
            + $"<p>Usuário  <b>{user}</b></p>"
            + $"<p>Senha <b>{password}</b></p>";
    }
}
