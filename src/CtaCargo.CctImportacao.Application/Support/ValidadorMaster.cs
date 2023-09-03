using CtaCargo.CctImportacao.Application.Validators;
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public class ValidadorMaster : IValidadorMaster
{
    private readonly IErroMasterRepository _erroMasterRepository;

    public ValidadorMaster(IErroMasterRepository erroMasterRepository)
    {
        _erroMasterRepository = erroMasterRepository;
    }

    public void InserirErrosMaster(Master master)
    {
        MasterEntityValidator validator = new MasterEntityValidator();
        var result = validator.Validate(master);
        master.StatusId = result.IsValid ? 1 : 0;

        foreach (var erro in result.Errors)
        {
            master.ErrosMaster.Add(new ErroMaster
            {
                Erro = erro.ErrorMessage
            });
        }
    }

    public void TratarErrosMaster(Master master)
    {
        MasterEntityValidator validator = new MasterEntityValidator();
        var result = validator.Validate(master);
        master.StatusId = result.IsValid ? 1 : 0;

        _erroMasterRepository.DeleteErroMaster(master.ErrosMaster);

        foreach (var erro in result.Errors)
        {
            master.ErrosMaster.Add(new ErroMaster
            {
                Erro = erro.ErrorMessage
            });
        }
    }
}
