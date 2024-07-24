using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Application.Validator;

public interface IValidadorMaster
{
    void InserirErrosMaster(Master master);
}