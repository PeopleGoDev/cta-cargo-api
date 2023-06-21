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
            RuleFor(x => x.PortoIataDestinoId)
                .NotNull()
                .WithMessage("Código porto de destino não cadastrado.");
            RuleFor(x => x.DataHoraSaidaEstimada)
                .NotNull().WithMessage("Data e hora estimada de saida do vôo é obrigatoria.");
            RuleFor(x => x.DataHoraSaidaReal)
                .NotNull().WithMessage("Data e hora real de saida do vôo é obrigatória.");
            RuleFor(x => x.DataEmissaoXML)
                .NotNull().WithMessage("Data de emissão do voo nula, altere o voo para a correção.");
            
        }
    }
}
