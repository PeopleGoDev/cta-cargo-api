namespace CtaCargo.CctImportacao.Domain.Validator;

public static class ValidaPassaporte
{
	public static bool IsPassporte(string passporte)
	{
		if (passporte.Trim().Length > 2)
			return true;
		return false;
	}
}
