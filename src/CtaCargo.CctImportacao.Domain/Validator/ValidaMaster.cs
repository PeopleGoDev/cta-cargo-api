using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Domain.Validators;

public static class ValidaMaster
{
    public static bool IsMaster(string master)
    {
        if (master.Length != 11)
            return false;
        int intMaster;
        if (!int.TryParse(master, out intMaster))
            return false;

        int digitos7;
        int.TryParse(master.Substring(3, 7), out digitos7);
        int digito;
        int.TryParse(master.Substring(10), out digito);

        int digitoesperado = digitos7 % 7;
        if (digito == digitoesperado)
            return true;
        return false;
    }
}
