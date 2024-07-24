
using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Repositories;
using CtaCargo.CctImportacao.Domain.Validator;
using System;

namespace CtaCargo.CctImportacao.Application.Validator;

public class ValidadorMaster : IValidadorMaster
{
    private readonly IErroMasterRepository _erroMasterRepository;
    public ValidadorMaster(IErroMasterRepository erroMasterRepository)
    {
        _erroMasterRepository = erroMasterRepository;
    }

    public void InserirErrosMaster(Master master)
    {
        master.ErrosMaster.ForEach(erroMaster =>
        {
            erroMaster.DataExclusao = DateTime.UtcNow;
            _erroMasterRepository.UpdateErroMaster(erroMaster);
        });

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
}
