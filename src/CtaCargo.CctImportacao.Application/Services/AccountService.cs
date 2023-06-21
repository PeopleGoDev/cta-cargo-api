using AutoMapper;
using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Application.Dtos.Response;
using CtaCargo.CctImportacao.Application.Services.Contracts;
using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenSerice;
        private readonly IMapper _mapper;
        public AccountService(IUsuarioRepository usuarioRepository, IMapper mapper, ITokenService tokenSerice)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _tokenSerice = tokenSerice;
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
                return (
                    new ApiResponse<UsuarioLoginResponse>
                    {
                        Dados = null,
                        Sucesso = false,
                        Notificacoes = new List<Notificacao>() {
                                new Notificacao
                                {
                                    Codigo = "9999",
                                    Mensagem = "Usuário ou Senha invalidos!"
                                }
                        }
                    });

            string token = null;
            UsuarioInfoResponse userReaddto = null;

            bool alteraSenha = user.AlterarSenha;
            if (user.AlterarSenha == false)
            {
                token = _tokenSerice.GenerateToken(user);
                userReaddto = _mapper.Map<UsuarioInfoResponse>(user);
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

    }
}
