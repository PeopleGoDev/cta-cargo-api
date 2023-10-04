using CtaCargo.CctImportacao.Domain.Entities;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Repository.Contracts;

public interface IErroMasterRepository
{
    void UpdateErroMaster(ErroMaster erroMaster);
    void DeleteErroMaster(List<ErroMaster> erroLista);
}