using System;

namespace CtaCargo.CctImportacao.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() { }
    public DomainException(string mensagem) : base(mensagem) { }
    public DomainException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}

public class BusinessException : Exception
{
    public BusinessException() { }
    public BusinessException(string mensagem) : base(mensagem) { }
    public BusinessException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}
