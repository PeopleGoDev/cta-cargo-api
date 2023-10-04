using CtaCargo.CctImportacao.Domain.Entities;
using FluentValidation;

namespace CtaCargo.CctImportacao.Domain.Validator;

public class HouseEntityValidator : AbstractValidator<House>
{
	public HouseEntityValidator()
	{
		RuleFor(x => x.Numero)
			.NotNull()
			.WithMessage("Número do house é obrigatório.");
		RuleFor(x => x.MasterNumeroXML)
			.NotNull()
			.NotEmpty()
			.WithMessage("Número do master é obrigatório.");
		RuleFor(x => x.ConsignatarioCNPJ)
			.Must(cnpj => CheckCNPJ(cnpj))
			.WithMessage("Número do master é invalido.");
		RuleFor(x => x.ConsignatarioNome)
			.NotEmpty()
			.NotNull()
			.WithMessage("Nome do consignatário é obrigatório.");
		RuleFor(x => x.ConsignatarioPaisCodigo)
			.MinimumLength(2)
			.WithMessage("Sigla do país do consignatário deve conter 2 caracteres.");
		RuleFor(x => x.ConsignatarioCNPJ)
			.Must(cnpj => CheckCNPJ(cnpj));
		RuleFor(x => x.TotalVolumes)
			.NotNull()
			.GreaterThan(0)
			.WithMessage("Número total de volumes deve ser maior que zero.");
		RuleFor(x => x.PesoTotalBruto)
			.NotNull()
			.GreaterThan(0)
			.WithMessage("Peso total bruto deve ser maior que zero.");
		RuleFor(x => x.PesoTotalBrutoUN)
			.NotNull()
			.MinimumLength(3)
			.WithMessage("Unidade de peso deve conter 3 caracteres.");
		RuleFor(x => x.DescricaoMercadoria)
			.NotNull()
			.NotEmpty()
			.WithMessage("Descrição da mercadoria é obrigatória.");
		RuleFor(x => x.ValorFretePP)
			.NotNull()
			.WithMessage("Valor do frete prepaid não pode ser nulo.");
		RuleFor(x => x.ValorFretePPUN)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.WithMessage("Unidade de moeda para o valor do frete prepaid deve conter 3 caracteres.");
		RuleFor(x => x.ValorFreteFC)
			.NotNull()
			.WithMessage("Valor do frete collect não pode ser nulo.");
		RuleFor(x => x.ValorFreteFCUN)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.WithMessage("Unidade de moeda para o valor do frete collect deve conter 3 caracteres.");
		RuleFor(x => x.DataEmissaoXML)
			.NotNull()
			.WithMessage("Data de emissão é obrigatória.");
		//RuleFor(x => x.NCMLista)
		//	.NotNull()
		//	.WithMessage("É requerido ao menos 1 NCM no house.")
		//	.NotEmpty()
		//	.WithMessage("É requerido ao menos 1 NCM no house.");
	}

	private bool CheckCNPJ(string cnpj)
	{
		if (cnpj == null || cnpj.Length == 0)
			return true;

		if (cnpj.StartsWith("PP"))
		{
			return ValidaPassaporte.IsPassporte(cnpj);
		}
		else if (cnpj.Length == 11)
		{
			return ValidaCPF.IsCpf(cnpj);
		}
		else if (cnpj.Length == 14)
		{
			return ValidaCNPJ.IsCnpj(cnpj);
		}
		else
		{
			return false;
		}
	}
}