using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Domain.Repositories;

public interface IErroMasterRepository
{
    void UpdateErroMaster(ErroMaster erroMaster);
    void DeleteErroMaster(List<ErroMaster> erroLista);
}