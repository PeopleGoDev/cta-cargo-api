using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request;


public class MasterUpdateRequestDto: MasterBaseDto
{
    public int MasterId { get; set; }
    public int VooId { get; set; }
    public DateTime DataVoo { get; set; }
}
