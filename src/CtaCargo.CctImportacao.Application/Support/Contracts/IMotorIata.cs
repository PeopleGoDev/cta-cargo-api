using CtaCargo.CctImportacao.Domain.Entities;
using CtaCargo.CctImportacao.Domain.Enums;
using System;

namespace CtaCargo.CctImportacao.Application.Support.Contracts;

public interface IMotorIata
{
    string GenFlightManifest(Voo voo, int? trechoId = null, DateTime? actualDateTime = null, bool isScheduled = false);
    string GenMasterManifest(Master master, IataXmlPurposeCode purposeCode);
}