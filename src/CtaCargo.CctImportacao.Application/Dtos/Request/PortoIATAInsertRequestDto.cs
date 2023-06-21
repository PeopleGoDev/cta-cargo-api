using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos.Request
{
    public class PortoIATAInsertRequestDto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int UsuarioInsercaoId { get; set; }
        public int EmpresaId { get; set; }
    }
}
