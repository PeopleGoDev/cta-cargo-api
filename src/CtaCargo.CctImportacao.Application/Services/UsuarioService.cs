using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Support;
using CtaCargo.CctImportacao.Application.Support.Contracts;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
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
            try
            {
                var lista = await _usuarioRepository.GetUsuarioById(usuarioId);
                if (lista == null)
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Usuário não encontrado !"
                                }
                            }
                        };
                }
                var dto = _mapper.Map<UsuarioResponseDto>(lista);
                return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = dto,
                            Sucesso = true,
                            Notificacoes = null
                        };
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<UsuarioResponseDto>
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
        public async Task<ApiResponse<IEnumerable<UsuarioResponseDto>>> ListarUsuarios(int empresaId)
        {
            try
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
            catch (Exception ex)
            {
                return
                        new ApiResponse<IEnumerable<UsuarioResponseDto>>
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
        public async Task<ApiResponse<UsuarioResponseDto>> InserirUsuario(UsuarioInsertRequest usuarioRequest)
        {
            try
            {
                var password = GeneratePassword(true, true, true, true, 8);
                var usuarioModel = _mapper.Map<Usuario>(usuarioRequest);
                usuarioModel.CreatedDateTimeUtc = DateTime.UtcNow;
                usuarioModel.Senha = password;
                usuarioModel.AlterarSenha = true;

                _usuarioRepository.CreateUsuario(usuarioModel);

                string emailBody = "<p>Bem-vindo ao CCT Importação</p>";
                emailBody += $"<p>Sua senha é <b>{ password }</b></p>";
                _sendEmail.Email(usuarioModel.EMail, "Bem-vindo ao CCT Importação", emailBody);

                if (await _usuarioRepository.SaveChanges())
                {
                    var usuarioResponseDTO = _mapper.Map<UsuarioResponseDto>(usuarioModel);
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = usuarioResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não Foi possível adicionar o usuário: Erro Desconhecido!"
                                }
                            }
                        };
                }

            }
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não Foi possível adicionar o usuário: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<UsuarioResponseDto>> AtualizarUsuario(UsuarioUpdateRequest usuarioRequest)
        {
            try
            {
                var usuarioRepo = await _usuarioRepository.GetUsuarioById(usuarioRequest.UsuarioId);
                if (usuarioRepo == null)
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar o usuário: Usuário não encontrado !"
                                }
                            }
                        };
                }

                _mapper.Map(usuarioRequest, usuarioRepo);
                usuarioRepo.ModifiedDateTimeUtc = DateTime.UtcNow;

                _usuarioRepository.UpdateUsuario(usuarioRepo);

                if (await _usuarioRepository.SaveChanges())
                {
                    var usuarioResponseDTO = _mapper.Map<UsuarioResponseDto>(usuarioRepo);
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = usuarioResponseDTO,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível atualizar o usuário: Erro Desconhecido!"
                                }
                            }
                        };
                }

            }
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível atualizar o usuário: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<string>> ResetarUsuario(UsuarioUpdateRequest usuarioRequest)
        {
            try
            {
                var usuarioRepo = await _usuarioRepository.GetUsuarioById(usuarioRequest.UsuarioId);
                if (usuarioRepo == null)
                {
                    return
                        new ApiResponse<string>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível resetar a senha do usuário !"
                                }
                            }
                        };
                }

                var password = GeneratePassword(true, true, true, true, 8);
                usuarioRepo.Senha = password;
                usuarioRepo.AlterarSenha = true;
                usuarioRepo.DataReset = DateTime.UtcNow;

                string emailBody = "<p>Alteração senha CCT Importação</p>";
                emailBody += $"<p>Sua senha foi alterada para <b>{ password }</b></p>";
                _sendEmail.Email(usuarioRepo.EMail, "Alteração de Senha CCT Importação", emailBody);

                _usuarioRepository.UpdateUsuario(usuarioRepo);

                if (await _usuarioRepository.SaveChanges())
                {
                    var usuarioResponseDTO = _mapper.Map<UsuarioResponseDto>(usuarioRepo);
                    return
                        new ApiResponse<string>
                        {
                            Dados = "Senha do usuário resetada com sucesso!",
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<string>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível resetar senha do usuário!"
                                }
                            }
                        };
                }

            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<string>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível resetar senha do usuário: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        public async Task<ApiResponse<UsuarioResponseDto>> ExcluirUsuario(int usuarioId)
        {
            try
            {
                var usuarioRepo = await _usuarioRepository.GetUsuarioById(usuarioId);
                if (usuarioRepo == null)
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir usuário: Usuário não encontrado !"
                                }
                            }
                        };
                }

                usuarioRepo.DataExclusao = DateTime.UtcNow;

                _usuarioRepository.UpdateUsuario(usuarioRepo);

                if (await _usuarioRepository.SaveChanges())
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = true,
                            Notificacoes = null
                        };
                }
                else
                {
                    return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Não foi possível excluir usuário: Erro Desconhecido!"
                                }
                            }
                        };
                }

            }
            catch (DbUpdateException e)
            {
                return ErrorHandling(e);
            }
            catch (Exception ex)
            {
                return
                        new ApiResponse<UsuarioResponseDto>
                        {
                            Dados = null,
                            Sucesso = false,
                            Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = $"Não foi possível excluir usuário: {ex.Message} !"
                                }
                            }
                        };
            }

        }
        private ApiResponse<UsuarioResponseDto> ErrorHandling(Exception exception)
        {
            var sqlEx = exception?.InnerException as SqlException;
            if (sqlEx != null)
            {
                //This is a DbUpdateException on a SQL database

                if (sqlEx.Number == SqlServerViolationOfUniqueIndex)
                {
                    //We have an error we can process
                    return new ApiResponse<UsuarioResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"Já existe um usuário cadastrado com o mesmo E-mail !"
                                }
                        }
                    };
                }
                else
                {
                    return new ApiResponse<UsuarioResponseDto>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"SQL{sqlEx.Number.ToString()}",
                                    Mensagem = $"{sqlEx.Message}"
                                }
                        }
                    };
                }
            }
            else
            {
                return new ApiResponse<UsuarioResponseDto>
                {
                    Dados = null,
                    Sucesso = false,
                    Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = $"9999",
                                    Mensagem = $"{exception.Message}"
                                }
                        }
                };
            }

        }
        private string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
        {
            const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
            const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMBERS = "123456789";
            const string SPECIALS = @"!@$%&*#";

            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            System.Random _random = new Random();
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

            return String.Join(null, _password);
        }
    }
}
