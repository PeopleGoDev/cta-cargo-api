using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Support
{
	public class ProtocoloReceitaCheckFile
	{
		public string protocolNumber { get; set; }
		public DateTime dateTime { get; set; }
		public string fileType { get; set; }
		public string status { get; set; }
		public string cpf { get; set; }
		public string cnpj { get; set; }
		public ErrorListCheckFileRFB[] errorList { get; set; }
	}

    public class ErrorListCheckFileRFB
    {
		public string code { get; set; }
		public string description { get; set; }
		public string detail { get; set; }
	}

}
