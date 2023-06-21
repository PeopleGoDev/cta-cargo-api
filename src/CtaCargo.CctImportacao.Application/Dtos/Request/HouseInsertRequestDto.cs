using System;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class HouseInsertRequestDto: HouseBaseDto
    {
        public DateTime DataProcessamento { get; set; }
    }
}
