using CtaCargo.CctImportacao.Domain.Entities;
using FluentValidation;

namespace CtaCargo.CctImportacao.Application.Validators
{
    public class MasterEntityValidator: AbstractValidator<Master>
    {
        public MasterEntityValidator()
        {
            RuleFor(x => x.Numero)
                .NotNull()
				.WithMessage("Número do master é obrigatório.");
			RuleFor(x => x.AeroportoOrigemId)
				.NotNull()
				.WithMessage("Porto de Origem inválido ou não informado.");
			RuleFor(x => x.AeroportoDestinoId)
				.NotNull()
				.WithMessage("Porto de Destino inválido ou não informado.");
			RuleFor(x => new { x.NaturezaCarga, x.NaturezaCargaId } )
				.Must(obj => ChecarNaturezaOperacao(obj.NaturezaCarga, obj.NaturezaCargaId))
				.WithMessage("Natureza da Operação inválida.");
			RuleFor(x => x.ConsignatarioNome)
                .NotEmpty()
				.WithMessage("Nome do Consignatário não informado.")
				.NotNull()
				.WithMessage("Nome do Consignatário não informado.");
            RuleFor(x => x.ConsignatarioPaisCodigo)
				.MinimumLength(2)
                .WithMessage("Sigla do País do consignatário inválido ou não informado.");
			RuleFor(x => x.ConsignatarioCNPJ)
				.Must(cnpj => CheckCNPJ(cnpj))
				.WithMessage("CNPJ do consignatário invalido");
			RuleFor(x => x.TotalPecas)
				.NotNull()
				.WithMessage("Número Total de Volumes não informado.");
			RuleFor(x => x.PesoTotalBruto)
				.NotNull()
				.WithMessage("Peso Total Bruto deve ser maior que zero.")
				.GreaterThan(0)
				.WithMessage("Peso Total Bruto deve ser maior que zero.");
			RuleFor(x => x.PesoTotalBrutoUN)
				.NotNull()
				.WithMessage("Unidade de peso deve conter 3 caracteres.")
				.MinimumLength(3)
				.WithMessage("Unidade de peso deve conter 3 caracteres.");
			RuleFor(x => x.DescricaoMercadoria)
				.NotNull()
				.WithMessage("Descrição da mercadoria é obrigatória.")
				.NotEmpty()
				.WithMessage("Descrição da mercadoria é obrigatória.");
			RuleFor(x => x.ValorFretePP)
				.NotNull()
				.WithMessage("Valor do Frete PrePaid não pode ser nulo.");
			RuleFor(x => x.ValorFretePPUN)
				.NotNull()
				.WithMessage("Unidade de moeda para o valor do Frete PrePaid deve conter 3 caracteres.")
				.NotEmpty()
				.WithMessage("Unidade de moeda para o valor do Frete PrePaid deve conter 3 caracteres.")
				.MinimumLength(3)
				.WithMessage("Unidade de moeda para o valor do Frete PrePaid deve conter 3 caracteres.");
			RuleFor(x => x.ValorFreteFC)
				.NotNull()
				.WithMessage("Valor do Frete Collect não pode ser nulo.");
			RuleFor(x => x.ValorFreteFCUN)
				.NotNull()
				.WithMessage("Unidade de moeda para o valor do Frete Collect deve conter 3 caracteres.")
				.NotEmpty()
				.WithMessage("Unidade de moeda para o valor do Frete Collect deve conter 3 caracteres.")
				.MinimumLength(3)
				.WithMessage("Unidade de moeda para o valor do Frete Collect deve conter 3 caracteres.");
			RuleFor(x => x.DataEmissaoXML)
				.NotNull()
				.WithMessage("Data de emissão é obrigatória.");
			//RuleFor(x => x.NCMLista)
			//	.NotNull()
			//	.WithMessage("É requerido ao menos 1 NCM no master.")
			//	.NotEmpty()
			//	.WithMessage("É requerido ao menos 1 NCM no master.");
			RuleFor(x => x.CountUldValidas())
				.NotEqual(0)
				.WithMessage("Não há ULD/BLK associado ao Master.");
			RuleFor(x => x.CodigoConteudo)
				.NotNull()
				.WithMessage("Campo Consolidado/Direto é obrigatório.");
		}

        private bool CheckCNPJ(string cnpj)
        {
			if(cnpj.StartsWith("PP"))
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

		private bool ChecarNaturezaOperacao(string naturezaOperacao, int? naturezaOperacaoId)
        {
			if ( naturezaOperacao == null || naturezaOperacao.Length == 0)
				return true;

			if (naturezaOperacao.Length == 3 && naturezaOperacaoId != null)
				return true;

			return false;
        }
	}
}
