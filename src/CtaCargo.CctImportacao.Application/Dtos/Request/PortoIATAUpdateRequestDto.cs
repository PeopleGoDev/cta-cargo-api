using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class PortoIATAUpdateRequestDto
    {
        public int PortoIATAId { get; set; }
        public string Nome { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
