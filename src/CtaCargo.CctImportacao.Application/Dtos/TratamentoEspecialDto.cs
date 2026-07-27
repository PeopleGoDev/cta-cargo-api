using CtaCargo.CctImportacao.Domain.Enums;

namespace CtaCargo.CctImportacao.Application.Dtos;

public class TratamentoEspecialDto
{
    public CargoHangling Tipo { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}