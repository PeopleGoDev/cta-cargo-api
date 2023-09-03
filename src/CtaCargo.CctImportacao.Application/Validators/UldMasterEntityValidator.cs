using CtaCargo.CctImportacao.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Validators
{
    public class UldMasterEntityValidator : AbstractValidator<UldMaster>
    {
        public UldMasterEntityValidator()
        {
			RuleFor(x => x.ULDCaracteristicaCodigo)
				.NotEmpty()
				.WithMessage("Código Caracteristica é obrigatório.")
				.NotNull()
				.WithMessage("Código Caracteristica é obrigatório.");
			RuleFor(x => x.ULDCaracteristicaCodigo == "ULD" &&  x.ULDId == null)
				.NotEqual(true)
				.WithMessage("Número da ULD é obrigatório.");
			RuleFor(x => x.ULDCaracteristicaCodigo == "ULD" &&  x.ULDIdPrimario == null)
				.NotEqual(true)
				.WithMessage("Sigla da companhia aérea deve conter 2 caracteres.");
			RuleFor(x => x.QuantidadePecas)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Quantidade de peças deve ser maior que zero.");
			RuleFor(x => x.Peso)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Peso deve ser maior que zero.");
            RuleFor(x => x.PesoUN)
                .NotEmpty()
                .WithMessage("Unidade do Peso é obrigatório.")
                .NotNull()
                .WithMessage("Unidade do Peso é obrigatório.");
        }
    }
}
