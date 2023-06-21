using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Application.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(Usuario user);
    }
}