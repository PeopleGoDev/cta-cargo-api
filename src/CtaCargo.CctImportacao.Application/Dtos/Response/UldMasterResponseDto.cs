using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Response
{
    public class UldMasterResponseDto: UldMasterBaseDto
    {
        public int Id { get; set; }
        public int MasterId { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime? DataCricao { get; set; }
    }
}
