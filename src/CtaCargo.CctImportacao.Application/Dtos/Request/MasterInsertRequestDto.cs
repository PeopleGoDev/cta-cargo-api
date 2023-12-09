using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;

public class MasterInsertRequestDto: MasterBaseDto
{
    public int VooId { get; set; }
    public DateTime DataVoo { get; set; }
}
