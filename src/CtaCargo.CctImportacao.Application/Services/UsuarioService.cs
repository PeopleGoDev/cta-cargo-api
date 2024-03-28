using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Exceptions;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
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

    public async Task<ApiResponse<UsuarioResponseDto>> InserirUsuario(UsuarioInsertRequest usuarioRequest)
    {
        var password = GeneratePassword(true, true, true, true, 8);
        var usuarioModel = _mapper.Map<Usuario>(usuarioRequest);
        usuarioModel.Account = usuarioRequest.Account;
        usuarioModel.CreatedDateTimeUtc = DateTime.UtcNow;
        usuarioModel.Senha = password;
        usuarioModel.AlterarSenha = true;

        _usuarioRepository.CreateUsuario(usuarioModel);

        string emailBody = "<p>Bem-vindo ao CCT Importação</p>";
        emailBody += $"<p>Sua senha é <b>{password}</b></p>";
        _sendEmail.Email(usuarioModel.EMail, "Bem-vindo ao CCT Importação", emailBody);

        if (!await _usuarioRepository.SaveChanges())
            throw new BusinessException("Não Foi possível adicionar o usuário: Erro Desconhecido!");

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

        string emailBody = "<p>Alteração senha CCT Importação</p>";
        emailBody += $"<p>Sua senha foi alterada para <b>{password}</b></p>";
        _sendEmail.Email(user.EMail, "Alteração de Senha CCT Importação", emailBody);

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
}
