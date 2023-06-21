using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Validators
{
    public static class ValidaPassaporte
    {
		public static bool IsPassporte(string passporte)
		{
			if(passporte.Trim().Length > 2)
				return true;
			return false;
		}
	}
}
