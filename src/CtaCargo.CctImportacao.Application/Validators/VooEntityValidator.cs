using CtaCargo.CctImportacao.Domain.Entities;
using FluentValidation;

namespace CtaCargo.CctImportacao.Application.Validators
{
    public class VooEntityValidator: AbstractValidator<Voo>
    {
        public VooEntityValidator()
        {
            RuleFor(x => x.Numero)
                .NotNull().WithMessage("Número do Vôo é obrigatório.");
            RuleFor(x => x.PortoIataOrigemId)
                .NotNull()
                .WithMessage("Código porto de origem não cadastrado.");
            RuleFor(x => x.Trechos)
                .NotNull()
                .WithMessage("Ao menos um trecho é obrigatórrio")
                .NotEmpty()
                .WithMessage("Ao menos um trecho é obrigatórrio");
            RuleFor(x => x.DataHoraSaidaReal)
                .NotNull().WithMessage("Data e hora real de saida do vôo é obrigatória.");
            RuleFor(x => x.DataEmissaoXML)
                .NotNull().WithMessage("Data de emissão do voo nula, altere o voo para a correção.");
        }
    }
}
