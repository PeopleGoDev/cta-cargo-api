using CtaCargo.CctImportacao.Application.Dtos.Request;
using FluentValidation;
using FluentValidation.Results;

namespace CtaCargo.CctImportacao.Application.Validators
{
    public class UsuarioLoginRequestValidator : AbstractValidator<UsuarioLoginRequest>
    {
        public UsuarioLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("O e-mail é obrigatório!");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("A senha senha é obrigatória!");

            RuleFor(x => x.NovaSenha)
                .NotEmpty()
                .WithMessage("A nova senha está vazia!")
                .When(x => x.AlterarSenhar == true);

            RuleFor(x => x.NovaSenhaConfirmacao)
                .NotEmpty()
                .WithMessage("A confirmação da nova senha está vazia!")
                .When(x => x.AlterarSenhar == true);

            RuleFor(x => x.NovaSenha)
                .MaximumLength(20)
                .WithMessage("A nova senha excede os 20 caracteres permitidos!")
                .When(x => x.AlterarSenhar == true);

            RuleFor(x => x.NovaSenha)
                .MinimumLength(6)
                .WithMessage("A nova senha deve conter no mínimo 6 caracteres!")
                .When(x => x.AlterarSenhar == true);

            RuleFor(x => string.CompareOrdinal(x.NovaSenha, x.NovaSenhaConfirmacao))
                .Equal(0)
                .WithMessage("A senha de alteração não é igual a senha de confirmação de alteração!")
                .When(x => x.AlterarSenhar == true);
                
        }
    }
}
