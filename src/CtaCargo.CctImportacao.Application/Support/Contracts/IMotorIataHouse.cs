using CtaCargo.CctImportacao.Application.Dtos.Request;
using CtaCargo.CctImportacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;
public interface IMotorIataHouse
{
    string GenHouseManifest(House house, List<NaturezaCarga> naturezaCargaList, IataXmlPurposeCode purposeCode);
    string GenMasterHouseManifest(SubmeterRFBMasterHouseItemRequest masterInfo, List<House> houses, IataXmlPurposeCode purposeCode, DateTime issueDate);
}