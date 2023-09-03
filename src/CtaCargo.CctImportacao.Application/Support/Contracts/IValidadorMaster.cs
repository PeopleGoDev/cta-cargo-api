using CtaCargo.CctImportacao.Domain.Entities;

namespace CtaCargo.CctImportacao.Application.Support
{
    public interface IValidadorMaster
    {
        void InserirErrosMaster(Master master);
        void TratarErrosMaster(Master master);
    }
}